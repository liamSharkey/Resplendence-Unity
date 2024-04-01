using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSteal : MonoBehaviour
{
    public float followSpeed = 1.5f; // Speed of following the player
    public float followDelay = 1.5f; // Delay before starting to follow the player
    public float damageInterval = 1f; // Interval between each damage application
    public int damageAmount = 10; // Amount of damage to apply

    private Transform playerTransform; // Reference to the player's transform
    private bool canFollow = false; // Flag to indicate if the LifeSteal can start following
    private bool isPlayerInside = false; // Flag to indicate if the player is inside the trigger collider
    private float damageTimer = 0f; // Timer for tracking damage intervals

    private float aliveTime;
    public float lifespan = 25f;

    private GameObject player;
    private GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player's transform
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");

        playerTransform = player.transform;

        Invoke("EnableFollowing", followDelay);

        aliveTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // If canFollow is true, start following the player
        if (canFollow && playerTransform != null)
        {
            // Move towards the player's position smoothly
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, followSpeed * Time.deltaTime);
        }

        // If the player is inside the collider, apply damage in intervals
        if (isPlayerInside)
        {
            // Increment the damage timer
            damageTimer += Time.deltaTime;
            // If the damage timer exceeds the interval, apply damage and reset the timer
            if (damageTimer >= damageInterval)
            {
                damage();
                damageTimer = 0f; // Reset the timer
            }
        }

        if (Time.time - aliveTime > lifespan)
        {
            Destroy(gameObject);
        }
    }

    // Enable following after the delay
    private void EnableFollowing()
    {
        canFollow = true;
    }

    // Called when another collider enters the trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            damage();
            
        }
    }

    // Called when another collider exits the trigger collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            damageTimer = 0f; // Reset the damage timer when the player exits
        }
    }

    void damage(){
        player.GetComponent<PlayerMovement>().takeDamage(5);
        boss.GetComponent<sixthBoss>().health+=5;
    }
}