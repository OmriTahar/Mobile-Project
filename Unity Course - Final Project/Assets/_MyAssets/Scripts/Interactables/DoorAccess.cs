using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAccess : Interactable
{


    [Header("Door Access States")]
    public bool isAccessedDoor = false;
    public GameObject OnPhase;
    public GameObject StandByPhase;
    public GameObject OffPhase;

    [Header("References")]
    public Animator Animator;
    public GameObject TriggerToActivate;
    public FirstPersonController Player;

    private void Start()
    {
        if (OffPhase.activeInHierarchy)
        {
            gameObject.layer = 0;
        }
        else if (StandByPhase.activeInHierarchy)
        {
            gameObject.layer = 7;
        }
    }

    public override void OnInteraction()
    {
        isAccessedDoor = true;
        gameObject.layer = 0;
        ChangeState(StandByPhase, OnPhase);
        Animator.SetBool("Start", true);

        if (TriggerToActivate != null)
        {
            TriggerToActivate.SetActive(true);
        }

        if (Player != null)
        {
            Player.isAllowedToWalk = true;
        }
    }

    public void ChangeState(GameObject currentState, GameObject newState)
    {
        currentState.SetActive(false);
        newState.SetActive(true);
    }

}
