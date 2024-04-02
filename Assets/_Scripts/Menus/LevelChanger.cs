using System;
using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private string levelToLoad;
    private bool clickedIntoLevel;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject contButton;

    public float wordSpeed;

    private int entryTextToWrite;
    private string[] tavernEntryDialogues = { "Great, another horrible day. Time to see what kind of gruelish beings poured into my tavern today.",
        "Ugh here we go again.",
        "Another day of nothing.",
        "I guess I'd better get up.",
        "I wonder if more people have arrived.",
        "Welp, better get a start to the day.",
        "I hope my favourite customers are here.",
        "Who knows, maybe it'll be a good day.",
        "I hope Ozzy's here today!",
        "Thank goodness for another beautiful day!",
        "Thank goodness for another beautiful day!",
        "Thank goodness for another beautiful day!",};

    private string[] tavernEntryHeaders = { "Mani",
        "Mani (The next morning)",
        "Mani (The next morning)",
        "Mani (a few days later)",
        "Mani (a few days later)",
        "Mani (a few days later)",
        "Mani (a few days later)",
        "Mani (a few weeks later)",
        "Mani (a few weeks later)",
        "Mani (a few weeks later)",
        "Mani (a few weeks later)"};

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "TavernScene")
        {
            animator.SetTrigger("FadeIn");
        }
        else
        {
            StartCoroutine(displayStartMessage());
        }

        entryTextToWrite = _GameManager.highestBossDefeated;

    }

    private void Update()
    {
        if (dialogueText.text == tavernEntryDialogues[entryTextToWrite])
        {
            contButton.SetActive(true);
            if (Input.GetMouseButtonDown(0)) {
                zeroText();
                animator.SetTrigger("FadeIn");
            }
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }

    public void FadeToLevel(string sceneName)
    {
        animator.SetTrigger("FadeOut");
        levelToLoad = sceneName;
    }

    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public IEnumerator displayStartMessage()
    {
        yield return new WaitForSeconds(1);
        dialoguePanel.SetActive(true);
        nameText.text = tavernEntryHeaders[entryTextToWrite];

        StartCoroutine(Typing());
    }
    IEnumerator Typing()
    {
        foreach (char letter in tavernEntryDialogues[entryTextToWrite].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

}
