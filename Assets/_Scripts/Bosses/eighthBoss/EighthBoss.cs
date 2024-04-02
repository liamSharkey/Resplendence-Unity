using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EighthBoss : Boss
{  
    private bool moving = false;
    private Vector3 newPosition;
    public GameObject illusionPrefab;

    private float lastIllusionSpawnedTime = 0f;
    private float illusionSpawnInterval = 3f;
    private float withinRange = 2;

    private float shootCooldownTime = 2f;
    private float lastShootTime = 0f;
    private bool isOnCooldown = false;

    void Start()
    {
        bossNumber = 8;
        UniversalStart();
    }

    void SpawnIllusion()
    {
        int numberOfIllusions = inPhaseTwo ? 2 : 1;
        for (int i = 0; i < numberOfIllusions; i++)
        {
            Camera mainCamera = Camera.main;
            Vector2 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector2 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

            float minDistanceFromPlayer = 5f; 
            Vector3 spawnPosition;

            do
            {
                float spawnX = UnityEngine.Random.Range(screenBottomLeft.x, screenTopRight.x);
                float spawnY = UnityEngine.Random.Range(screenBottomLeft.y, screenTopRight.y);
                spawnPosition = new Vector3(spawnX, spawnY, 0);
            }
            while (Vector3.Distance(spawnPosition, playerTransform.position) < minDistanceFromPlayer);

            Instantiate(illusionPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void Update()
    {
        if (Time.time - lastIllusionSpawnedTime > illusionSpawnInterval && !dead)
        {
            lastIllusionSpawnedTime = Time.time;
            SpawnIllusion();
        }

        if (Time.time - lastFired > fireTime && !dead && !isOnCooldown)
        {
            fire();
        }

        if (isOnCooldown && Time.time - lastShootTime > shootCooldownTime)
        {
            isOnCooldown = false;
        }

        if (!isOnCooldown && Time.time - lastMoved > moveTime)
        {
            lastMoved = Time.time;
            moving = true;
            if (!inPhaseTwo)
            {
                newPosition = new Vector3(UnityEngine.Random.Range(-13f, 13f), UnityEngine.Random.Range(8f, 20f), 0);
            }
            else
            {
                newPosition = new Vector3(UnityEngine.Random.Range(playerTransform.position.x - 1f, playerTransform.position.x + 1f), UnityEngine.Random.Range(playerTransform.position.y - 1, playerTransform.position.y + 1f), 0);
            }
        }

        if (moving)
        {
            moveTowards(newPosition);
        }

        if (health <= (maxHealth / 2) && !inPhaseTwo)
        {
            movementSpeed += 1;
            moveTime -= 0.1f;
            fireTime = fireTime / 2;
            inPhaseTwo = true;
            transform.localScale += new Vector3(1, 1, 0);
        }

        if (health <= 0 && !dead)
        {
            StartCoroutine(die());
        }
    }


    void fire()
    {
        Instantiate(bossProjectile, transform.position, Quaternion.identity);
        lastFired = Time.time;
        isOnCooldown = true;
        lastShootTime = Time.time;
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
