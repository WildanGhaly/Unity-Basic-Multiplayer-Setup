using UnityEngine;
using Mirror;

public class PlayerHealth : Health
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
        GetComponent<Animator>().SetTrigger("TriggerDeath");
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
        if (isLocalPlayer)
        {
            GetComponent<InputManager>().onFoot.Disable();
            GetComponent<InputManager>().enabled = false;
        }
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
