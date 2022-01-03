using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManuScript : MonoBehaviour
{

    [Header("Door Access States")]
    public GameObject OnPhase;
    public GameObject StandByPhase;
    public GameObject OffPhase;

    [Header("References")]
    public AudioManager audioManager;
    public Animator DoorAnimator;
    public Animator CameraAnimator;
    public Canvas canvas;

    private void Start()
    {
        audioManager.PlaySound("MenuTheme");
    }

    public void StartGame()
    {
        audioManager.PlaySound("BigClick");
        audioManager.FadeOutSound("MenuTheme", 2f);

        canvas.gameObject.SetActive(false);
        ChangeDoorAccess(OnPhase, StandByPhase);
        DoorAnimator.SetBool("Start", true);

        Invoke("MoveCamera", 2f);
        Invoke("GoToScene", 6f);
    }

    public void QuitGame()
    {
        audioManager.PlaySound("BigClick");
        ChangeDoorAccess(OffPhase, StandByPhase);
        Invoke("ActualQuit", 1f);
    }

    private void ActualQuit()
    {
        Application.Quit();
    }

    private void MoveCamera()
    {
        CameraAnimator.SetBool("Start", true);
    }

    private void GoToScene()
    {
        SceneManager.LoadScene(1);
    }

    private void ChangeDoorAccess(GameObject setActive, GameObject setNOTactive)
    {
        setActive.SetActive(true);
        setNOTactive.SetActive(false);
    }
}
