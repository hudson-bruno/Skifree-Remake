using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostessBehaviour : MonoBehaviour, IInteractable
{
    public Transform toolTipPosition;

    public void Interact(GameObject interactor)
    {
        Debug.Log(name + " interacted!");
    }
    public Vector3 GetToolTipPosition()
    {
        return toolTipPosition.position;
    }
}
