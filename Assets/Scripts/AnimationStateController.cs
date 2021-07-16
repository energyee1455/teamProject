using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationStateController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidBody;
    private Vector2 velocity;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SetStateToAnimator();
    }

    private void SetStateToAnimator()
    {
        velocity = rigidBody.velocity;
        if (velocity.magnitude == 0)
        {
            animator.speed = 0.0f;
            return;
        }
        animator.speed = 1.0f;
        animator.SetFloat("X", velocity.x);
        animator.SetFloat("Y", velocity.y);
    }
}
