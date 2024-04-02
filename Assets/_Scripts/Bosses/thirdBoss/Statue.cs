using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;

    public delegate void StatueRemoved();
    public event StatueRemoved OnStatueRemoved;

    void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public IEnumerator Die()
    {
        capsuleCollider.enabled = false;
        animator.SetBool("Break", true);
        yield return new WaitForSeconds(0);
    }

    public void remove()
    {

        if (OnStatueRemoved != null)
            OnStatueRemoved();

        Destroy(gameObject);

        
    }
}