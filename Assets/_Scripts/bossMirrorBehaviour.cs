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

    // Update is called once per frame
    void Update()
    {
        mirror.SetActive(inRange && ! _GameManager.bossOneDefeated);
        UIInstruction.SetActive(inRange && !_GameManager.bossOneDefeated);

        UIFinished.SetActive(inRange && _GameManager.bossOneDefeated);

        if (Input.GetKeyUp("e") && inRange && !_GameManager.bossOneDefeated)
        {
            SceneManager.LoadScene("BossFight1");
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
