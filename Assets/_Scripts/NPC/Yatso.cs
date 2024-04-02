using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yatso : NPCDialogue
{
    private string[] firstDialogueSet = { "Oh goodness!",
    "I'm sorry I don't mean to sound startled. Your spirit is just so unbalanced, it shocked me to my core!",
    "I can sense a journey in your future. It is important that you know some things before you begin.",
    "Your TRANQUILITY will rise quickly when you stand still, and slowly when you move.",
    "You can channel TRANQUILITY into attacks, but be careful. Trying to attack without enough TRANQUILITY will make you lose it!",
    "Be sure that you have enough TRANQUILITY for your attack before you use it, or you will find yourself defenseless!"};

    private string[] secondDialogueSet = { "I see you've begun to purify your spirit, but there is still much work to do.",
    "You may have noticed, as you conquer reflections, your maximum TRANQUILITY and HEALTH will rise.",
    "I should also tell you, the more TRANQUILITY you have, the faster your HEALTH will regenerate.",
    "You may want to keep this in mind as you progress further..."};

    private string[] thirdDialogueSet = { "You've made great progress Mani.",
    "Although your spirit still needs some work.",
    "You must still face Ignorance, Thirst, Fear, Delusion, Disgust, and Jealousy.",
    "I wish you luck in these challenges, they will be not be as easy as what you've done so far."};

    private string[] fourthDialogueSet = { "Your spirit is almost cleansed Mani.",
    "You have only the toughest challenges left, I hope for your sake you're prepared."};

    private void Awake()
    {
        NPCName = "Yatso";
    }

    void Start()
    {
        dialogueProgress = getDialogueProgress();

        // Set Base params
        defaultDialogueSet[0] = "Gah! Still an unbalanced spirit!";

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
            case 4:
                currentKeyDialogue = 3;
                break;
            case 8:
                currentKeyDialogue = 4;
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
