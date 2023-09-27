using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockyAnimation : MonoBehaviour
{
    Animator animator;
    float count;
    IEnumerator CountBlink()
    { 
        if(count > 0)
        {
            animator.SetBool("blink", false);
            count -= Time.deltaTime;
            Debug.Log(count);
        }
        else
        {
            animator.SetBool("blink", true);
            yield return new WaitForSeconds(0.5f);
            count = 5;
        }
        yield return null;
    }
    IEnumerator Jump()
    {
        animator.SetBool("jump", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("jump", false);
        yield return null;
    }

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        count = 5;
        animator.SetBool("blink", false);
        animator.SetBool("jump", false);
    }
    private void Update()
    {
        StartCoroutine(CountBlink());
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Jump());
        }
    }
}
