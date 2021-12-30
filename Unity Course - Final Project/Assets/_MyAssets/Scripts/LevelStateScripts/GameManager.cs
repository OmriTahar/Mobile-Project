using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public FirstPersonController Player;

    [Header("Level Finish")]
    public GameObject EndTrigger;
    public FinishLevel finishLevel;
    public GameObject EndCamera;


    private void Start()
    {
        Player.isAllowedToWalk = false;
        EndCamera.SetActive(false);
    }

    private void Update()
    {
        if (finishLevel.isTriggered)
        {
            finishLevel.isTriggered = false;
            finishLevel.LevelEnding();
            Invoke("GoToMainMenu", 10f);
        }
    }

    public void GoToMainMenu()
    {
        Debug.Log("Main Menu!");
        SceneManager.LoadScene(0);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
