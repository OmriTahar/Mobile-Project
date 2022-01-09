using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public FirstPersonController Player;
    public AudioManager audioManager;

    [Header("Pause Menu")]
    public PauseMenu pauseMenu;
    public GameObject pauseMenuPanel;
    public GameObject UICanvas;

    [Header("Level Finish")]
    public GameObject FinishLevelTrigger;
    public FinishLevel FinishLevelScript;
    public GameObject FinishLevelCamera;

    [Header("Level Finish")]
    public TextMeshProUGUI GameOverText;


    private void Start()
    {
        //Player.isAllowedToWalk = false;
        FinishLevelCamera.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Debug.Log("Main Menu!");
        SceneManager.LoadScene(0);
    }

    public void OpenPauseMenu()
    {
        audioManager.PlaySound("Click");

        Player.isAllowedToWalk = false;
        Player.isAllowedToLook = false;
        UICanvas.SetActive(false);

        pauseMenuPanel.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        audioManager.PlaySound("Click");

        Player.isAllowedToWalk = true;
        Player.isAllowedToLook = true;
        UICanvas.SetActive(true);

        pauseMenuPanel.SetActive(false);
    }

    public void WinCondition()
    {
        Player.isAllowedToWalk = false;
        audioManager.PlaySound("LevelWon");

        FinishLevelScript.isTriggered = false;
        Invoke("GoToMainMenu", 9f);
    }

    public void GameOver()
    {
        audioManager.PlaySound("GameOver");
        GameOverText.gameObject.SetActive(true);
        Invoke("GoToMainMenu", 5f);
    }

    
    public void QuitGame() 
    {
        Application.Quit();
    }
}
