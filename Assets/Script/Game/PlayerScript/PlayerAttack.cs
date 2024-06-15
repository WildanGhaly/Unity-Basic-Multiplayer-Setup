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

    private float currentTime;
    private float resetTime;

    private void Start()
    {
        currentTime = 0;
        resetTime = 1f / maxAttackPerSecond;
        
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
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
            CmdMeleeAttack();
        }

        if (inputManager.onFoot.RangedAttack.triggered)
        {
            if (currentTime < resetTime) return;

            currentTime = 0;
            CmdRangedAttack();
        }
    }

    [Command]
    private void CmdMeleeAttack()
    {
        Debug.Log("Command melee attack");
        animator.SetTrigger("TriggerSword");
        StopAllCoroutines();
        StartCoroutine(SwordVisibleTimer());
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
