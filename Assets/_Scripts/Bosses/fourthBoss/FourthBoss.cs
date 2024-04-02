using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FourthBoss : Boss
{
    public float chargeSpeed = 10f;
    public float retreatSpeed = 12f;
    public float pauseDuration = 2f;
    public int shotsFired = 5;
    public float fireRate = 0.5f;

    private Vector3 retreatPosition;

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        playerTransform = playerObject.transform;
        StartCoroutine(BehaviorRoutine());
        bossNumber = 4; 
        movementSpeed = 7; 
        fireTime = 0.5f;
        moveTime = 2f; 
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
        yield return new WaitForSeconds(3f);

        while (!dead)
        { 
            if (!inPhaseTwo)
            {
                StartCoroutine(ChargeTowardsPlayer());

                yield return new WaitForSeconds(1f); 
                for (int i = 0; i < shotsFired; i++)
                {
                    FireProjectile();
                    yield return new WaitForSeconds(fireRate);
                }

                // Run away from the player
                StartCoroutine(RetreatFromPlayer());

                // Pause
                yield return new WaitForSeconds(pauseDuration);
            }
            else
            {
                for (int j = 0; j < 2; j++)
                {
                    StartCoroutine(ChargeTowardsPlayer());
                    yield return new WaitForSeconds(1f); 
                    for (int i = 0; i < shotsFired; i++)
                    {
                        FireProjectile();
                        yield return new WaitForSeconds(fireRate / 2); 
                    }
                }

                StartCoroutine(RetreatFromPlayer());
                yield return new WaitForSeconds(pauseDuration);
            }
        }
    }

    private IEnumerator ChargeTowardsPlayer()
    {
        if (dead) { yield return null; }
        float startTime = Time.time;
        transform.localScale = new Vector3(1.5f, 1.5f, 1);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        Color chargeColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.95f);
        spriteRenderer.color = chargeColor;

        float chargeDuration = inPhaseTwo ? 0.75f : 1f;

        while (Time.time - startTime < chargeDuration)
        {
            if (playerTransform != null)
            {
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                transform.Translate(direction * chargeSpeed * Time.deltaTime);
            }
            yield return null;
        }

        spriteRenderer.color = originalColor;
    }


    private IEnumerator RetreatFromPlayer()
    {
        if (dead) { yield return null; }
        float startTime = Time.time;
        transform.localScale = new Vector3(0.75f, 0.75f, 1);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.1f);

        spriteRenderer.color = transparentColor;

        Vector3 direction = (transform.position - playerTransform.position).normalized;
        retreatPosition = transform.position + direction * 5f;

        Camera mainCamera = Camera.main;
        Vector2 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector2 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        float bufferX = spriteRenderer.bounds.size.x / 2;
        float bufferY = spriteRenderer.bounds.size.y / 2;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (dead) { return; }
            collision.GetComponent<PlayerMovement>().takeDamage(6);
        }
    }

    private void FireProjectile()
    {
        if (dead) { return; }
        Instantiate(bossProjectile, transform.position, Quaternion.identity);
        lastFired = Time.time;
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
