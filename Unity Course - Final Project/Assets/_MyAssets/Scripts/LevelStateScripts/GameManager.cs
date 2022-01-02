using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public FirstPersonController Player;

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

    private void Update()
    {
        if (FinishLevelScript.isTriggered)
        {
            FinishLevelScript.isTriggered = false;
            FinishLevelScript.LevelEnding();
            Invoke("GoToMainMenu", 10f);
        }
    }

    public void GoToMainMenu()
    {
        Debug.Log("Main Menu!");
        SceneManager.LoadScene(0);
    }

    public void LoseCondition()
    {
        GameOverText.gameObject.SetActive(true);
        Invoke("GoToMainMenu", 4f);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
