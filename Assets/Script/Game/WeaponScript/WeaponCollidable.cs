using Mirror;
using UnityEngine;

public class WeaponCollidable : Collidable
{
    [SerializeField] private float damage;

    protected override void CollideEnter(Collision collision)
    {
        // Check if the collided object has a Health component
        PlayerHealth targetHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (targetHealth != null)
        {
            // Apply damage to the target
            targetHealth.TakeDamage(damage);
        }
    }
}
