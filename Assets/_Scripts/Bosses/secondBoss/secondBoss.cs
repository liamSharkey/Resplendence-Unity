using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class secondBoss : Boss
{
    private bool moving = false; // Indicates whether the boss is currently moving.
    private Vector3 newPosition; // The new position the boss is moving towards.
    private float withinRange = 2; // The range within which the boss considers itself to have reached its destination.

    // Start is called before the first frame update.
    void Start()
    {
        bossNumber = 2; // Identifier for the boss.
        movementSpeed = 7; // Speed at which the boss moves.
        fireTime = 0.5f; // Time interval between firing projectiles.
        moveTime = 2f; // Time interval between moving to a new position.
        maxHealth = 400; // Maximum health of the boss.
        UniversalStart(); // Calls the start function from the parent Boss class.
    }

    // Update is called once per frame.
    void Update()
    {
        if (Time.time - lastFired > fireTime && !dead)
        {
            fire();
        }

        if (Time.time - lastMoved > moveTime)
        {
            lastMoved = Time.time;
            moving = true;
            newPosition = !inPhaseTwo ?
                new Vector3(UnityEngine.Random.Range(-13f, 13f), UnityEngine.Random.Range(8f, 20f), 0) :
                new Vector3(UnityEngine.Random.Range(playerTransform.position.x - 3f, playerTransform.position.x + 3f), UnityEngine.Random.Range(playerTransform.position.y - 3, playerTransform.position.y + 3f), 0);
        }

        if (moving)
        {
            moveTowards(newPosition);
        }

        if (health <= (maxHealth / 2) && !inPhaseTwo)
        {
            movementSpeed += 1;
            moveTime -= 1f;
            fireTime = fireTime / 2;
            inPhaseTwo = true;
            transform.localScale += new Vector3(1, 1, 0);
        }

        if (inPhaseTwo)
        {
            // Random size changes
            if (UnityEngine.Random.Range(0, 100) < 5) // 5% chance to change size every frame
            {
                float newSize = UnityEngine.Random.Range(0.2f, 1.8f); // Random size between 0.8 and 1.2 times original size
                transform.localScale = new Vector3(newSize, newSize, 1);
            }
        }

        if (health <= 0 && !dead)
        {
            StartCoroutine(die());
        }
    }

    // Fires a projectile.
    void fire()
    {
        Instantiate(bossProjectile, transform.position, Quaternion.identity); // Instantiates a projectile prefab.
        lastFired = Time.time; // Updates the last fired time.
    }

    // Moves the boss towards the new position.
    void moveTowards(Vector3 newPosition)
    {
        if (dead) return; // If the boss is dead, don't move.

        // Checks if the boss is within range of the new position.
        if (Mathf.Abs(newPosition.x - transform.position.x) < withinRange && Mathf.Abs(newPosition.y - transform.position.y) < withinRange)
        {
            moving = false; // Stops moving if the boss has reached the new position.
        }

        // Determines the direction to move in.
        float xMovementSign = newPosition.x > transform.position.x ? 1 : -1;
        float yMovementSign = newPosition.y > transform.position.y ? 1 : -1;

        // Stops moving in a direction if the boss is close enough to the new position in that direction.
        if (Mathf.Abs(newPosition.x - transform.position.x) < withinRange) xMovementSign = 0;
        if (Mathf.Abs(newPosition.y - transform.position.y) < withinRange) yMovementSign = 0;

        // Moves the boss towards the new position.
        transform.Translate(new Vector3(Time.deltaTime * xMovementSign * movementSpeed, Time.deltaTime * yMovementSign * movementSpeed, 0));
    }

    // Coroutine for handling the boss's death.
    public IEnumerator die()
    {
        capsuleCollider.enabled = false; // Disables the collider.
        transform.localScale = new Vector3(1.5f, 1.5f, 0); // Enlarges the boss's sprite.
        dead = true; // Marks the boss as dead.
        animator.SetBool("Dying", true); // Triggers the dying animation.

        bossHealthBar.SetActive(false); // Hides the boss's health bar.
        victoryScreen.SetActive(true); // Shows the victory screen.

        // Saves the highest defeated boss number.
        _GameManager.savePrefInt("HighestBossDefeated", bossNumber);

        yield return new WaitForSeconds(deathTime); // Waits for the death animation to finish.

        SceneManager.LoadScene("TavernScene"); // Loads the TavernScene.
    }
}
