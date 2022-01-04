using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMAG : Interactable
{

    public Weapon playerWeapon;
    public bool isPicked = false;

    public override void OnInteraction()
    {
        audioManager.PlaySound("PickUpMag");
        isPicked = true;
        playerWeapon.SpareAmmo += playerWeapon._magSize;
        playerWeapon.UpdateAmmoText();

        if (playerWeapon._magCurrentAmmo <= 0)
        {
            Invoke("AutoReload", 0.5f);
        }

        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }

    private void AutoReload()
    {
        playerWeapon.Reload();
    }
}
