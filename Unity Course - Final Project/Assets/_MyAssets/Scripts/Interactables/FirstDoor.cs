using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoor : Interactable
{

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnInteraction()
    {
        animator.SetBool("Start", true);
    }

}
