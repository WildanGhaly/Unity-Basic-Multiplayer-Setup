using UnityEngine;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHealthChanged))]
    private float currentHealth;

    [SerializeField] private float maxHealth = 100;

    public event System.Action<float, float> OnHealthChangedEvent;

    public override void OnStartLocalPlayer()
    {
        if (isServer)
        {
            currentHealth = maxHealth;
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        currentHealth = maxHealth;
    }

    [Server]
    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        Debug.Log("I got hit, my currentHealth is "+ currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // Add additional logic for player death if needed
        }
    }

    [Server]
    public void Heal(float amount)
    {
        if (currentHealth <= 0) return;

        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void OnHealthChanged(float oldHealth, float newHealth)
    {
        OnHealthChangedEvent?.Invoke(newHealth, maxHealth);
        // Add additional logic for when health changes (e.g., updating UI)
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
