using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeventhBoss : Boss
{
    public float retreatSpeed = 15f;
    public float pauseDuration = 5f;
    public int shotsFired = 3;
    public float fireRate = 0.25f;
    public GameObject largeProjectilePrefab; 
    public float largeProjectileSpeed = 5f; 
    public float regularProjectileSpeed = 15f; 

    private Vector3 retreatPosition;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player"); 
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        StartCoroutine(BehaviorRoutine());
        bossNumber = 7;
        movementSpeed = 7;
        fireTime = 0.15f;
        moveTime = 5f;
        maxHealth = 400;
        UniversalStart();
    }

    void Update()
    {
        if (health <= maxHealth / 2 && !inPhaseTwo)
        {
            inPhaseTwo = true;
        }

        if (health <= 0 && !dead)
        {
            StartCoroutine(die());
        }
    }

    private IEnumerator BehaviorRoutine()
    {
        while (!dead)
        {
            if (!inPhaseTwo)
            {
                for (int i = 0; i < shotsFired; i++)
                {
                    FireProjectile(regularProjectileSpeed);
                    yield return new WaitForSeconds(fireRate);
                }
                StartCoroutine(RetreatFromPlayer());
                yield return new WaitForSeconds(pauseDuration);
            }
            else
            {
                yield return new WaitForSeconds(pauseDuration / 2);

                FireLargeProjectile();

                yield return new WaitForSeconds(pauseDuration / 2);
                for (int i = 0; i < shotsFired; i++)
                {
                    FireProjectile(regularProjectileSpeed * 1.5f); 
                    yield return new WaitForSeconds(fireRate / 2);
                }
                yield return new WaitForSeconds(pauseDuration / 2);

                StartCoroutine(RetreatFromPlayer());
                yield return new WaitForSeconds(pauseDuration / 2);
            }
        }
    }

    private void FireProjectile(float speed)
    {
        if (playerTransform != null)
        {
            GameObject projectile = Instantiate(bossProjectile, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                rb.velocity = direction * speed;
            }
        }
    }

    private void FireLargeProjectile()
    {
        if (playerTransform != null)
        {
            GameObject projectile = Instantiate(largeProjectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                rb.velocity = direction * largeProjectileSpeed;
            }
        }
    }


    private IEnumerator RetreatFromPlayer()
    {
        float startTime = Time.time;
        transform.localScale = new Vector3(0.75f, 0.75f, 1);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.1f);

        spriteRenderer.color = transparentColor;

        Vector3 direction = (transform.position - playerTransform.position).normalized;
        retreatPosition = transform.position + direction * 10f;

        Camera mainCamera = Camera.main;
        Vector2 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector2 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        float bufferX = spriteRenderer.bounds.size.x / 2;
        float bufferY = spriteRenderer.bounds.size.y / 2;

        if (transform.position.x - bufferX < screenBottomLeft.x + bufferX)
        {
            direction.x = Mathf.Abs(direction.x); 
        }
        else if (transform.position.x + bufferX > screenTopRight.x - bufferX)
        {
            direction.x = -Mathf.Abs(direction.x); 
        }

        if (transform.position.y - bufferY < screenBottomLeft.y + bufferY)
        {
            direction.y = Mathf.Abs(direction.y); 
        }
        else if (transform.position.y + bufferY > screenTopRight.y - bufferY)
        {
            direction.y = -Mathf.Abs(direction.y); 
        }

        retreatPosition = transform.position + direction * 20f;

        retreatPosition.x = Mathf.Clamp(retreatPosition.x, screenBottomLeft.x + bufferX, screenTopRight.x - bufferX);
        retreatPosition.y = Mathf.Clamp(retreatPosition.y, screenBottomLeft.y + bufferY, screenTopRight.y - bufferY);

        while (Time.time - startTime < 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, retreatPosition, retreatSpeed * Time.deltaTime);
            yield return null;
        }

        transform.localScale = Vector3.one;
        spriteRenderer.color = originalColor;
    }

    private void PushPlayerAway()
    {
        if (playerTransform != null)
        {
            Vector3 pushDirection = (playerTransform.position - transform.position).normalized;
            float pushForce = -10f; 
            playerTransform.GetComponent<Rigidbody2D>().AddForce(-pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            collision.GetComponent<PlayerMovement>().takeDamage(3);

            PushPlayerAway();
        }
    }

    public IEnumerator die()
    {
        capsuleCollider.enabled = false;
        transform.localScale = new Vector3(1.5f, 1.5f, 0);
        dead = true;
        animator.SetBool("Dying", true);

        bossHealthBar.SetActive(false);
        victoryScreen.SetActive(true);

        _GameManager.savePrefInt("HighestBossDefeated", bossNumber);

        yield return new WaitForSeconds(deathTime);

        SceneManager.LoadScene("TavernScene");
    }

}
