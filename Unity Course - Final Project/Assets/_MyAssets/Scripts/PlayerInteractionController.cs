using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionController : MonoBehaviour
{

    [Header("Interaction Settings")]
    public float maxDistance = 5f;
    public LayerMask interactableLayers;

    [Header("UI: adapt this to fit your game")]
    public Button interactButton;

    private Interactable currentInteractable;

    private void Update()
    {

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance, interactableLayers))
        {
            currentInteractable = hit.collider.GetComponent<Interactable>();
        }
        else
        {
            currentInteractable = null;
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
