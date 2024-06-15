using UnityEngine;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHealthChanged))]
    private float currentHealth;

    [SerializeField] private float maxHealth = 100;

    public event System.Action<float, float> OnHealthChangedEvent;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    [Command(requiresAuthority = false)]
    public void CmdTakeDamage(float amount)
    {
        RpcTakeDamage(amount);
    }

    [ClientRpc]
    private void RpcTakeDamage(float amount)
    {
        TakeDamage(amount);
    }

    private void TakeDamage(float amount)
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

    private void DeathAnimation()
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

    [Command]
    public void CmdHeal(float amount)
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
