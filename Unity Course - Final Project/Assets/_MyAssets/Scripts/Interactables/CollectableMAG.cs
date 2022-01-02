using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMAG : Interactable
{

    public Weapon playerWeapon;

    public override void OnInteraction()
    {
        playerWeapon.SpareAmmo += playerWeapon._magSize;
        Destroy(gameObject, 1f);
    }
}
