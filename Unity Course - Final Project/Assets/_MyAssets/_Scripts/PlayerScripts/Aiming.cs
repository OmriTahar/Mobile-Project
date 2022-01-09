using System.Collections;
using UnityEngine;

public class Aiming : MonoBehaviour
{

    [Header("Animator")]
    public Animator animator;
    public string animatorParam = "IsAiming";

    [Header("Camera")]
    public Camera camera;
    public float defaultFOV, aimingFOV;

    [Header("General")]
    public float aimDuration = 0.2f;

    [Header("Buttons")]
    public GameObject pauseButton;
    public GameObject aimingFireButton;

    [Header("Player")]
    public FirstPersonController Player;
    public Weapon PlayerWeapon;


    private void Start()
    {
        aimingFireButton.SetActive(false);
    }

    public void OnAim(bool state)
    {
        StopAllCoroutines();
        StartCoroutine(Aim(state));

    }

    private IEnumerator Aim(bool isAiming)
    {
        float blendValue = 0;   // Progress of animation
        float timeElapsed = 0;  // Time passed since animation started

        while (timeElapsed < aimDuration)
        {
            // Calculate the transition's progress
            blendValue = timeElapsed / aimDuration;

            // Blend bewteen animations and calculate the camera's FOV
            if (isAiming)
            {
                //animator.SetFloat(animatorParam, blendValue);
                camera.fieldOfView = Mathf.Lerp(aimingFOV, defaultFOV, 1 - blendValue); // minus because we want to decrease FOV when aiming
                //fireButton.SetActive(false);
                pauseButton.SetActive(false);
                aimingFireButton.SetActive(true);
            }
            else
            {
                //animator.SetFloat(animatorParam, 1 - blendValue);
                camera.fieldOfView = Mathf.Lerp(aimingFOV, defaultFOV, blendValue);
                //fireButton.SetActive(true);
                aimingFireButton.SetActive(false);
                pauseButton.SetActive(true);
            }

            // Increase timer
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Confirm/Finalize changes
        if (isAiming)
        {
            //animator.SetFloat(animatorParam, 1);
            animator.SetBool(animatorParam, true);
            camera.fieldOfView = aimingFOV;
            Player.IsAiming = true;
        }
        else
        {
            //animator.SetFloat(animatorParam, 0);
            animator.SetBool(animatorParam, false);
            camera.fieldOfView = defaultFOV;
            Player.IsAiming = false;
        }

    }
}
