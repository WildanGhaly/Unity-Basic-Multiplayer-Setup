using UnityEngine;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHealthChanged))]
    private int currentHealth;

    [SerializeField] private int maxHealth = 100;

    public event System.Action<int, int> OnHealthChangedEvent;

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
    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // Add additional logic for player death if needed
        }
    }

    [Server]
    public void Heal(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void OnHealthChanged(int oldHealth, int newHealth)
    {
        OnHealthChangedEvent?.Invoke(newHealth, maxHealth);
        // Add additional logic for when health changes (e.g., updating UI)
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
