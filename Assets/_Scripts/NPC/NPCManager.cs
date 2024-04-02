using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private int tavernSetup;

    public GameObject pinkLady;
    public GameObject greenMan;
    public GameObject ozzy;
    public GameObject bruce;
    public GameObject olaf;
    public GameObject yatso;
    public GameObject milo;

    // translation values for different seats in the tavern
    private Vector3 firstSeat = new Vector3(-3.51f, 1.543f, 0);
    private Vector3 secondSeat = new Vector3(6.5f, -2.35f, 0);

    // ctrl+k, ctrl+d to autoformat
    void Start()
    {
        tavernSetup = _GameManager.highestBossDefeated;

        switch (tavernSetup)
        {
            case 0:
                pinkLady.transform.position = firstSeat;
                greenMan.SetActive(false);
                break;
            case 1:
                pinkLady.SetActive(false);
                greenMan.transform.position = firstSeat;

                break;
            case 2:
                pinkLady.transform.position = firstSeat;
                greenMan.transform.position = secondSeat;
                break;
            case 3:
                pinkLady.SetActive(false);
                greenMan.SetActive(false);
                ozzy.SetActive(false);
                bruce.SetActive(false);
                olaf.SetActive(false);
                yatso.SetActive(false);
                milo.SetActive(false);
                break;
            case 4:
                pinkLady.SetActive(false);
                greenMan.SetActive(false);
                ozzy.SetActive(false);
                bruce.SetActive(false);
                olaf.SetActive(false);
                yatso.SetActive(false);
                milo.SetActive(false);
                break;
            case 5:
                pinkLady.SetActive(false);
                greenMan.SetActive(false);
                ozzy.SetActive(false);
                bruce.SetActive(false);
                olaf.SetActive(false);
                yatso.SetActive(false);
                milo.SetActive(false);
                break;
            case 6:
                pinkLady.SetActive(false);
                greenMan.SetActive(false);
                ozzy.SetActive(false);
                bruce.SetActive(false);
                olaf.SetActive(false);
                yatso.SetActive(false);
                milo.SetActive(false);
                break;
            case 7:
                pinkLady.SetActive(false);
                greenMan.SetActive(false);
                ozzy.SetActive(false);
                bruce.SetActive(false);
                olaf.SetActive(false);
                yatso.SetActive(false);
                milo.SetActive(false);
                break;
            case 8:
                pinkLady.SetActive(false);
                greenMan.SetActive(false);
                ozzy.SetActive(false);
                bruce.SetActive(false);
                olaf.SetActive(false);
                yatso.SetActive(false);
                milo.SetActive(false);
                break;
            case 9:
                pinkLady.SetActive(false);
                greenMan.SetActive(false);
                ozzy.SetActive(false);
                bruce.SetActive(false);
                olaf.SetActive(false);
                yatso.SetActive(false);
                milo.SetActive(false);
                break;
            case 10:
                pinkLady.SetActive(false);
                greenMan.SetActive(false);
                ozzy.SetActive(false);
                bruce.SetActive(false);
                olaf.SetActive(false);
                yatso.SetActive(false);
                milo.SetActive(false);
                break;
        }
        
    }

}
