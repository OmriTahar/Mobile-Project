using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public FirstPersonController player;
    public Slider cameraSlider;

    public void ChangeCameraSensitivity()
    {
        player.cameraSensitivity = cameraSlider.value;
    }
}
