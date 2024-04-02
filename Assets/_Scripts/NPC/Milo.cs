using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo : NPCDialogue
{
    private string[] firstDialogueSet = { "first Milo" };

    private string[] secondDialogueSet = { "second Milo" };

    private string[] thirdDialogueSet = { "third Milo" };

    private string[] fourthDialogueSet = { "fourth Milo"};

    private void Awake()
    {
        NPCName = "Milo";
    }

    void Start()
    {
        dialogueProgress = getDialogueProgress();

        // Set Base params
        defaultDialogueSet[0] = "deafult Milo";

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
