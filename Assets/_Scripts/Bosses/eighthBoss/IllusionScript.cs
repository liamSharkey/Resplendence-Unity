using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionScript : MonoBehaviour
{
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;

    // Event declaration for statue removal
    public delegate void StatueRemoved();
    public event StatueRemoved OnStatueRemoved;

    // I'd like it to use random sprites so the illusions have some variety. The logic should work but I think it's something to do with the way unity does animations.
    public Sprite[] sprites;

    public GameObject projectilePrefab;

    private float shootingInterval = 3f;
    private float lastShotTime;

    private GameObject shot;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }

    void Update()
    {
        if (Time.time - lastShotTime > shootingInterval)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null)
        {
            shot = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }


    public IEnumerator Die()
    {
        Destroy(shot);
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