using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class CharacterAnimation : MonoBehaviour
{
    public Vector3 jumpDistance = new Vector3(0, 0.4f, 0);
    public float jumpDuration = 0.5f;
    private Animator animator;
    private Vector3 originalPosition;
    private float animationTimer;
    private bool isJumping;


    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("missed", false);
        animator.SetBool("good", false);
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (isJumping)
        {
            animationTimer += Time.deltaTime;
            float progress = animationTimer / jumpDuration;

            if (progress < 1.0f)
            {
                transform.position = Vector3.Lerp(originalPosition, originalPosition + jumpDistance, progress);
            }
            else
            {
                transform.position = originalPosition;
                animator.SetBool("missed", false);
                animator.SetBool("good", false);
                isJumping = false;
            }
        }
    }

    public void TriggerMissedAnimation()
    {
        //originalPosition = transform.position;
        animationTimer = 0f;
        animator.SetBool("missed", true);
        isJumping = true;
    }
    
    public void TriggerGoodAnimation()
    {
        animationTimer = 0f;
        animator.SetBool("good", true);
        isJumping = true;
    }

    
}
