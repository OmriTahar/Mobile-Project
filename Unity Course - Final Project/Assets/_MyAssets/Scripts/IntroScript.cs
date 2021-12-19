using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    public FirstPersonController Player;
    public GameObject TriggerToActivate;
    public Image image;

    private void Start()
    {
        //Player.isAllowedToWalk = false;
    }

    void Update()
    {
        Invoke("ActivateTrigger", 2f);
    }

    void ActivateTrigger()
    {
        TriggerToActivate.SetActive(true);
        image.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
