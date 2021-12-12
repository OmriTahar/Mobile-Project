using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActivateUITrigger : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI Text;
    public TextMeshProUGUI Text2;
    public GameObject TriggerToActivate;
    public FirstPersonController Player;

    public float DisableTime = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 9)
        {
            if (Image != null)
            {
                Image.gameObject.SetActive(true);
            }

            if (Text != null)
            {
                Text.gameObject.SetActive(true);
            }

            if (TriggerToActivate != null)
            {
                TriggerToActivate.SetActive(true);
            }

            Invoke("EndUITrigger", DisableTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.name == "Aim Trigger Object")
        {
            if (Player != null && Player.IsAiming)
            {
                Image.gameObject.SetActive(false);
                Text.gameObject.SetActive(false);
                Text2.gameObject.SetActive(true);
            }
            else
            {
                Image.gameObject.SetActive(true);
                Text.gameObject.SetActive(true);
                Text2.gameObject.SetActive(false);
            }
        }
    }

    void EndUITrigger()
    {
        if (Image != null)
        {
            Image.gameObject.SetActive(false);
        }

        if (Text != null)
        {
            Text.gameObject.SetActive(false);
        }

        if (Text2 != null)
        {
            Text2.gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {

        if (Image != null)
        {
            Image.gameObject.SetActive(false);
        }

        if (Text != null)
        {
            Text.gameObject.SetActive(false);
        }

        if (Text2 != null)
        {
            Text2.gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }

}
