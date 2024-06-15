using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class Health : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHealthChanged))]
    protected float currentHealth;

    [SerializeField] 
    protected float maxHealth = 100;

    public event System.Action<float, float> OnHealthChangedEvent;

    protected virtual void OnHealthChanged(float oldHealth, float newHealth)
    {
        OnHealthChangedEvent?.Invoke(newHealth, maxHealth);
        // Add additional logic for when health changes (e.g., updating UI)
    }

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    [Command(requiresAuthority = false)]
    public void CmdTakeDamage(float amount)
    {
        RpcTakeDamage(amount);
    }

    [ClientRpc]
    protected virtual void RpcTakeDamage(float amount)
    {
        TakeDamage(amount);
    }

    protected virtual void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        Debug.Log("I got hit, my currentHealth is " + currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            DeathAnimation();
        }
    }

    protected virtual void DeathAnimation()
    {

    }

    [Command(requiresAuthority = false)]
    public void CmdHeal(float amount)
    {
        RpcTakeHeal(amount);
    }

    [ClientRpc]
    protected virtual void RpcTakeHeal(float amount)
    {
        TakeHeal(amount);
    }

    protected virtual void TakeHeal(float amount)
    {
        if (currentHealth <= 0) return;

        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
