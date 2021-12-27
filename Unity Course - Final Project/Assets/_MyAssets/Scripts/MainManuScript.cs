using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManuScript : MonoBehaviour
{

    [Header("Door Access States")]
    public GameObject OnPhase;
    public GameObject StandByPhase;
    public GameObject OffPhase;

    [Header("References")]
    public Animator DoorAnimator;
    public Animator CameraAnimator;
    public Canvas canvas;

    public void StartGame()
    {
        canvas.gameObject.SetActive(false);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
