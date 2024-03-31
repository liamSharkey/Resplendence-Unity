using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class firstBoss : Boss
{  
    private bool moving = false;
    private Vector3 newPosition;

    private float withinRange = 2;

    // Start is called before the first frame update
    void Start()
    {
        bossNumber = 1;
        UniversalStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastFired > fireTime && ! dead)
        {
            fire();
        } 

        if (Time.time - lastMoved > moveTime)
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

        if (moving)
        {
            moveTowards(newPosition);
        }

        if (health <= (maxHealth/2) && !(inPhaseTwo))
        {
            movementSpeed += 1;
            moveTime -= 0.1f;
            fireTime = fireTime / 2;
            inPhaseTwo = true;
            transform.localScale += new Vector3(1, 1, 0);
        }

        if (health <= 0 && ! dead)
        {
            StartCoroutine(die());
        }

    }

    void fire()
    {
        Instantiate(bossProjectile, transform.position, Quaternion.identity);
        lastFired = Time.time;
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
