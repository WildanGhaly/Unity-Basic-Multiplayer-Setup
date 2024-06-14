using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string attackAnimationTrigger;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private Transform attackPoint;

    public void PerformAttack()
    {
        Debug.Log("Performing attack");
        if (animator != null && !string.IsNullOrEmpty(attackAnimationTrigger))
        {
            animator.SetTrigger(attackAnimationTrigger);
        }

        // Perform melee attack hit detection
        if (attackRange > 0)
        {
            Collider[] hitTargets = Physics.OverlapSphere(attackPoint.position, attackRange, targetMask);

            foreach (var target in hitTargets)
            {
                // Assuming the target has a PlayerHealth script
                if (target.TryGetComponent<PlayerHealth>(out PlayerHealth health))
                {
                    health.TakeDamage(attackDamage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Debug.Log("I am here");
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
