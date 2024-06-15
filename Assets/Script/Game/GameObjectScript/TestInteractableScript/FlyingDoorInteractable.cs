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
        isOpen = !isOpen;
        selfAnimator.SetBool("IsOpen", isOpen);
    }
}