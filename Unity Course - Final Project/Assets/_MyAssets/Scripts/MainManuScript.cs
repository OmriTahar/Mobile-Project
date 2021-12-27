using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManuScript : MonoBehaviour
{

    [Header("Door Access States")]
    public GameObject OnPhase;
    public GameObject StandByPhase;
    public GameObject OffPhase;

    [Header("References")]
    public Animator Animator;
    public Camera ManuCamera;

    public void StartGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
