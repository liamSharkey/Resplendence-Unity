using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EighthBossProjectile : MonoBehaviour
{
    public Sprite[] projectileSprites;
    public float movementSpeed = -15;
    public float lifespan = 5f;

    private Vector3 initialPosition;
    private float[] circlingRadii = { 1f, 2f };
    private int currentCirclingPhase = 0;
    private float circleTimer = 0f;
    private bool isTargetingPlayer = false;
    private float aliveTime;

    private float targetingDelay = 0.5f;

    void Start()
    {
        aliveTime = Time.time;
        initialPosition = transform.position;
        UpdateSprite();
    }

    void Update()
        {
            circleTimer += Time.deltaTime;
            float circlingDuration = lifespan / circlingRadii.Length;

            if (circleTimer < circlingDuration * (currentCirclingPhase + 1))
            {
                // Circular movement
                float angle = (circleTimer - circlingDuration * currentCirclingPhase) / circlingDuration * 2 * Mathf.PI;
                float currentRadius = circlingRadii[currentCirclingPhase];
                Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * currentRadius;
                transform.position = initialPosition + offset;
            }
            else if (currentCirclingPhase < circlingRadii.Length - 1)
            {
                // Move to the next circling phase
                currentCirclingPhase++;
                UpdateSprite();
            }
            else
            {
            if (isTargetingPlayer != true)
            {
                TargetPlayer();
                transform.Translate(Vector3.right * Mathf.Abs(movementSpeed) * Time.deltaTime);
                isTargetingPlayer = true;
            }
            else
            {
                transform.Translate(Vector3.right * Mathf.Abs(movementSpeed) * Time.deltaTime);
            }
            }
    }

    void UpdateSprite()
    {
        if (projectileSprites.Length > currentCirclingPhase)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = projectileSprites[currentCirclingPhase];
        }
    }

    void TargetPlayer()
    {
        aliveTime = Time.time; // Reset the aliveTime to start the 3-second countdown

        if (projectileSprites.Length > 2)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = projectileSprites[2];
        }

        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform != null)
        {
            float xOffset = Random.Range(-2f, 2f);
            float yOffset = Random.Range(-2f, 2f);
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().takeDamage(5);
            Destroy(gameObject);
        }
        else if (!collision.gameObject.CompareTag("Illusion") && !collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
