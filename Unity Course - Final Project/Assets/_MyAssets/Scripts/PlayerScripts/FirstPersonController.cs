using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CameraBobbing _cameraBobbing;

    [Header("Movement & Look Settings")]
    public bool isAllowedToWalk = true;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private float moveInputDeadZone;

    [Header("Gravity & Jumping")]
    public float GravityWhenGrounded = 10;
    public float GravityInAir = 10;
    public float JumpForce = 10;
    private float _velocity;

    [Header("Ground Check")]
    public Transform GroundCheck;
    public LayerMask GroundLayers;
    public float GroudCheckRadius;
    public bool _isGrounded;

    [Header("Other Checks")]
    public bool IsAiming;

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
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);
    }

    void Update()
    {
        ApplyGravity();
        GetTouchInput();

        if (_rightFingerId != -1) // Only look around if the right finger is being tracked
            LookAround(); 

        if (_leftFingerId != -1 && !IsAiming && isAllowedToWalk) // Only move if the left finger is being tracked
            Move();
        else
            _cameraBobbing.isWalking = false;
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics.CheckSphere(GroundCheck.position, GroudCheckRadius, GroundLayers);
    }

    void ApplyGravity()
    {
        if (_isGrounded && _velocity <= 0)
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
        if (moveInput.sqrMagnitude <= moveInputDeadZone)
        {
            _cameraBobbing.isWalking = false;
            return;
        }

        _cameraBobbing.isWalking = true;

        // Multiply the normalized direction by the speed
        Vector2 movementDirection = moveInput.normalized * moveSpeed * Time.deltaTime;
        // Move relatively to the local transform's direction
        characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _velocity = JumpForce;
        }
    }

}