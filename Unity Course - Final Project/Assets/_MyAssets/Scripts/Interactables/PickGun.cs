using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickGun : Interactable
{

    public GameObject PlayerGun;
    public Image AimButton;

    public override void OnInteraction()
    {
        PlayerGun.SetActive(true);
        AimButton.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    
}
