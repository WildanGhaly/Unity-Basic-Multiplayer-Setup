using Mirror;
using UnityEngine;

public class WeaponCollidable : Collidable
{
    [SerializeField] private float damage;

    protected override void CollideEnter(Collision collision)
    {
        Debug.Log("Collide enter");

        if (!isServer) return;

        PlayerHealth targetHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (targetHealth != null)
        {
            Debug.Log("HIT!!!");
            targetHealth.CmdTakeDamage(damage);
        }
    }
}
