using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialEnd : MonoBehaviour
{
    public Weapon playerWeapon;
    public FirstPersonController player;
    public GameObject AttentionPanel;
    public GameObject HUD;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HUD.SetActive(false);
            playerWeapon.enabled = false;
            player.isAllowedToLook = false;
            player.isAllowedToWalk = false;

            playerWeapon._magCurrentAmmo = playerWeapon._magSize;
            AttentionPanel.SetActive(true);
            playerWeapon._isInfiniteAmmo = false;
        }
    }

    public void TutorialConfirm()
    {
        HUD.SetActive(true);
        playerWeapon.enabled = true;
        player.isAllowedToLook = true;
        player.isAllowedToWalk = true;

        AttentionPanel.SetActive(false);
        Destroy(gameObject, 1f);
    }

}
