using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class thirdBoss : Boss
{
    private bool moving = false;
    private Vector3 newPosition;
    private int initialStatuesSpawned = 0; // Counter for spawned statues
    private bool bossVisible = false;

    public GameObject statuePrefab;
    private GameObject currentStatue;

    // Start is called before the first frame update
    void Start()
    {
        bossNumber = 3;
        movementSpeed = 7;
        fireTime = 1f;
        moveTime = 4f;
        maxHealth = 400;
        UniversalStart();

        // Instantiate the first statue prefab
        InstantiateStatue();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossVisible)
            return;

        if (Time.time - lastFired > fireTime && !dead)
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
                newPosition = new Vector3(UnityEngine.Random.Range(playerTransform.position.x - 3f, playerTransform.position.x + 3f), UnityEngine.Random.Range(playerTransform.position.y - 3, playerTransform.position.y + 3f), 0);
            }
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

    void InstantiateStatue()
    {
        // Spawn a new statue only if the total number spawned is less than 3
        if (initialStatuesSpawned < 3)
        {
            if (currentStatue != null)
                Destroy(currentStatue);

            currentStatue = Instantiate(statuePrefab, transform.position, Quaternion.identity);
            initialStatuesSpawned++; // Increment the counter
            bossVisible = false;
            capsuleCollider.enabled = false;

            // Subscribe to the statue's remove event
            currentStatue.GetComponent<Statue>().OnStatueRemoved += InitialStatueDestroyed;

        }
    }

    public void InitialStatueDestroyed()
    {
        if (initialStatuesSpawned < 3)
        {
            InstantiateStatue();
        }
        else
        {
            // If already spawned 3 statues, set boss visible and enable collider
            bossVisible = true;
            capsuleCollider.enabled = true;
        }
    }
}