using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private Renderer myRenderer;

    public Material StandbyMat;
    public Material OnMat;
    public bool isTriggered = false;
    public bool isCounted = false;

    public GameObject TriggerToTurnOff;
    public GameObject DoorAccessOff;
    public GameObject DoorAccessStandBy;

    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    public void ChangeToStandby()
    {
        myRenderer.material = StandbyMat;
    }

    public void ChangeToOn()
    {
        myRenderer.material = OnMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            isTriggered = true;
            
            ChangeToStandby();

            if (DoorAccessOff != null)
            {
                DoorAccessOff.SetActive(false);
                DoorAccessStandBy.SetActive(true);
            }

            if (TriggerToTurnOff != null)
            {
                Destroy(TriggerToTurnOff);
            }
        }
    }
}
