using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerBody;
    public float movementSpeed;

    public float tranquility;
    public float tranquilityLimit = 100;

    private Vector3 movementChange; //stored as vector3 otherwise the transform.position wont work
    private Animator animator;

    private bool dead = false;
    public float deathTime;

    private float lastFiredTime;
    public float cooldown = 0.1f;

    private Vector3 fireChange;
    public float fireCost = 10;
    public float bigFireCost = 30;
    public float ultFireCost = 90;

    public GameObject projectile;
    public GameObject projectileBig;
    public GameObject projectileUlt;

    public float health = 100;
    public float maxHealth = 100;

    private float lastHealed = 0;
    private float healSpeed;

    private float lastDamaged = 0;
    private float takeDamageCooldown = 0.7f;
    private bool canTakeDamage = true;
    private new Renderer renderer;

    private bool shiftPressed;
    private bool ctrlPressed;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();

        lastFiredTime = Time.time;
        lastHealed = Time.time;
        lastDamaged = Time.time;

        maxHealth = 20 + _GameManager.highestBossDefeated * 5;
        health = maxHealth;

        tranquilityLimit = 100 + _GameManager.highestBossDefeated * 20;
        tranquility = 0;
    }

    void Update()
    {
        movementChange = Vector3.zero; //reset player change ammount
        movementChange.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * movementSpeed;
        movementChange.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * movementSpeed;

        fireChange = Vector3.zero;
        fireChange.x = Input.GetAxisRaw("Fire1");
        fireChange.y = Input.GetAxisRaw("Fire2");

        if (health <= 0)
        {
            StartCoroutine(die());
        }

        if (!canTakeDamage && Time.time - lastDamaged > takeDamageCooldown)
        {
            canTakeDamage = true;
            renderer.material.color = new Color(1, 1, 1, 1f);
        }
        if (!canTakeDamage)
        {
            renderer.material.color = new Color(1, 1, 1, 0.5f);
        }


        if (!dead)
        {
            movePlayer();
            fireProjectile();
            heal();
        }
    }

    void movePlayer()
    {
        if (movementChange != Vector3.zero)
        {

            // does moving animation ---------------------
            animator.SetBool("Moving", true);
            transform.Translate(new Vector3(movementChange.x, movementChange.y));
            animator.SetFloat("horizontalMovement", movementChange.x);
            animator.SetFloat("verticalMovement", movementChange.y);

            if (tranquility < tranquilityLimit)
            {
                tranquility += 5 * Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("Moving", false);

            if (tranquility < tranquilityLimit)
            {
                tranquility += 10 * Time.deltaTime;
            }
        }
    }

    private float cost;
    void fireProjectile()
    {
        // only allows projectiles in non boss fight scenes
        if (! SceneManager.GetActiveScene().name.Contains("Boss")) { return; }
        if (Time.time < (lastFiredTime + cooldown)) { return; }

        //Set cost to either big projectile cost or regular dependent on shift
        shiftPressed = Input.GetKey("left shift");
        ctrlPressed = Input.GetKey(KeyCode.Space);
        if (shiftPressed) { cost = bigFireCost; }
        else if (ctrlPressed) { cost = ultFireCost;  }
        else
        {
            cost = fireCost;
        }

        if (fireChange != Vector3.zero && tranquility > cost)
        {
            if (shiftPressed)
            {
                Instantiate(projectileBig, transform.position, Quaternion.Euler(fireChange));
            }
            else if (ctrlPressed)
            {
                Instantiate(projectileUlt, transform.position, Quaternion.Euler(fireChange));
            }
            else
            {
                Instantiate(projectile, transform.position, Quaternion.Euler(fireChange));
            }
            
            lastFiredTime = Time.time;

            // removes tranquility from player
            if (tranquility > cost)
            {
                tranquility -= cost;
            }
            else
            {
                tranquility = 0;
            }
        }

        else if (fireChange != Vector3.zero && tranquility < cost)
        {
            lastFiredTime = Time.time;
            if (tranquility > (cost / 2))
            {
                tranquility -= (cost / 2);
            }
            else
            {
                tranquility = 0;
            }
        }
    }

    void heal()
    {
        if (tranquility >= 1)
        {
            // Heals at 1hp/3 at max tranq, 1hp/300s at min tranq
            healSpeed = (1 / tranquility) * 300;
        }
        else
        {
            healSpeed = 0;
        }
        if (health < maxHealth && Time.time - lastHealed > healSpeed && healSpeed > 0)
        {
            if (health + 1 > maxHealth)
            {
                health = maxHealth;
                lastHealed = Time.time;
            }
            else
            {
                health += 1;
                lastHealed = Time.time;
            }
        }
    }

    public IEnumerator die()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 0);
        dead = true;
        animator.SetBool("Dying", true);
        yield return new WaitForSeconds(deathTime);

        SceneManager.LoadScene("GameOver");
    }

    public void takeDamage(float damage)
    {
        if (canTakeDamage)
        {
            lastDamaged = Time.time;
            canTakeDamage = false;
            health -= damage;
        }
    }

}

