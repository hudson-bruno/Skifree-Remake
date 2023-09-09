using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float maxDistance = 1.0f;
    public LayerMask interactableLayerMask;
    public KeyCode interactableKeyCode = KeyCode.E;

    public GameObject interactablePopUp;

    private void Awake()
    {
        interactablePopUp.SetActive(false);
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, interactableLayerMask)) {
            if (interactablePopUp.activeSelf)
            {
                interactablePopUp.SetActive(false);
            }

            return;
        }

        IInteractable interactable = hit.transform.GetComponent(typeof(IInteractable)) as IInteractable;
        if (interactable == null) { return; }

        interactablePopUp.transform.position = interactable.GetToolTipPosition();
        interactablePopUp.SetActive(true);

        if (Input.GetKeyDown(interactableKeyCode))
        {
            interactable.Interact(gameObject);
        }
    }
}
