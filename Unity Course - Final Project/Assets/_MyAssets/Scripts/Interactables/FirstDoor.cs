using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoor : Interactable
{

    public Animator animator;
    public GameObject StandByPhase;
    public GameObject OnPhase;
    public GameObject TriggerToActivate;

    public override void OnInteraction()
    {
        StandByPhase.SetActive(false);
        OnPhase.SetActive(true);
        animator.SetBool("Start", true);
        gameObject.layer = 0;
        TriggerToActivate.SetActive(true);
    }

}
