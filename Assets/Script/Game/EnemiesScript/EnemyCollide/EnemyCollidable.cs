using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemyCollidable : Collidable
{
    [SerializeField]
    private float damagePerSecond = 10f;

    protected override void TriggerStay(GameObject other)
    {
        base.TriggerStay(other);
        Debug.Log("STAYYY");
        other.GetComponent<Health>().CmdTakeDamage(damagePerSecond * Time.fixedDeltaTime);
    }
}
