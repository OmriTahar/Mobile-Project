using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public FirstPersonController player;
    public Slider cameraSlider;
    public TextMeshProUGUI CameraSettingsText;

    private void Start()
    {
        cameraSlider.value = player.cameraSensitivity;
        CameraSettingsText.text = player.cameraSensitivity.ToString();
    }

    public void ChangeCameraSensitivity()
    {
        player.cameraSensitivity = cameraSlider.value;
        CameraSettingsText.text = cameraSlider.value.ToString();
    }
}
