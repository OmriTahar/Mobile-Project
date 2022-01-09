using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInteractionController : MonoBehaviour
{

    [Header("Interaction Settings")]
    public float maxDistance = 5f;
    public LayerMask interactableLayers;

    [Header("UI Button")]
    public Button interactButton;

    [Header("Tutorial Helper")]
    public bool isAbleToInteract = false;

    private Interactable currentInteractable;

    private void Start()
    {
        interactButton.gameObject.SetActive(false);
    }

    private void Update()
    {

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance, interactableLayers))
        {
            isAbleToInteract = true;
            currentInteractable = hit.collider.GetComponent<Interactable>();
            interactButton.gameObject.SetActive(true);

        }
        else
        {
            isAbleToInteract = false;
            currentInteractable = null;
            interactButton.gameObject.SetActive(false);

        }

        interactButton.interactable = currentInteractable != null;
    }

    public void Interact()
    {
        if (currentInteractable)
        {
            currentInteractable.OnInteraction();
        }
    }

}
