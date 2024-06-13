using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDoorAnimator : Interactable
{
    private Animator selfAnimator;

    private bool isOpen;

    private void Awake()
    {
        selfAnimator = GetComponent<Animator>();
    }

    protected override void Interact()
    {
        base.Interact();
        isOpen = !isOpen;
        selfAnimator.SetBool("IsOpen", isOpen);
    }
}
