using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActivateUITrigger : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI Text;

    private void OnTriggerEnter(Collider other)
    {
        Image.gameObject.SetActive(true);
        Text.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Image.gameObject.SetActive(false);
        Text.gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
