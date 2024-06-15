using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerAttack : NetworkBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private LayerMask targetMask;

    private Animator animator;
    private InputManager inputManager;
    private GameObject sword;

    [SerializeField] private float maxAttackPerSecond = 2f;
    [SerializeField] private float meleeAttackRange = 1f;
    [SerializeField] private float attackDamage = 10f;

    private float currentTime;
    private float resetTime;

    private void Awake()
    {
        currentTime = 0;
        resetTime = 1f / maxAttackPerSecond;
        
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
    }

    private void Start()
    {
        sword = inputManager.weapon;
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        currentTime += Time.deltaTime;

        if (inputManager.onFoot.MeleeAttack.triggered)
        {
            if (currentTime < resetTime) return;

            currentTime = 0;
            LocalMeleeAttack();
        }

        if (inputManager.onFoot.RangedAttack.triggered)
        {
            if (currentTime < resetTime) return;

            currentTime = 0;
            CmdRangedAttack();
        }
    }

    private void LocalMeleeAttack()
    {
        StopAllCoroutines();
        StartCoroutine(SwordVisibleTimer());
        CmdMeleeAttack();
    }

    [Command]
    private void CmdMeleeAttack()
    {
        Debug.Log("Command melee attack");
        RpcAnimateMeleeAttack();
        PerformMeleeAttack();
    }

    [ClientRpc]
    private void RpcAnimateMeleeAttack()
    {
        animator.SetTrigger("TriggerSword");
    }

    private void PerformMeleeAttack()
    {
        if (meleeAttackRange > 0)
        {
            Collider[] hitTargets = Physics.OverlapSphere(attackPoint.position, meleeAttackRange, targetMask);

            foreach (var target in hitTargets)
            {
                // Assuming the target has a PlayerHealth script
                if (target.TryGetComponent<PlayerHealth>(out PlayerHealth health))
                {
                    Debug.Log("Hit a person");
                    health.CmdTakeDamage(attackDamage);
                }
            }
        }
    }

    [Command]
    private void CmdRangedAttack()
    {
        RpcRangedAttack();
    }

    [ClientRpc]
    private void RpcRangedAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, attackPoint.rotation);
        projectile.GetComponent<Rigidbody>().velocity = attackPoint.forward * projectileSpeed;
        NetworkServer.Spawn(projectile);
    }

    IEnumerator SwordVisibleTimer()
    {
        sword.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        sword.SetActive(false);
    }
}
