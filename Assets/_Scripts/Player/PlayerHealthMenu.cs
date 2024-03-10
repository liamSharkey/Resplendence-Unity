using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthMenu : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    public GameObject playerObject;
    public GameObject pannel;
    private PlayerMovement player;

    private float health;
    private float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        health = player.health;
        maxHealth = player.maxHealth;

        if (health <= (maxHealth / 4))
        {
            pannel.transform.localScale = new Vector3(1.2f, 1.2f, 0);
        }
        else
        {
            pannel.transform.localScale = Vector3.one;
        }

        healthText.text = health.ToString("F0") + "/" + maxHealth.ToString("F0");
    }
}
