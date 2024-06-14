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

    private void Start()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        sword = inputManager.weapon;
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        if (inputManager.onFoot.MeleeAttack.triggered)
        {
            CmdMeleeAttack();
        }

        if (inputManager.onFoot.RangedAttack.triggered)
        {
            CmdRangedAttack();
        }
    }

    [Command]
    private void CmdMeleeAttack()
    {
        Debug.Log("Command melee attack");
        RpcMeleeAttack();
    }

    [ClientRpc]
    private void RpcMeleeAttack()
    {
        Debug.Log("Rpc melee attack");
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
        yield return new WaitForSeconds(2);
        sword.SetActive(false);
    }
}
