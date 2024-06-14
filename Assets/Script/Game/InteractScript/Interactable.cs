using Mirror;

public abstract class Interactable : NetworkBehaviour
{
    public bool eventInteract;
    public string promptMessage = "Interactable";

    [Server]
    public void ServerInteract()
    {
        if (eventInteract)
        {
            GetComponent<InteractionEvent>().unityEvent.Invoke();
        }
        RpcInteract();
    }

    [ClientRpc]
    private void RpcInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
    }
}