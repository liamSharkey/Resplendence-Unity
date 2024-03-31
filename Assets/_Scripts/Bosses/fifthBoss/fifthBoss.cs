using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fifthBoss : Boss
{
    public Sprite FacingUp;
    public Sprite FacingRight;
    public Sprite FacingLeft;
    public Sprite FacingDown;

    public SpriteRenderer spriteRenderer;

    public GameObject Flare;

    private Vector3 TopLeft = new Vector3 (-6, 20, 0);
    private Vector3 TopMiddle = new Vector3 (0, 20, 0);
    private Vector3 TopRight = new Vector3 (6, 20, 0);
    private Vector3 MiddleLeft = new Vector3 (-6, 14, 0);
    private Vector3 MiddleMiddle = new Vector3 (0, 14, 0);
    private Vector3 MiddleRight = new Vector3 (6, 14, 0);
    private Vector3 BottomLeft = new Vector3 (-6, 8, 0);
    private Vector3 BottomMiddle = new Vector3 (0, 8, 0);
    private Vector3 BottomRight = new Vector3 (6, 8, 0);

    // Start is called before the first frame update
    void Start()
    {
        bossNumber = 5;
        UniversalStart();
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!inPhaseTwo && health <= maxHealth/2){
            inPhaseTwo = true;
        }
        if(health <= 0 && !dead){
            dead = true;
            StopAllCoroutines();
            StartCoroutine(die());
        }
        // Check if playerTransform is not null and if the boss is not dead
        if (playerTransform != null && !dead)
        {
            // Calculate direction from boss to player
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // Determine which direction the boss should face based on the direction vector
            if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
            {
                // Face left or right
                if (directionToPlayer.x > 0)
                    spriteRenderer.sprite = FacingLeft;
                else
                    spriteRenderer.sprite =  FacingRight;
            }
            else
            {
                // Face up or down
                if (directionToPlayer.y > 0)
                    spriteRenderer.sprite = FacingDown;
                else
                    spriteRenderer.sprite = FacingUp;
            }

        }

        if (Time.time - lastFired > fireTime && !dead)
        {
            fire();
        } 
    }

    public IEnumerator die()
    {


        capsuleCollider.enabled = false;
        transform.localScale = new Vector3(1.5f, 1.5f, 0);
        dead = true;
        animator.enabled=true;
        animator.SetBool("Dying", true);

        bossHealthBar.SetActive(false);
        victoryScreen.SetActive(true);

        // setting highest defeated boss to be 5
        _GameManager.savePrefInt("HighestBossDefeated", bossNumber);

        yield return new WaitForSeconds(deathTime);

        SceneManager.LoadScene("TavernScene");
    }

    void Move1()
{
    List<Vector3> movePositions = new List<Vector3>();

    // Check each predefined position
    if (Mathf.Approximately(transform.position.x, TopLeft.x) || Mathf.Approximately(transform.position.y, TopLeft.y))
        movePositions.Add(TopLeft);
    if (Mathf.Approximately(transform.position.x, TopMiddle.x) || Mathf.Approximately(transform.position.y, TopMiddle.y))
        movePositions.Add(TopMiddle);
    if (Mathf.Approximately(transform.position.x, TopRight.x) || Mathf.Approximately(transform.position.y, TopRight.y))
        movePositions.Add(TopRight);
    if (Mathf.Approximately(transform.position.x, MiddleLeft.x) || Mathf.Approximately(transform.position.y, MiddleLeft.y))
        movePositions.Add(MiddleLeft);
    if (Mathf.Approximately(transform.position.x, MiddleMiddle.x) || Mathf.Approximately(transform.position.y, MiddleMiddle.y))
        movePositions.Add(MiddleMiddle);
    if (Mathf.Approximately(transform.position.x, MiddleRight.x) || Mathf.Approximately(transform.position.y, MiddleRight.y))
        movePositions.Add(MiddleRight);
    if (Mathf.Approximately(transform.position.x, BottomLeft.x) || Mathf.Approximately(transform.position.y, BottomLeft.y))
        movePositions.Add(BottomLeft);
    if (Mathf.Approximately(transform.position.x, BottomMiddle.x) || Mathf.Approximately(transform.position.y, BottomMiddle.y))
        movePositions.Add(BottomMiddle);
    if (Mathf.Approximately(transform.position.x, BottomRight.x) || Mathf.Approximately(transform.position.y, BottomRight.y))
        movePositions.Add(BottomRight);

    // If there are no valid positions, return
    if (movePositions.Count == 0)
        return;

    // Remove current position from valid positions
    movePositions.Remove(transform.position);

    // Choose a random position from the filtered list
    Vector3 newPosition = movePositions[Random.Range(0, movePositions.Count)];

    // Move the boss instantly to the new position
    transform.position = newPosition;
}

void Move2()
{
    List<Vector3> movePositions = new List<Vector3>();

    // Check each predefined position
    if (Mathf.Approximately(transform.position.x, TopLeft.x) || Mathf.Approximately(transform.position.y, TopLeft.y))
        movePositions.Add(TopLeft);
    if (Mathf.Approximately(transform.position.x, TopMiddle.x) || Mathf.Approximately(transform.position.y, TopMiddle.y))
        movePositions.Add(TopMiddle);
    if (Mathf.Approximately(transform.position.x, TopRight.x) || Mathf.Approximately(transform.position.y, TopRight.y))
        movePositions.Add(TopRight);
    if (Mathf.Approximately(transform.position.x, MiddleLeft.x) || Mathf.Approximately(transform.position.y, MiddleLeft.y))
        movePositions.Add(MiddleLeft);
    if (Mathf.Approximately(transform.position.x, MiddleMiddle.x) || Mathf.Approximately(transform.position.y, MiddleMiddle.y))
        movePositions.Add(MiddleMiddle);
    if (Mathf.Approximately(transform.position.x, MiddleRight.x) || Mathf.Approximately(transform.position.y, MiddleRight.y))
        movePositions.Add(MiddleRight);
    if (Mathf.Approximately(transform.position.x, BottomLeft.x) || Mathf.Approximately(transform.position.y, BottomLeft.y))
        movePositions.Add(BottomLeft);
    if (Mathf.Approximately(transform.position.x, BottomMiddle.x) || Mathf.Approximately(transform.position.y, BottomMiddle.y))
        movePositions.Add(BottomMiddle);
    if (Mathf.Approximately(transform.position.x, BottomRight.x) || Mathf.Approximately(transform.position.y, BottomRight.y))
        movePositions.Add(BottomRight);

    // Remove current position from valid positions
    movePositions.Remove(transform.position);

    // If there are no valid positions, return
    if (movePositions.Count == 0)
        return;

    foreach (var pos in movePositions.ToArray()) // Use ToArray to avoid modifying the collection inside the loop
    {
        if (pos == TopLeft)
        {
            movePositions.Add(MiddleMiddle);
        }
        else if (pos == TopMiddle)
        {
            movePositions.Add(MiddleRight);
            movePositions.Add(MiddleLeft);
        }
        else if (pos == TopRight)
        {
            movePositions.Add(MiddleMiddle);
        }
        else if (pos == MiddleLeft)
        {
            movePositions.Add(TopMiddle);
            movePositions.Add(BottomMiddle);
        }
        else if (pos == MiddleRight)
        {
            movePositions.Add(TopMiddle);
            movePositions.Add(BottomMiddle);
        }
        else if (pos == BottomLeft)
        {
            movePositions.Add(MiddleMiddle);
        }
        else if (pos == BottomMiddle)
        {
            movePositions.Add(MiddleRight);
            movePositions.Add(MiddleLeft);
        }
        else if (pos == BottomRight)
        {
            movePositions.Add(MiddleMiddle);
        }
        else if(pos==MiddleMiddle){
            movePositions.Add(TopLeft);
            movePositions.Add(TopRight);
            movePositions.Add(BottomLeft);
            movePositions.Add(BottomRight);
        }
    }

    // If there are no valid positions, return
    if (movePositions.Count == 0)
        return;

    // Choose a random position from the filtered list
    Vector3 newPosition = movePositions[Random.Range(0, movePositions.Count)];

    // Move the boss instantly to the new position
    transform.position = newPosition;
}

    void fire()
{
    Vector3 position = transform.position;
    lastFired = Time.time;
    if(inPhaseTwo)
    {
        StartCoroutine(FireAttacks2(position));
    }
    else
    {
        StartCoroutine(FireAttacks(position));
    }
}

IEnumerator FireAttacks(Vector3 position)
{
    // Trigger FlareAttack1 immediately
    FlareAttack1(position);

    // Wait for 1.5 seconds
    yield return new WaitForSeconds(2.5f);

    // Trigger BeamAttack1 four times with a gap of 0.25 seconds between each call
    for (int i = 0; i < 12; i++)
    {
        BeamAttack1(position);
        if(i==8){
            Move1();
        }
        yield return new WaitForSeconds(0.1f);
    }    
}

IEnumerator FireAttacks2(Vector3 position)
{
    // Trigger FlareAttack1 immediately
    FlareAttack2(position);

    // Wait for 1.5 seconds
    yield return new WaitForSeconds(2.5f);

    // Trigger BeamAttack1 four times with a gap of 0.25 seconds between each call
    for (int i = 0; i < 12; i++)
    {
        BeamAttack2(position);
        if(i==8){
            Move2();
        }
        yield return new WaitForSeconds(0.1f);
    }    
}

void FlareAttack1(Vector3 position)
{
    Instantiate(Flare, position, Quaternion.Euler(0f, 0f, 0f));
    Instantiate(Flare, position, Quaternion.Euler(0f, 0f, 90f));
    Instantiate(Flare, position, Quaternion.Euler(0f, 0f, 180f));
    Instantiate(Flare, position, Quaternion.Euler(0f, 0f, 270f));
}

void BeamAttack1(Vector3 position)
{
    Instantiate(bossProjectile, position, Quaternion.Euler(0f, 0f, 0f));
    Instantiate(bossProjectile, position, Quaternion.Euler(0f, 0f, 90f));
    Instantiate(bossProjectile, position, Quaternion.Euler(0f, 0f, 180f));
    Instantiate(bossProjectile, position, Quaternion.Euler(0f, 0f, 270f));
}

void FlareAttack2(Vector3 position)
{
    Instantiate(Flare, position, Quaternion.Euler(0f, 0f, 0f));
    Instantiate(Flare, position, Quaternion.Euler(0f, 0f, 90f));
    Instantiate(Flare, position, Quaternion.Euler(0f, 0f, 180f));
    Instantiate(Flare, position, Quaternion.Euler(0f, 0f, 270f));
    Instantiate(Flare, position, Quaternion.Euler(0f, 0f, 45f));
    Instantiate(Flare, position, Quaternion.Euler(0f, 0f, 135f));
    Instantiate(Flare, position, Quaternion.Euler(0f, 0f, 225f));
    Instantiate(Flare, position, Quaternion.Euler(0f, 0f, 315f));
}

void BeamAttack2(Vector3 position)
{
    Instantiate(bossProjectile, position, Quaternion.Euler(0f, 0f, 0f));
    Instantiate(bossProjectile, position, Quaternion.Euler(0f, 0f, 90f));
    Instantiate(bossProjectile, position, Quaternion.Euler(0f, 0f, 180f));
    Instantiate(bossProjectile, position, Quaternion.Euler(0f, 0f, 270f));
    Instantiate(bossProjectile, position, Quaternion.Euler(0f, 0f, 45f));
    Instantiate(bossProjectile, position, Quaternion.Euler(0f, 0f, 135f));
    Instantiate(bossProjectile, position, Quaternion.Euler(0f, 0f, 225f));
    Instantiate(bossProjectile, position, Quaternion.Euler(0f, 0f, 315f));
}
}