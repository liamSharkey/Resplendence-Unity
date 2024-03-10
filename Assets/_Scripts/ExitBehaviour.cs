using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBehaviour : MonoBehaviour
{
    private bool inRange = false;
    public GameObject UIInstruction;

    // Update is called once per frame
    void Update()
    {
        UIInstruction.SetActive(inRange);

        if (Input.GetKeyUp("e") && inRange)
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
