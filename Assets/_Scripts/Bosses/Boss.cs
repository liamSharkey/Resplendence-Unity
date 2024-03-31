using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Boss : MonoBehaviour
{
    
    public float fireTime;

    public float movementSpeed;

    public GameObject bossProjectile;
    public GameObject player;
    
    public float moveTime;

    public float health;
    public float maxHealth;

    public float deathTime;

    public GameObject bossHealthBar;
    public GameObject victoryScreen;

    public Transform playerTransform;
    public Animator animator;
    public CapsuleCollider2D capsuleCollider;
    public float lastMoved = 0;
    public bool inPhaseTwo = false;
    public int bossNumber;
    public float lastFired;
    public bool dead = false;

    public void UniversalStart(){
        playerTransform = player.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        health = maxHealth;
        lastFired = Time.time;
        lastMoved = Time.time;

        victoryScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    // if the player runs into the boss sprite, player takes damage
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().takeDamage(5);
        }
    }


}
