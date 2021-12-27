using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CameraBobbing _cameraBobbing;

    [Header("Look and Touch Settings")]
    [SerializeField] private float touchInputDeadZone; // Higher means less dead zone
    [SerializeField] private float cameraSensitivity;

    [Header("Movement Settings")]
    [SerializeField] private float actualMoveSpeed;
    [SerializeField] private float groundMoveSpeed;
    [SerializeField] private float stairsMoveSpeed;
    [SerializeField] float StairsCheckRadius;
    public bool isAllowedToWalk = true;

    [Header("Gravity & Jumping")]
    public float GravityWhenGrounded = 10;
    public float GravityInAir = 10;
    public float JumpForce = 10;
    private float _velocity;

    [Header("Ground Check")]
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask GroundLayers;
    [SerializeField] LayerMask StairsLayer;
    [SerializeField] float GroudCheckRadius;
    public bool IsGrounded;

    [Header("Other Checks")]
    public bool IsAiming;
    public bool IsOnStairs;

    // Touch detaction
    private int _leftFingerId, _rightFingerId;
    private float _halfScreenWidth;

    // Camera control
    private float _cameraPitch;
    private Vector2 lookInput;

    // Player movement
    private Vector2 moveTouchStartPosition;
    private Vector2 moveInput;


    void Start()
    {
        // Id = -1 means the finger is not being tracked
        _leftFingerId = -1;
        _rightFingerId = -1;

        _halfScreenWidth = Screen.width / 2;

        // calculate the movement input dead zone
        touchInputDeadZone = Mathf.Pow(Screen.height / touchInputDeadZone, 2);
    }

    void Update()
    {
        ApplyGravity();
        GetTouchInput();
        IsOnStairsCheck();

        if (_rightFingerId != -1) // Only look around if the right finger is being tracked
            LookAround(); 

        if (_leftFingerId != -1 && !IsAiming && isAllowedToWalk) // Only move if the left finger is being tracked
            Move();
        else
            _cameraBobbing.isWalking = false;
    }

    private void FixedUpdate()
    {
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroudCheckRadius, GroundLayers);
    }

    void IsOnStairsCheck()
    {
        IsOnStairs = Physics.CheckSphere(GroundCheck.position, StairsCheckRadius, StairsLayer);

        // Reduce move speed on stairs
        if (IsOnStairs)
            actualMoveSpeed = stairsMoveSpeed;
        else
            actualMoveSpeed = groundMoveSpeed;
    }

    void ApplyGravity()
    {
        if (IsGrounded && _velocity <= 0)
            _velocity = -GravityWhenGrounded * Time.deltaTime;
        else
            _velocity -= GravityInAir * Time.deltaTime;

        Vector3 verticalMovement = transform.up * _velocity;
        characterController.Move(verticalMovement * Time.deltaTime);

        
    }

    void GetTouchInput()
    {
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (touch.position.x < _halfScreenWidth && _leftFingerId == -1)
                    {
                        // Start tracking the left finger if it was not previously being tracked
                        _leftFingerId = touch.fingerId;

                        // Set the start position for the movement control finger
                        moveTouchStartPosition = touch.position;
                    }
                    else if (touch.position.x > _halfScreenWidth && _rightFingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        _rightFingerId = touch.fingerId;
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (touch.fingerId == _leftFingerId)
                    {
                        _leftFingerId = -1;
                        //Debug.Log("Stopped tracking left finger");
                    }
                    else if (touch.fingerId == _rightFingerId)
                    {
                        _rightFingerId = -1;
                        //Debug.Log("Stopped tracking right finger");
                    }

                    break;
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (touch.fingerId == _rightFingerId)
                    {
                        lookInput = touch.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    else if (touch.fingerId == _leftFingerId)
                    {
                        // calculating the position delta from the start position
                        moveInput = touch.position - moveTouchStartPosition;
                    }

                    break;
                case TouchPhase.Stationary:
                    // Set the look input to zero if the finger is still
                    if (touch.fingerId == _rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    void LookAround()
    {
        // vertical (pitch) rotation
        _cameraPitch = Mathf.Clamp(_cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);

        // horizontal (yaw) rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    void Move()
    {

        // Don't move if the touch delta is shorter than the designated dead zone
        if (moveInput.sqrMagnitude <= touchInputDeadZone)
        {
            _cameraBobbing.isWalking = false;
            return;
        }

        _cameraBobbing.isWalking = true;

        // Multiply the normalized direction by the speed
        Vector2 movementDirection = moveInput.normalized * actualMoveSpeed * Time.deltaTime;
        // Move relatively to the local transform's direction
        characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            _velocity = JumpForce;
        }
    }

    //private void OnDrawGizmos()
    //{

    //    if (IsGrounded)
    //    {
    //        Gizmos.color = Color.green;
    //    }
    //    else
    //    {
    //        Gizmos.color = Color.red;
    //    }

    //    Gizmos.DrawWireSphere(GroundCheck.position, StairsCheckRadius);
    //}

}