using UnityEngine;
using Mirror;

public class EnemyAttack : NetworkBehaviour
{
    [SerializeField]
    private float attackRate = 1.0f;

    [SerializeField]
    private float attackDamage = 10f;

    private float nextAttackTime = 0f;

    public void Attack(Transform target)
    {
        if (Time.time >= nextAttackTime)
        {
            Debug.Log("Attacking " + target.name);

            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.CmdTakeDamage(attackDamage);
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
}
