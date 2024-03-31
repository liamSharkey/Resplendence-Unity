using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkLady : NPCDialogue
{
    private string[] firstDialogueSet = { "Hello! I am the Pink Lady, It's so nice to finally be meeting you. You must be Mani.", 
        "I've heard so many lovely things about your beautiful tavern but never had the pleasure of seeing it myself.", 
        "Say, you must see many travellers coming in and out of here, but surely I'm the loveliest... wouldn't you say?" };

    private string[] secondDialogueSet = { "Mani! You seem to have a new glow about you!",
        "Now I understand you may not be so eager to see me, but darling im just touched to see you today.",
        "Why don't we put our differences in the past and you can cuddle up with a lovely old lady like myself.",
        "What do you say, want to be best buds?"};

    void Start()
    {
        NPCName = "Delores";
        Debug.Log(_GameManager.highestBossDefeated);
        if (_GameManager.highestBossDefeated <= 1)
        {
            dialogueSet = firstDialogueSet;
        }
        else
        {
            dialogueSet = secondDialogueSet;
        }
    }
}
