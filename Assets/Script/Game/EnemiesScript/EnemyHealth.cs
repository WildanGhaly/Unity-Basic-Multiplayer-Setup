using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnHealthChanged(float oldHealth, float newHealth)
    {
        base.OnHealthChanged(oldHealth, newHealth);
    }

    protected override void RpcTakeDamage(float amount)
    {
        base.RpcTakeDamage(amount);
    }

    protected override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }

    protected override void DeathAnimation()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        Debug.Log("Enemy Death Animation Play");
    }

    protected override void RpcTakeHeal(float amount)
    {
        base.RpcTakeHeal(amount);
    }

    protected override void TakeHeal(float amount)
    {
        base.TakeHeal(amount);
    }
}
