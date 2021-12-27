using UnityEngine;
using TMPro;

public class FinishLevel : MonoBehaviour
{

    [Header("References")]
    public GameObject HUD;
    public TextMeshProUGUI EndText;
    public GameObject PlayerCamera;
    public GameObject EndCamera;

    [Header("Inforamtion")]
    public bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        LevelEnding();
    }

    public void LevelEnding()
    {
        HUD.SetActive(false);
        EndText.gameObject.SetActive(true);
        PlayerCamera.SetActive(false);
        EndCamera.SetActive(true);
    }
}
