using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo : NPCDialogue
{
    private string[] firstDialogueSet = { "Hey Mani, how've you been?",
        "Listen, I know you don't believe in any of the sorcery from the stories I've told you, but I'm telling you that mirror I gave you is something special.",
        "All you have to do is get real close and look into it and it'll give you a real test of strength",
        "Just remember, don't be reckless. You're not going to be able to fend anything off by just running around aimlessly. The longer you're calm, the stronger you'll be.",
        "If you do want to fight back though, try the arrow keys. They'll help you channel your tranquility and conquer the demons."};

    private string[] secondDialogueSet = { "Mani! You seem to have a new glow about you!",
        "Now I understand you may not be so eager to see me, but I just can't seem to leave its too nice of a place and the food is...",
        "Okay, I have a confession. The real reason I'm here today is I'm supposed to meet this man named Godot.",
        "You see, I've always dreamt of being an adventurer like the other folks in here, but I've never known how.",
        "But I've heard, if you give him a chance, Godot will take you on an adventure you won't forget! I can't wait to meet him and change my life!"};

    private string[] thirdDialogueSet = { "...oh, hi Mani",
        "I know I know, when did Delores turn into such a downer?",
        "I've been waiting for so long and no one has come to see me. I'm beginning to have second thoughts about this adventure of mine.",
        "I just can't help but seeing all these heroic folk in here and thinking: \"how could I ever do that?\"",
        "Maybe its time to pack it up. He was probably never going to come anyway..."};

    private string[] fourthDialogueSet = { "Oh my, Mani! Its great to see you!",
        "No, that man I was waiting for never came. But who cares. I've decided I'm going to figure out this adventuring stuff on my own.",
        "I don't know what it is about you, but it just seems like you've changed so much in the last few weeks.",
        "You've truly inspired me to make my own change. I'm going to be an adventurer! and no one can stop me."};

    private void Awake()
    {
        NPCName = "Milo";
    }

    void Start()
    {
        dialogueProgress = getDialogueProgress();

        // Set Base params
        defaultDialogueSet[0] = "Good luck Mani, I hope you find what you need in there.";

        // based on most recent boss defeted, determine next key piece of dialogue
        switch (_GameManager.highestBossDefeated)
        {
            default:
                currentKeyDialogue = 1;
                break;
            case 2:
                currentKeyDialogue = 1;
                break;
            case 3:
                currentKeyDialogue = 1;
                break;
            case 4:
                currentKeyDialogue = 2;
                break;
            case 6:
                currentKeyDialogue = 3;
                break;
            case 7:
                currentKeyDialogue = 3;
                break;
            case 9:
                currentKeyDialogue = 4;
                break;
        }
        // set the dialogue set to be the accurate dialogue set
        setDialogueSet(currentKeyDialogue);

    }

    public void setDialogueSet(int keyDialogue)
    {
        // if the player has already gone through the current key dialogue, show them the default set for the NPC
        // also locks player out of later key dialogues if they have not done previous key dialogues
        if (dialogueProgress == keyDialogue || dialogueProgress != keyDialogue - 1)
        {
            dialogueSet = defaultDialogueSet;
        }
        // if they have not, set the dialogue set to be the appropriate key dialogue box for their progress
        else
        {
            isKeyDialogue = true;
            switch(keyDialogue)
            {
                default:
                    dialogueSet = defaultDialogueSet;
                    break;
                case 1:
                    dialogueSet = firstDialogueSet;
                    break;
                case 2:
                    dialogueSet = secondDialogueSet;
                    break;
                case 3:
                    dialogueSet = thirdDialogueSet;
                    break;
                case 4:
                    dialogueSet = fourthDialogueSet;
                    break;
            }
        }

        //Debug.Log("Bosses defeated: " + _GameManager.highestBossDefeated.ToString());
        //Debug.Log("Key Dialogue to address: " + keyDialogue.ToString());
        //Debug.Log("Dialogue set associated with this: " + dialogueSet[0]);
        //Debug.Log("This is a key dialogue set: " + isKeyDialogue.ToString());
    }
}
