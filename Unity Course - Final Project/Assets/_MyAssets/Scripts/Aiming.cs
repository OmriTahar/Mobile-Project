using System.Collections;
using UnityEngine;

public class Aiming : MonoBehaviour
{

    [Header("Animator")]
    public Animator animator;
    public string animatorParam = "Aiming";

    [Header("Camera")]
    public Camera camera;
    public float defaultFOV, aimingFOV;

    [Header("General")]
    public float aimDuration = 0.2f;

    [Header("Scope")]
    public bool enableScope;
    public MeshRenderer weaponRenderer;
    public GameObject scopeOverlay;

    [Header("Buttons")]
    //public GameObject fireButton;
    public GameObject aimingFireButton;

    [Header("Player")]
    public FirstPersonController Player;


    private void Start()
    {
        // Prevent errors
        if (!weaponRenderer || !scopeOverlay)
        {
            enableScope = false;
        }

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

        // Show weapon model and hide scope UI
        if (enableScope)
        {
            weaponRenderer.enabled = true;
            scopeOverlay.SetActive(false);
        }

        while (timeElapsed < aimDuration)
        {
            // Calculate the transition's progress
            blendValue = timeElapsed / aimDuration;

            // Blend bewteen animations and calculate the camera's FOV
            if (isAiming)
            {
                animator.SetFloat(animatorParam, blendValue);
                camera.fieldOfView = Mathf.Lerp(aimingFOV, defaultFOV, 1 - blendValue); // minus because we want to decrease FOV when aiming
                //fireButton.SetActive(false);
                aimingFireButton.SetActive(true);
                
            }
            else
            {
                animator.SetFloat(animatorParam, 1 - blendValue);
                camera.fieldOfView = Mathf.Lerp(aimingFOV, defaultFOV, blendValue);
                //fireButton.SetActive(true);
                aimingFireButton.SetActive(false);
            }

            // Increase timer
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // If scope is enabled, hide weapon model and show scope UI
        if (enableScope)
        {
            weaponRenderer.enabled = !isAiming;
            scopeOverlay.SetActive(isAiming);
        }

        // Confirm/Finalize changes
        if (isAiming)
        {
            animator.SetFloat(animatorParam, 1);
            camera.fieldOfView = aimingFOV;
            Player.IsAiming = true;
        }
        else
        {
            animator.SetFloat(animatorParam, 0);
            camera.fieldOfView = defaultFOV;
            Player.IsAiming = false;
        }

    }
}
