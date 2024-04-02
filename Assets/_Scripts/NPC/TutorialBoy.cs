using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoy : NPCDialogue
{
    private string[] firstDialogueSet = { "Hey Mani, how've you been?",
        "Listen, I know you don't believe in any of the sorcery from the stories I've told you, but I'm telling you that mirror I gave you is something special.",
        "All you have to do is get real close and look into it and it'll give you a real test of strength",
        "Just remember, don't be reckless. You're not going to be able to fend anything off by just running around aimlessly. The longer you're calm, the stronger you'll be.",
        "If you do want to fight back though, try the ARROW KEYS. They'll help you channel your tranquility and conquer the demons."};

    private string[] secondDialogueSet = { "Looks like you got that mirror to work!",
        "What are you finding in there? I hear its different for everyone that uses it.",
        "Whatever it is, I hope you come out stronger. I've hated seeing you down recently."};

    private string[] thirdDialogueSet = { "I'm so glad to see you looking up Mani.",
    "To be honest I used that mirror myself once.",
    "Things got tough for me but it helped me. Looks like its doing the same for you.",
    "Don't stop now Mani, I know you can come to true inner peace with a little more push."};

    private string[] fourthDialogueSet = { "Hi Mani.",
    "I'm sorry, but I've gotta leave town.",
    "Its been great being nearby the last couple weeks but I have to go back home.",
    "I wanted to see you before I left. I know you're on your own journey right now, and I'm proud of how far you've come.",
    "I hope someday you can come visit me...",
    "Goodbye for now."};

    private void Awake()
    {
        NPCName = "Ozzy";
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
            case 0:
                currentKeyDialogue = 1;
                break;
            case 1:
                currentKeyDialogue = 1;
                break;
            case 2:
                currentKeyDialogue = 2;
                break;
            case 5:
                currentKeyDialogue = 3;
                break;
            case 7:
                currentKeyDialogue = 4;
                break;
            case 8:
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
