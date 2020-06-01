using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct talkSnds
{
    public AudioClip[] snds;
}

[System.Serializable]
public struct DialogueHolder
{
    public int dialogueSndId;
    public Sprite dialogueSprite;
    public string dialogue;
    public int stringLength;
    public GameObject endTarget;
    public string message;
}

public class Dialogue : MonoBehaviour
{
    public bool talking;
    public talkSnds[] characterSnds;
    public AudioSource sndSrc;
    public DialogueHolder[] dialogues;
    public TextMeshProUGUI text;
    public Image dialogueImage;
    public GameObject dialogueBox;
    public float talkSpeed;

    private bool spacePressed = false;
    private float futureTime = 0;
    private bool firstD = false;
    private int currDialogue = 0;
    private int currChar = 0;

    private void Update()
    {
        talking = currDialogue < dialogues.Length;

        if (!spacePressed)
        {
            spacePressed = Input.GetKeyDown(KeyCode.Space);
        }

        if (talking)
        {
            dialogueImage.sprite = dialogues[currDialogue].dialogueSprite;
            if(!firstD)
            {
                firstD = true;
                currChar = 0;
                dialogues[currDialogue].stringLength = dialogues[currDialogue].dialogue.Length;
            }

            if (Time.time > futureTime)
            {
                futureTime = Time.time + talkSpeed;
                if (currChar < dialogues[currDialogue].stringLength)
                {
                    if (spacePressed)
                    {
                        currChar = dialogues[currDialogue].stringLength;
                        text.text = dialogues[currDialogue].dialogue;
                        spacePressed = false;
                    }
                    else
                    {
                        sndSrc.PlayOneShot(characterSnds[dialogues[currDialogue].dialogueSndId].snds[Random.Range(0, characterSnds[dialogues[currDialogue].dialogueSndId].snds.Length)]);
                        text.text += dialogues[currDialogue].dialogue[currChar];
                        currChar++;
                    }
                }
                else if(spacePressed)
                {
                    text.text = "";
                    firstD = false;
                    if(talking && (dialogues[currDialogue].endTarget != null))
                    {
                        dialogues[currDialogue].endTarget.SendMessage(dialogues[currDialogue].message);
                    }

                    currDialogue++;
                    currChar = 0;
                    spacePressed = false;
                }
            }

        }
    }
}