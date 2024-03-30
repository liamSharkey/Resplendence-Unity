using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;

    // Event declaration for statue removal
    public delegate void StatueRemoved();
    public event StatueRemoved OnStatueRemoved;

    // Start is called before the first frame update
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

        // Invoke the event when the statue is removed
        if (OnStatueRemoved != null)
            OnStatueRemoved();

        Destroy(gameObject);

        
    }
}