using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyAnimationControl : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PressKey()
    {
        animator.Play("Press Down");
    }

    public void ReleaseKey()
    {
        animator.Play("Release");
    }
}
