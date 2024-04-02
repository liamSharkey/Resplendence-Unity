using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossMirrorBehaviour : MonoBehaviour
{
    private bool inRange = false;
    public GameObject mirror;
    public GameObject UIInstruction;
    public GameObject UIFinished;

    private bool allBossesDefeated;
    private bool canFightNextBoss;

    // Update is called once per frame
    void Update()
    {
        if (_GameManager.highestBossDefeated < _GameManager.totalNumberOfBosses)
        {
            allBossesDefeated = false;
        }
        else
        {
            allBossesDefeated = true;
        }

        // in order for the mirror to work, you have to be in range, not have defeated all the bosses, and have talked to the tutorial NPC at least once
        canFightNextBoss = inRange && !allBossesDefeated && (PlayerPrefs.GetInt("Ozzy") > 0) && (PlayerPrefs.GetInt("Yatso") > 0);

        mirror.SetActive(canFightNextBoss);
        UIInstruction.SetActive(canFightNextBoss);

        UIFinished.SetActive(inRange && allBossesDefeated);

        if (Input.GetKeyDown(KeyCode.E) && inRange && !allBossesDefeated)
        {
            // loads the next boss based on index, using boss defeated tracker. +2 for two scenes that come before the first boss scene
            SceneManager.LoadScene(_GameManager.highestBossDefeated + 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            inRange = false;
        }
    }
}
