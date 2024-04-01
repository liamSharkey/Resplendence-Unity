using System.Collections;
using UnityEngine;

public class FourthBossProjectile : MonoBehaviour
{
    public float movementSpeed = -6;
    public float lifespan = 2f;
    public float reverseDuration = 5f;

    private float aliveTime;
    private Transform playerTransform;
    private Vector3 targetPosition;

    void Start()
    {
        aliveTime = Time.time;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (playerTransform != null)
        {
            float xOffset = Random.Range(-3f, 3f);
            float yOffset = Random.Range(-3f, 3f);
            targetPosition = playerTransform.position + new Vector3(xOffset, yOffset, 0);
        }

        Vector3 direction = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.Translate(Vector3.right * Mathf.Abs(movementSpeed) * Time.deltaTime);

        if (Time.time - aliveTime > lifespan)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().takeDamage(5);
            ReverseMovement reverseMovement = collision.gameObject.GetComponent<ReverseMovement>();
            if (reverseMovement != null)
            {
                reverseMovement.ActivateReverse(reverseDuration);
            }
            Destroy(gameObject); 
        }
        else if (collision.gameObject.name != "Boss")
        {
            Destroy(gameObject);
        }
    }
}
