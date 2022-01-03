using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public FirstPersonController player;
    public Slider cameraSlider;
    public TextMeshProUGUI CameraSettingsText;

    public void ChangeCameraSensitivity()
    {
        player.cameraSensitivity = cameraSlider.value;
        CameraSettingsText.text = cameraSlider.value.ToString();
    }
}
