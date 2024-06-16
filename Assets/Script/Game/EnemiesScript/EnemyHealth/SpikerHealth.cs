using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikerHealth : EnemyHealth
{
    [SerializeField] private GameObject particle;

    protected override void DeathAnimation()
    {
        base.DeathAnimation();
        particle.SetActive(false);
    }
}
