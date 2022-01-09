using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkTutorialButton : MonoBehaviour
{
    public GameObject PauseButton;

    private void OnTriggerEnter(Collider other)
    {
        PauseButton.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        PauseButton.SetActive(true);
    }
}
