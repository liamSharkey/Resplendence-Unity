using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sixthBoss : Boss
{
    public GameObject LifeStealPrefab;
    public GameObject OrbiterPrefab;

    // Rates for orbiter instantiation
    private bool moving = false;
    private Vector3 newPosition;

    private float withinRange = 2;

    private int amount = 3;
    private int Gap = 2;

    private float lifeStealInterval = 28f;

    // Start is called before the first frame update
    void Start()
    {
        bossNumber = 6;
        UniversalStart();
        Instantiate(LifeStealPrefab, transform.position, Quaternion.identity);
        StartCoroutine(SpawnLifeSteals());
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
            amount = 4;
            Gap = 1;
            lifeStealInterval = 25;
            movementSpeed += 1;
            moveTime -= 0.1f;
            fireTime = fireTime / 1.5f;
            inPhaseTwo = true;
            transform.localScale += new Vector3(1, 1, 0);
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
            StartCoroutine(die());
        }
    }

    IEnumerator SpawnLifeSteals()
    {
        while (!dead)
        {
            yield return new WaitForSeconds(lifeStealInterval);
            Instantiate(LifeStealPrefab, transform.position, Quaternion.identity);
        }
    }

    IEnumerator FireOrbiters()
{
    for (int i = 0; i < amount; i++)
    {
        Instantiate(OrbiterPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(Gap); // Gap of 2 seconds
    }
}

void fire()
{
    StartCoroutine(FireOrbiters());
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

        _GameManager.savePrefInt("HighestBossDefeated", bossNumber);

        yield return new WaitForSeconds(deathTime);

        SceneManager.LoadScene("TavernScene");
    }
    

}