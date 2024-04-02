using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMan : NPCDialogue
{
    private string[] firstDialogueSet = { "You must be the tavern keeper. Mani is it?",
    "I'm Faendel, the greatest explorer who ever lived.",
    "I was on my way through a nearby forest and stumbled upon this place. \"The Ramblin' Rumbler\". Catchy. ",
    "I like it here, you'll be seeing more of me.",
    "That is when I'm not out searching."};

    private string[] secondDialogueSet = { "Mani. Good to see you.",
    "I recently stumbled upon something you might be interested in.",
    "It's a way of focussing tranquility into energy. Ozzy told me you were into this sorta thing.",
    "All you have to do is hold THE LEFT SHIFT KEY while pressing an arrow key and you'll harness way more energy.",
    "Be careful though. It costs much more of your focus than you would typically exert."};

    private string[] thirdDialogueSet = { "Hey Mani you'll never believe what I found!",
    "Deep in a forest past the mountain range south of here, I found a small village, and there are more people like me there!",
    "To be honest, I've never really known where I came from... ",
    "I can't wait to go back!"};

    private string[] fourthDialogueSet = { "Well Mani, this will be the last time I see you",
    "I found a nice lady in that village I was telling you about.",
    "I think its time I settle down over there, so I won't be needing to stop through here anymore.",
    "I found what I was searching for."};

    private void Awake()
    {
        NPCName = "Faendel";
    }

    void Start()
    {
        dialogueProgress = getDialogueProgress();

        // Set Base params
        defaultDialogueSet[0] = "Better recharge, got to get back out there.";

        // based on most recent boss defeted, determine next key piece of dialogue
        switch (_GameManager.highestBossDefeated)
        {
            default:
                currentKeyDialogue = 1;
                break;
            case 1:
                currentKeyDialogue = 1;
                break;
            case 2:
                currentKeyDialogue = 2;
                break;
            case 4:
                currentKeyDialogue = 3;
                break;
            case 6:
                currentKeyDialogue = 4;
                break;
            case 7:
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
        if (dialogueProgress == keyDialogue || dialogueProgress != keyDialogue-1)
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
