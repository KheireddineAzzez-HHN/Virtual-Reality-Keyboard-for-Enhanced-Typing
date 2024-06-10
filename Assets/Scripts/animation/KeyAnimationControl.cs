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

    public void PressKey(KeyboardConfig.KeyNames keyname)
    {
        if (keyname == KeyboardConfig.KeyNames.key_space)
        {

            animator.Play("KeySpace_Release");

        }
        else
        {
            animator.Play("Press Down");

        }
    }

    public void ReleaseKey(KeyboardConfig.KeyNames keyname)
    {
        if (keyname == KeyboardConfig.KeyNames.key_space)
        {

            animator.Play("KeySpace_Press");

        }
        else
        {

            animator.Play("Release");

        }
    }

    public bool IsAnimating()
    {
        return animator.GetBool("isAnimating");
    }
}
