using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    private bool paused;
    public GameObject panel;


    // ctrl+k, ctrl+d to autoformat
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            paused = !paused;
        }
        panel.SetActive(paused);

        if (paused)
        {
            Time.timeScale = 0f;
        }

        if (!paused)
        {
            Time.timeScale = 1f;
        }
    }
}
