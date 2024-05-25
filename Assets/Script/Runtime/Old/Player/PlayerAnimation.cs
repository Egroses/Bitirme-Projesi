using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    public void WalkOn()
    {
        animator.SetBool("Walking", true);
    }

    public void WalkOff()
    {
        animator.SetBool("Walking", false);
    }
}
