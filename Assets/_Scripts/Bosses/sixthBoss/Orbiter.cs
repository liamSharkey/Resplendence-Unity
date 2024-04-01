using UnityEngine;

public class Orbiter : MonoBehaviour
{
    public float speed = 2f; // Speed of movement
    public float rotationSpeed = 7f; // Speed of rotation
    public float minRadius = 0.5f; // Minimum radius before destruction

    private Vector3 centerPosition; // Center position around which the projectile rotates
    private float initialDistance; // Initial distance from the center point
    private float currentRadius; // Current radius of the circular path
    private Vector3 initialPosition; // Initial position of the orbiter before orbiting
    private bool isMovingToInitialPosition = true; // Flag to indicate if the orbiter is moving to its initial position
    private float angle = Mathf.PI / 2f; // Initial angle for rotation

    // Start is called before the first frame update
    void Start()
    {
        // Get the initial position of the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            centerPosition = player.transform.position;
            initialDistance = Vector3.Distance(transform.position, centerPosition);
            currentRadius = initialDistance;
            initialPosition = new Vector3(centerPosition.x, centerPosition.y + initialDistance, centerPosition.z);
            
            // Randomly decide if the rotation speed should be negative
            if (Random.value < 0.5f)
            {
                rotationSpeed *= -1f; // Make rotation speed negative
            }
        }
        else
        {
            Debug.LogError("Orbiter: Player not found!");
            Destroy(gameObject); // Destroy the projectile if player is not found
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the orbiter is moving to its initial position, move it gradually
        if (isMovingToInitialPosition)
        {
            MoveToInitialPosition();
        }
        else
        {
            // Otherwise, start orbiting
            UpdatePosition();
        }
    }

    // Move the orbiter to its initial position
    private void MoveToInitialPosition()
    {
        // Calculate the direction towards the initial position
        Vector3 direction = (initialPosition - transform.position).normalized;

        // Move the orbiter towards the initial position
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the orbiter has reached the initial position
        if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
        {
            isMovingToInitialPosition = false; // Stop moving to initial position
        }
    }

    // Update the position of the projectile
    void UpdatePosition()
    {
        // Calculate the new position of the projectile based on the angle and radius
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * currentRadius;
        transform.position = centerPosition + offset;

        // Update the angle based on the rotation speed
        angle += Time.deltaTime * rotationSpeed;

        // Decrease the radius over time
        currentRadius -= Time.deltaTime * 2;

        // If the radius becomes less than the minimum radius, destroy the projectile
        if (currentRadius <= minRadius)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().takeDamage(5);
        }
        if (collision.gameObject.name != "Boss")
        {
            Destroy(gameObject);
        }
    }
}