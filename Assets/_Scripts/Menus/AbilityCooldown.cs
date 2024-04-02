using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour

{
    public GameObject player;
    public GameObject spriteObject;
    private PlayerMovement playerScript;
    private Image spriteImage;

    public float requiredTranq;
    private Image backgroundImage;

    public string abilityName;
    private bool secondAbilityUnlocked;
    private bool thirdAbilityUnlocked;

    void Start()
    {
        playerScript = player.GetComponent<PlayerMovement>();
        spriteImage = spriteObject.GetComponent<Image>();
        backgroundImage = GetComponent<Image>();

        secondAbilityUnlocked = PlayerPrefs.GetInt("Faendel") > 1;
        thirdAbilityUnlocked = PlayerPrefs.GetInt("Olaf") > 2;

    }

    void Update()
    {
        if (abilityName == "Big")
        {
            if (!secondAbilityUnlocked) { gameObject.SetActive(false); }
            
        }

        if (abilityName == "Ult")
        {
            if (!thirdAbilityUnlocked) { gameObject.SetActive(false); }

        }


        if (playerScript.tranquility < requiredTranq)
        {
            backgroundImage.color = new Color(1, 1, 1, 0.5f);
            spriteImage.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            backgroundImage.color = new Color(1, 1, 1, 1f);
            spriteImage.color = new Color(1, 1, 1, 1f);
        }

    }
}
