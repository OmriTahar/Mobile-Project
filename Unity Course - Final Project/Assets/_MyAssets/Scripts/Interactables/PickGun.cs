using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickGun : Interactable
{

    public GameObject PlayerGun;
    public Image AimButton;
    public GameObject TriggerToActivate;
   
    public override void OnInteraction()
    {
        PlayerGun.SetActive(true);
        AimButton.gameObject.SetActive(true);
        TriggerToActivate.SetActive(true);

        Destroy(gameObject);
    }

    
}
