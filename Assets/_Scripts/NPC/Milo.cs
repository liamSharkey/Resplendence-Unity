using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo : NPCDialogue
{
    private string[] firstDialogueSet = { "Oh man, are you Mani? The owner of the Ramblin' Rumbler?",
    "Oh this is great! I'm such a huge fan, people all over the world talk about how great this place is.",
    "I can't believe its really you! You see I'm an adventurer like many of the other folks in here.",
    "But no matter who I've talked to, everyone says that this is the best place to rest up!",
    "Thanks for what you do Mani."};

    private string[] secondDialogueSet = { "Hey Mani, I'm thinking of retiring from the adventuring business.", 
        "You see, as much as I love it, it seems like you're helping more people with your tavern than I ever have out there.",
    "If you'd let me, I'd love to work for you for a bit and learn the ropes.",
    "I don't know its probably stupid... Just think about it okay?"};

    private string[] thirdDialogueSet = { "Hey Mani, you thought about having me work here at all?",
    "Oh, no. I get it, that's okay.",
    "You got your own thing going here. Maybe I could start up my own place so I could help people the way you do..."};

    private string[] fourthDialogueSet = { "Well Mani, I'm doing it, I'm starting up my own place.",
    "I'm gonna call it \"Beck's\", got a nice ring to it huh?",
    "Oh I should explain, my real name is Beck, I just got a reputation that... warranted some change.",
    "Anyways, don't worry I'm going far away, I won't be taking your business, but I wanted to thank you for inspiring me!",
    "Hope you'll come visit some time! "};

    private void Awake()
    {
        NPCName = "Milo";
    }

    void Start()
    {
        dialogueProgress = getDialogueProgress();

        // Set Base params
        defaultDialogueSet[0] = "Great to see you Mani, always brightens my day.";

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
