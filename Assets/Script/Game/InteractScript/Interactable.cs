using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public abstract class Interactable : NetworkBehaviour
{
    public bool eventInteract;
    public string promptMessage = "Interactable";
    public void BaseInteract()
    {
        if (!isLocalPlayer) return;

        if (eventInteract)
        {
            GetComponent<InteractionEvent>().unityEvent.Invoke();
        }
        Interact();
    }

    protected virtual void Interact()
    {

    }
}
