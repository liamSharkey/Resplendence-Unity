using System.Collections;
using UnityEngine;

public class SeventhBossProjectile : MonoBehaviour
{
    public float movementSpeed = -15;
    public float lifespan = 2f;

    private float aliveTime;
    private Transform playerTransform;
    private Vector3 targetPosition;

    void Start()
    {
        aliveTime = Time.time;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (playerTransform != null)
        {
            float xOffset = Random.Range(-1f, 1f);
            float yOffset = Random.Range(-1f, 1f);
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
            Destroy(gameObject);
        }
        else if (collision.gameObject.name != "Boss")
        {
            Destroy(gameObject);
        }
    }
}
