using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bruce : NPCDialogue
{
    private string[] firstDialogueSet = { "God, this place is the worst!",
    "Wait you're the Tavern Keeper?",
    "You must be the biggest idiot there is!",
    "Just a foolish little man with a foolish little tavern."};

    private string[] secondDialogueSet = { "Hey Mani, I'm sorry about earlier, that was mean.",
    "I didn't really mean any of it...",
    "HA! Tricked you! of course I meant it, you loser!",
    "I can't believe you fell for that!"};

    private string[] thirdDialogueSet = { "Okay so I recently learned that you're friends with Olaf.",
    "Listen Mani I was just joking earlier. Please don't tell Olaf I said any of it.",
    "That guy scares me to my core..."};

    private string[] fourthDialogueSet = { "Okay seriously I don't get it!",
    "You seem like the worst guy ever and yet everyone seems to like you!",
    "Yeah sure this tavern is alright or whatever, but why don't they like me.",
    "I'm just as good as you, alright?!"};

    private void Awake()
    {
        NPCName = "Bruce";
    }

    void Start()
    {
        dialogueProgress = getDialogueProgress();

        // Set Base params
        defaultDialogueSet[0] = "This place sucks!";

        // based on most recent boss defeted, determine next key piece of dialogue
        switch (_GameManager.highestBossDefeated)
        {
            default:
                currentKeyDialogue = 1;
                break;
            case 1:
                currentKeyDialogue = 1;
                break;
            case 3:
                currentKeyDialogue = 2;
                break;
            case 6:
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
