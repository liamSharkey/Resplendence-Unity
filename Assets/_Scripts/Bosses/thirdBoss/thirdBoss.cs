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

    private Vector3 position1 = new Vector3(0, 20, 0);
    private Vector3 position2 = new Vector3(-6, 17, 0);
    private Vector3 position3 = new Vector3(-6f, 11, 0);
    private Vector3 position4 = new Vector3(6f, 11, 0);
    private Vector3 position5 = new Vector3(6, 17, 0);
    private Vector3 position6 = new Vector3(0, 8, 0);
    private Vector3 center = new Vector3(0, 14, 0);

    private Vector3 furthestPosition;
    private Vector3[] statuePositions; // Positions for statues in phase 2
    private int statueCount = 6; // Number of statues to spawn in phase 2

    // Start is called before the first frame update
    void Start()
    {
        UniversalStart();

        // Instantiate the first statue prefab
        InstantiateStatue();
        furthestPosition = position1;

        // Initialize statue positions in phase 2
        statuePositions = CalculateCirclePositions(center, 6f, statueCount);
    }

    private int shots = 5;

    // Update is called once per frame
    void Update()
    {
        if (!bossVisible)
        {
            return;
        }
            

            if(dead){
                return;
            }

        
            // Regular behavior for phase 1
            if (Time.time - lastFired > fireTime && !moving)
            {
                StartCoroutine(FireRepeatedly(shots, 0.25f)); // Fire 5 times with 0.5 seconds delay
            }

            // Only move if not already moving
            if (!moving)
            {
                if (Time.time - lastMoved > moveTime)
                {
                    // Move the boss to the position furthest from the player
                    furthestPosition = FindFurthestPositionFromPlayer();
                    moving = true;
                }
            }
            if (moving)
            {
                MoveToPosition(furthestPosition);
            }
        

        if (health <= (maxHealth / 2) && !inPhaseTwo)
        {
            StartPhaseTwo();
        }

        if (health <= 0 && !dead)
        {
            StartCoroutine(die());
        }
    }

    Vector3 FindFurthestPositionFromPlayer()
    {
        // Calculate the distance between the player and each position
        float maxDistance = float.MinValue;
        Vector3 furthestPosition = Vector3.zero;

        Vector3[] positions = new Vector3[] { position1, position2, position3, position4, position5, position6 };
        foreach (Vector3 position in positions)
        {
            float distance = Vector3.Distance(playerTransform.position, position);
            if (distance > maxDistance)
            {
                // Check if the boss is not already within range of this position
                if (Vector3.Distance(transform.position, position) > 1f)
                {
                    maxDistance = distance;
                    furthestPosition = position;
                }
            }
        }

        return furthestPosition;
    }

    void MoveToPosition(Vector3 targetPosition)
    {
        // Calculate the distance remaining to the target position
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // If the distance is small enough, stop moving
        if (distanceToTarget < 0.1f)
        {
            lastMoved = Time.time;
            moving = false;
            return;
        }

        // Calculate the movement direction
        Vector3 movementDirection = (targetPosition - transform.position).normalized;

        // Move the boss smoothly towards the target position
        transform.position += movementDirection * movementSpeed * Time.deltaTime;
    }

    void StartPhaseTwo()
    {
        // Disable collider
        capsuleCollider.enabled = false;
        HandlePhaseTwo();

        // Start moving in a circle
        StartCoroutine(MoveInCircle(center, 6f));
    }

    IEnumerator MoveInCircle(Vector3 center, float radius)
{

    SpawnStatues(); // Spawn statues initially

    while (true)
    {
        // Calculate position on the circle using sine and cosine functions
        float x = center.x + Mathf.Cos(Time.time * movementSpeed) * radius;
        float y = center.y + Mathf.Sin(Time.time * movementSpeed) * radius;
        Vector3 targetPosition = new Vector3(x, y, 0);

        // Move towards target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

        yield return null;
    }
}

    void SpawnStatues()
    {
        // Spawn statues at evenly spaced positions around the circle
        for (int i = 0; i < statueCount; i++)
        {
            currentStatue = Instantiate(statuePrefab, statuePositions[i], Quaternion.identity);

            currentStatue.GetComponent<Statue>().OnStatueRemoved += SecondaryStatueDestroyed;
        }
    }

    Vector3[] CalculateCirclePositions(Vector3 center, float radius, int count)
    {
        // Calculate angle between each statue
        float angleStep = 360f / count;

        // Initialize array to hold positions
        Vector3[] positions = new Vector3[count];

        // Calculate positions around the circle
        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep;
            positions[i] = center + Quaternion.Euler(0, 0, angle) * Vector3.right * radius;
        }

        return positions;
    }

    void HandlePhaseTwo()
    {
        movementSpeed += 1;
        moveTime -= 0.1f;
        fireTime = fireTime / 2;
        inPhaseTwo = true;
        transform.localScale += new Vector3(1, 1, 0);
        shots = 7;
    }

    void fire()
    {
        // Calculate direction from boss to player
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // Calculate rotation angle
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        // Adjust rotation to point bottom towards player
        angle += 90f;

        // Instantiate projectile with rotation
        GameObject projectile = Instantiate(bossProjectile, transform.position, Quaternion.Euler(0f, 0f, angle));

        // Update last fired time
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

    private int statues = 6;

    public void SecondaryStatueDestroyed(){
        statues --;
        if( statues == 0){
            capsuleCollider.enabled = true;
        }
    }

    IEnumerator FireRepeatedly(int numShots, float delayBetweenShots)
    {
        for (int i = 0; i < numShots; i++)
        {
            fire(); // Call the fire method
            yield return new WaitForSeconds(delayBetweenShots); // Wait for the specified delay
        }

        lastFired = Time.time; // Update last fired time
    }
}