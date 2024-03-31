using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject dialogueToolTip;

    public string[] dialogueSet;
    private int index;  

    public float wordSpeed;
    public bool playerIsClose = false;
    public string NPCName;

    public GameObject contButton;



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                nameText.text = NPCName;
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogueSet[index])
        {
            contButton.SetActive(true);
            if (Input.GetMouseButtonDown(0)) { NextLine(); }
        }

        dialogueToolTip.SetActive(playerIsClose && !dialoguePanel.activeInHierarchy);

    }

    public void NextLine()
    {
        contButton.SetActive(false);
        if (index < dialogueSet.Length -1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing ()
    {
        foreach(char letter in dialogueSet[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }

}
