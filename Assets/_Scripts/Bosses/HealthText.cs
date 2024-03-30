using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    public GameObject bossObject;
    private Boss bossScript;

    private float health;

    // Start is called before the first frame update
    void Start()
    {
        bossScript = bossObject.GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        health = bossScript.health;

        healthText.text = "BOSS HEALTH: " + health.ToString("F0");
    }
}
