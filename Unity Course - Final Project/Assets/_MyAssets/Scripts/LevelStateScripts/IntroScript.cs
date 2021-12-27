using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{

    public GameObject TriggerToActivate;
    public Image image;

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
