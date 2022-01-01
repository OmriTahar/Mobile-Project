using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickGun : Interactable
{

    public GameObject PlayerGun;
    public Image AimButton;
    public GameObject TriggerToActivate;
    public Animator GunAnimator;
   
    public override void OnInteraction()
    {
        PlayerGun.SetActive(true);
        GunAnimator.SetTrigger("PickedGun");
        FindObjectOfType<AudioManager>().PlaySound("Pistol Draw");

        AimButton.gameObject.SetActive(true);
        TriggerToActivate.SetActive(true);

        Destroy(gameObject);
    }
}
