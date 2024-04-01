using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private int tavernSetup;

    public GameObject pinkLady;

    // ctrl+k, ctrl+d to autoformat
    void Start()
    {
        tavernSetup = _GameManager.highestBossDefeated;

        switch (tavernSetup)
        {
            case 0:
                pinkLady.transform.position = new Vector3(-3.51f, 1.543f, 0); 
                break;
            case 1:
                pinkLady.SetActive(false);
                break;
            case 2:
                pinkLady.SetActive(false);
                break;
            case 3:
                pinkLady.SetActive(false);
                break;
            case 4:
                pinkLady.SetActive(false);
                break;
            case 5:
                pinkLady.SetActive(false);
                break;
            case 6:
                pinkLady.SetActive(false);
                break;
            case 7:
                pinkLady.SetActive(false);
                break;
            case 8:
                pinkLady.SetActive(false);
                break;
            case 9:
                pinkLady.SetActive(false);
                break;
            case 10:
                pinkLady.SetActive(false);
                break;
        }
        
    }

}
