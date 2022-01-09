using UnityEngine;
using TMPro;

public class FinishLevel : MonoBehaviour
{

    [Header("References")]
    public GameManager gameManager;
    public GameObject HUD;
    public TextMeshProUGUI EndText;
    public GameObject PlayerCamera;
    public GameObject EndCamera;

    [Header("Torches")]
    public GameObject TorchLeft;
    public GameObject TorchRight;

    [Header("Inforamtion")]
    public bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        TorchLeft.SetActive(true);
        TorchRight.SetActive(true);
        isTriggered = true;
        LevelEnding();
    }

    public void LevelEnding()
    {
        HUD.SetActive(false);
        EndText.gameObject.SetActive(true);
        PlayerCamera.SetActive(false);
        EndCamera.SetActive(true);

        gameManager.WinCondition();
    }
}
