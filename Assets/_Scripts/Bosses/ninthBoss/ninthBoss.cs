using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ninthBoss : Boss
{
    private bool moving = false;  
    private Vector3 newPosition;

    private float withinRange = 2;  

    private Vector3 MiddleRight = new Vector3 (10, 14, 0);

    private Vector3 MiddleLeft = new Vector3 (-10, 14, 0);

    public GameObject Spawner;

    private int shots = 25;
    
    void Start()
    {
        bossNumber = 9;
        UniversalStart();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time - lastFired > fireTime && !dead)
        {
            lastFired=Time.time;
            fire();
        } 

        if (Time.time - lastMoved > moveTime && !dead)
        {
            lastMoved = Time.time;
            moving = true;
            if (!inPhaseTwo)
            {
                newPosition = new Vector3(UnityEngine.Random.Range(-13f, 13f), UnityEngine.Random.Range(8f, 20f), 0);
            }
            else
            {
                newPosition = new Vector3(UnityEngine.Random.Range(playerTransform.position.x - 3f, playerTransform.position.x + 3f), UnityEngine.Random.Range(playerTransform.position.y -3, playerTransform.position.y + 3f), 0);
            }
        }

        if (moving && !dead)
        {
            moveTowards(newPosition);
        }

        if (health <= (maxHealth/2) && !inPhaseTwo)
        {
            movementSpeed += 1;
            moveTime -= 0.1f;
            fireTime = fireTime / 1.5f;
            inPhaseTwo = true;
            transform.localScale += new Vector3(1, 1, 0);
            Instantiate(Spawner, MiddleLeft, Quaternion.identity);
            Instantiate(Spawner, MiddleRight, Quaternion.identity);
            shots = 35;
        }

        if (health <= 0 && ! dead)
        {
            dead = true;
            StopAllCoroutines();
            GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            foreach (GameObject p in projectiles)
            {
                Destroy(p);
            }
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
            foreach (GameObject s in spawners)
            {
                Destroy(s);
            }
            StartCoroutine(die());
        }
    }

    void fire()
{
    for (int i = 0; i < shots; i++)
    {
        // Calculate random offset for position
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);

        // Calculate direction from boss to player
        Vector3 directionToPlayer = (playerTransform.position - (transform.position + randomOffset)).normalized;

        // Calculate rotation angle
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        // Adjust rotation to point bottom towards player
        angle += 90f;

        // Instantiate projectile with rotation and random position
        GameObject projectile = Instantiate(bossProjectile, transform.position + randomOffset, Quaternion.Euler(0f, 0f, angle));

        // Update last fired time
        lastFired = Time.time;
    }
}


    private float xMovementSign;
    private float yMovementSign;
    void moveTowards(Vector3 newPosition)
    {
        if (dead) { return; }
        if (Mathf.Abs(newPosition.x - transform.position.x) < withinRange && Mathf.Abs(newPosition.y - transform.position.y) < withinRange)
        {
            moving = false;
        }

        xMovementSign = 0;
        yMovementSign = 0;

        if (transform.position.x < newPosition.x)
        {
            xMovementSign = 1;
        }
        else
        {
            xMovementSign = -1;
        }
        if (transform.position.y < newPosition.y)
        {
            yMovementSign = 1;
        }
        else
        {
            yMovementSign = -1;
        }

        if (Mathf.Abs(newPosition.x - transform.position.x) < withinRange)
        {
            xMovementSign = 0;
        }
        if (Mathf.Abs(newPosition.y - transform.position.y) < withinRange)
        {
            yMovementSign = 0;
        }

        transform.Translate(new Vector3(Time.deltaTime * xMovementSign * movementSpeed, Time.deltaTime * yMovementSign * movementSpeed, 0));

    }

    public IEnumerator die()
    {


        capsuleCollider.enabled = false;
        transform.localScale = new Vector3(1.5f, 1.5f, 0);
        dead = true;
        animator.SetBool("Dying", true);

        bossHealthBar.SetActive(false);
        victoryScreen.SetActive(true);

        // setting highest defeated boss to be 1
        _GameManager.savePrefInt("HighestBossDefeated", bossNumber);

        yield return new WaitForSeconds(deathTime);

        SceneManager.LoadScene("TavernScene");
    }
}