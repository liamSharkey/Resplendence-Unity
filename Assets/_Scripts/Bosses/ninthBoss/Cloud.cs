using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float movementSpeed = 1f; // Speed of movement towards the player
    public float jiggleAmount = 0.1f; // Maximum amount of jiggle

    public bool popped = false;

    private Transform playerTransform; // Reference to the player's transform



    void Start()
    {
        // Find the player object and get its transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Cloud: Player not found!");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            MoveTowardsPlayer();
            ApplyJiggle();
        }

         Collider2D[] results = new Collider2D[5]; // Adjust the array size based on your needs
        int numColliders = Physics2D.OverlapCollider(GetComponent<Collider2D>(), new ContactFilter2D().NoFilter(), results);

        // Iterate through the colliders detected in the overlap
        for (int i = 0; i < numColliders; i++)
        {
            Collider2D otherCollider = results[i];

            // Check if the other collider is not the trigger collider itself
            if (otherCollider != GetComponent<Collider2D>() && otherCollider.GetComponent<Rigidbody2D>()== null)
            {
                if(otherCollider.tag=="PlayerProjectile"){
                    Destroy(otherCollider.gameObject);
                }
                    Pop();
               
            }
        }
        
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction towards the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Move the cloud towards the player
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    void ApplyJiggle()
    {
        // Calculate random jiggle
        float jiggleX = Random.Range(-jiggleAmount, jiggleAmount);
        float jiggleY = Random.Range(-jiggleAmount, jiggleAmount);

        // Apply the jiggle
        transform.Translate(new Vector3(jiggleX, jiggleY, 0f));
    }

    void Pop(){
        
        popped = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().takeDamage(15);
            Pop();
        }
        if (collision.gameObject.name != "Boss")
        {
            Pop();
        }

    }
}