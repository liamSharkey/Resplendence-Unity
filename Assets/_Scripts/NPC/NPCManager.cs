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
    private Vector3 thirdSeat = new Vector3(1.5f, 1.543f, 0);
    private Vector3 fourthSeat = new Vector3(6.5f, 1.543f, 0);
    private Vector3 fifthSeat = new Vector3(-3.51f, -2.35f, 0);

    private Vector3 firstStand = new Vector3(-20.53f, 1.15f, 0);
    private Vector3 secondStand = new Vector3(-7.7f, 6.9f, 0);
    private Vector3 thirdStand = new Vector3(8.65f, 5.72f, 0);

    // ctrl+k, ctrl+d to autoformat
    void Start()
    {
        tavernSetup = _GameManager.highestBossDefeated;

        // case equates to pre boss tavern scene number
        switch (tavernSetup)
        {
            // pinkLady.transform.position = firstSeat;
            case 0:
                pinkLady.transform.position = firstSeat;
                greenMan.SetActive(false);
                ozzy.transform.position = firstStand;
                bruce.SetActive(false);
                olaf.SetActive(false);
                yatso.transform.position = thirdSeat;
                milo.SetActive(false);
                break;
            case 1:
                pinkLady.SetActive(false);
                greenMan.transform.position = fourthSeat;
                ozzy.transform.position = firstStand;
                bruce.transform.position = secondStand;
                olaf.SetActive(false);
                yatso.transform.position = fifthSeat;
                milo.SetActive(false);

                break;
            case 2:
                pinkLady.transform.position = firstSeat;
                greenMan.transform.position = thirdStand;
                ozzy.transform.position = firstStand;
                bruce.SetActive(false);
                olaf.transform.position = thirdSeat;
                yatso.transform.position = secondSeat;
                milo.transform.position = secondStand;
                break;
            case 3:
                pinkLady.SetActive(false);
                greenMan.SetActive(false);
                ozzy.SetActive(false);
                bruce.transform.position = fifthSeat;
                olaf.transform.position = fourthSeat;
                yatso.SetActive(false);
                milo.transform.position = secondStand;
                break;
            case 4:
                pinkLady.transform.position = firstSeat;
                greenMan.transform.position = thirdSeat;
                ozzy.SetActive(false);
                bruce.SetActive(false);
                olaf.SetActive(false);
                yatso.transform.position = fifthSeat;
                milo.transform.position = thirdStand;
                break;
            case 5:
                pinkLady.transform.position = secondSeat;
                greenMan.SetActive(false);
                ozzy.transform.position = firstStand;
                bruce.SetActive(false);
                olaf.transform.position = thirdSeat;
                yatso.SetActive(false);
                milo.SetActive(false);
                break;
            case 6:
                pinkLady.SetActive(false);
                greenMan.transform.position = thirdSeat;
                ozzy.SetActive(false);
                bruce.transform.position = fifthSeat;
                olaf.transform.position = firstSeat;
                yatso.SetActive(false);
                milo.transform.position = secondStand;
                break;
            case 7:
                pinkLady.SetActive(false);
                greenMan.transform.position = firstSeat;
                ozzy.transform.position = firstStand;
                bruce.SetActive(false);
                olaf.SetActive(false);
                yatso.SetActive(false);
                milo.transform.position = fourthSeat;
                break;
            case 8:
                pinkLady.transform.position = fifthSeat;
                greenMan.SetActive(false);
                ozzy.transform.position = firstStand;
                bruce.SetActive(false);
                olaf.transform.position = thirdSeat;
                yatso.transform.position = secondStand;
                milo.SetActive(false);
                break;
            case 9:
                pinkLady.SetActive(false);
                greenMan.SetActive(false);
                ozzy.SetActive(false);
                bruce.transform.position = secondStand;
                olaf.transform.position = fifthSeat;
                yatso.transform.position = secondSeat;
                milo.transform.position = thirdStand;
                break;
        }
        
    }

}
