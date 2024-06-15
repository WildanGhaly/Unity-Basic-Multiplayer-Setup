using Mirror;
using UnityEngine;

public abstract class Collidable : NetworkBehaviour
{
    public string promptMessage = "Collide";
    public bool eventCollideEnter;
    public bool eventCollideStay;
    public bool eventCollideExit;
    public LayerMask collisionLayers;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Something touch me");
        if (IsInCollisionLayer(collision.gameObject.layer))
        {
            if (isServer)
            {
                BaseCollideEnter(collision);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Something touch me2");
        if (IsInCollisionLayer(collision.gameObject.layer))
        {
            if (isServer)
            {
                BaseCollideStay(collision);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Something touch me3");
        if (IsInCollisionLayer(collision.gameObject.layer))
        {
            if (isServer)
            {
                BaseCollideExit(collision);
            }
        }
    }

    private bool IsInCollisionLayer(int layer)
    {
        return (collisionLayers.value & (1 << layer)) != 0;
    }

    [Server]
    private void BaseCollideEnter(Collision collision)
    {
        if (eventCollideEnter)
        {
            GetComponent<CollisionEventEnter>()?.unityEvent.Invoke();
        }
        CollideEnter(collision);
    }

    [Server]
    private void BaseCollideStay(Collision collision)
    {
        if (eventCollideStay)
        {
            GetComponent<CollisionEventStay>()?.unityEvent.Invoke();
        }
        CollideStay(collision);
    }

    [Server]
    private void BaseCollideExit(Collision collision)
    {
        if (eventCollideExit)
        {
            GetComponent<CollisionEventExit>()?.unityEvent.Invoke();
        }
        CollideExit(collision);
    }

    protected virtual void CollideEnter(Collision collision)
    {
        // Default behavior or leave empty for subclasses to override
    }

    protected virtual void CollideStay(Collision collision)
    {
        // Default behavior or leave empty for subclasses to override
    }

    protected virtual void CollideExit(Collision collision)
    {
        // Default behavior or leave empty for subclasses to override
    }
}
