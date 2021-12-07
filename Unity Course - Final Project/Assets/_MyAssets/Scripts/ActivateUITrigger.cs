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
    public GameObject triggerToActivate;

    private void OnTriggerEnter(Collider other)
    {

        if (Image != null)
        {
            Image.gameObject.SetActive(true);
        }

        if (Text != null)
        {
            Text.gameObject.SetActive(true);
        }

        if (Text2 != null)
        {
            Text2.gameObject.SetActive(true);
        }

        if (triggerToActivate != null)
        {
            triggerToActivate.SetActive(true);
        }
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
