using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAccess : Interactable
{

    public Animator animator;

    public GameObject OffPhase;
    public GameObject StandByPhase;
    public GameObject OnPhase;

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
        gameObject.layer = 0;

        StandByPhase.SetActive(false);
        OnPhase.SetActive(true);

        animator.SetBool("Start", true);

        if (TriggerToActivate != null)
        {
            TriggerToActivate.SetActive(true);
        }

        if (Player != null)
        {
            Player.isAllowedToWalk = true;
        }

        Debug.Log("Pressed Door Key!");
    }

}
