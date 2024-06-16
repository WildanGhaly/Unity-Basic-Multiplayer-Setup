using Mirror;
using UnityEngine;

public abstract class Collidable : NetworkBehaviour
{
    public string promptMessage = "Collide";
    public bool eventCollideEnter;
    public bool eventCollideStay;
    public bool eventCollideExit;
    public LayerMask collisionLayers;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something touched me");
        if (IsInCollisionLayer(other.gameObject.layer))
        {
            if (isServer)
            {
                RpcBaseTriggerEnter(other.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Something is touching me");
        if (IsInCollisionLayer(other.gameObject.layer))
        {
            if (isServer)
            {
                RpcBaseTriggerStay(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Something stopped touching me");
        if (IsInCollisionLayer(other.gameObject.layer))
        {
            if (isServer)
            {
                RpcBaseTriggerExit(other.gameObject);
            }
        }
    }

    private bool IsInCollisionLayer(int layer)
    {
        return (collisionLayers.value & (1 << layer)) != 0;
    }

    [ClientRpc]
    private void RpcBaseTriggerEnter(GameObject other)
    {
        if (eventCollideEnter)
        {
            GetComponent<CollisionEventEnter>()?.unityEvent.Invoke();
        }
        TriggerEnter(other);
    }

    [ClientRpc]
    private void RpcBaseTriggerStay(GameObject other)
    {
        if (eventCollideStay)
        {
            GetComponent<CollisionEventStay>()?.unityEvent.Invoke();
        }
        TriggerStay(other);
    }

    [ClientRpc]
    private void RpcBaseTriggerExit(GameObject other)
    {
        if (eventCollideExit)
        {
            GetComponent<CollisionEventExit>()?.unityEvent.Invoke();
        }
        TriggerExit(other);
    }

    protected virtual void TriggerEnter(GameObject other)
    {
        // Default behavior or leave empty for subclasses to override
    }

    protected virtual void TriggerStay(GameObject other)
    {
        // Default behavior or leave empty for subclasses to override
    }

    protected virtual void TriggerExit(GameObject other)
    {
        // Default behavior or leave empty for subclasses to override
    }
}
