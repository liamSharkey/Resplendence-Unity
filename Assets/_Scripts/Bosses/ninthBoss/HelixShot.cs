using UnityEngine;

public class HelixShot : MonoBehaviour
{
    public float movementSpeed = 6f;
    public float rotationSpeed = 100f;
    public float lifespan = 2f;
    public int damageAmount = 5;

    private float creationTime;
    private Vector3 initialForwardDirection;

    public float frequency;
    public float amplitude;

    private float timer = 0f;

    void Start()
    {
        creationTime = Time.time;
        initialForwardDirection = -transform.up; // Store the initial forward direction
        transform.eulerAngles += new Vector3(0, 0, Random.Range(-5, 5));

        // 50/50 chance to invert the amplitude
        if (Random.value < 0.5f)
        {
            amplitude = -amplitude;
        }
    }

    void Update()
    {
        // Increment the timer every frame
        timer += Time.deltaTime;

        // Check if one second has passed
        if (timer >= 0.25f)
        {
            // Reset the timer
            timer = 0f;

            // 50/50 chance to increase amplitude and decrease frequency
            
            if (Random.value < 0.1f && amplitude < 0.0175)
            {
                amplitude += 0.0025f;
                frequency -= 1f;
            }
            
        }
        MoveProjectile();
        CheckLifespan();
    }

    void MoveProjectile()
    {
        // Calculate the side-to-side oscillation using a sine wave pattern
        float xOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        // Move the projectile based on its initial forward direction
        transform.Translate(initialForwardDirection * (movementSpeed * Time.deltaTime), Space.World);

        // Move the projectile side to side along its local x-axis
        transform.Translate(transform.right * xOffset, Space.World);
    }

    void CheckLifespan()
    {
        // Destroy the projectile after its lifespan
        if (Time.time - creationTime > lifespan)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Damage the player upon collision
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().takeDamage(damageAmount);
        }

        // Destroy the projectile upon collision with anything other than the boss
        if (!collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
