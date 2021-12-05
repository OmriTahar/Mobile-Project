using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    [Header("Transform Refrences")]
    public Transform HeadTransform;
    public Transform CameraTransform;
    
    [Header("Head Bobbing")]
    public float BobFrequency = 5f;
    public float BobHorizontalAmplitude = 0.1f;
    public float BobVerticalAmplitude = 0.1f;
    [Range(0, 1)] public float HeadBobSmoothing = 0.1f;

    // State
    public bool isWalking;
    private float _walkingTime;
    private Vector3 _targetCameraPosition;

    void Update()
    {
        // set time and offset to 0
        if (!isWalking)
        {
            _walkingTime = 0f;
        }
        else
        {
            _walkingTime += Time.deltaTime;
        }

        // calculate the camera's target position
        _targetCameraPosition = HeadTransform.position + CalculateHeadBobOffset(_walkingTime);

        // Interpolate position
        CameraTransform.position = Vector3.Lerp(CameraTransform.position, _targetCameraPosition, HeadBobSmoothing);

        // snap to position if it is close enough
        if ((CameraTransform.position - _targetCameraPosition).magnitude <= 0.001)
        {
            CameraTransform.position = _targetCameraPosition;
        }
    }

    private Vector3 CalculateHeadBobOffset(float t)
    {
        float horizontalOffest = 0;
        float verticalOffset = 0;
        Vector3 offset = Vector3.zero;

        if (t > 0)
        {
            // Calculate offsets
            horizontalOffest = Mathf.Cos(t * BobFrequency) * BobHorizontalAmplitude;
            verticalOffset = Mathf.Sin(t * BobFrequency * 2) * BobVerticalAmplitude;

            // Combine offsets relative to the head's position and calculate the camera's target position
            offset = HeadTransform.right * horizontalOffest + HeadTransform.up * verticalOffset;
        }

        return offset;
    }
}
