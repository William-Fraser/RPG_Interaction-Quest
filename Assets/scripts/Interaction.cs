using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [Header("Collectable Object")]
    [Tooltip("Object can be picked up")]
    public bool pickup = false;

    [Header("Quest Collectable Object")]
    [Tooltip("Object can only be picked up if a quest is started")]
    public bool questPickup = false;
    [Tooltip("CASE SENSITIVE, this is the quest name the pickup is apart of")]
    public string questLine;

    [Header("Information Object")]
    [Tooltip("Object gives small amount of info about themselves")]
    public bool info = false;
    [Tooltip("Displayed Text")]
    public string message;
    [Tooltip("Amount of time until message dissappears")]
    public float displayTime;
    private Text infoText;

    [Header("Dialogue Object")]
    [Tooltip("Object displays text box and chain of dialogue")]
    public bool talks = false;
    [TextArea]
    public string[] sentances;

    [Header("Quest Giver & Dialogue Object")]
    [Tooltip("Object displays dialogue if you've completed their quest")]
    public bool questGiver = false;
    public string questName;
    public GameObject questCompletionObject = null;
    [TextArea]
    public string[] questCompletionSentances;
    public bool leavesOnComplete = false;

    private Interactable _InteractableState;
    private enum Interactable{ 
        STATE_NULL,
        STATE_INFO,
        STATE_PICKUP
    
    };

    void Start()
    {
        infoText = GameObject.Find("InfoText").GetComponent<Text>();
    }
    public void Pickup()
    {
        Debug.Log("picked up a " + this.gameObject.name);
        this.gameObject.SetActive(false);
    }
    public void QuestPickup()
    {
        // almost identical to pickup but has a different debug message and turns off former means of communication
        info = false;
        talks = false;
        Debug.Log("picked up a Quest Item " + this.gameObject.name);
        this.gameObject.SetActive(false);
    }
    public void Info()
    {
        Debug.Log("this is a "+ this.gameObject.name);
        StartCoroutine(ShowInfo(message, displayTime));
    }
    public void Talks()
    {
        Debug.Log("you're looking at " + this.gameObject.name);
        FindObjectOfType<DialogueManager>().StartDialogue(sentances);
    }
    public void QuestCompleTalks(List<string>inventory)
    {
        // checks the players inventory to makes sure the item is there
        if (inventory.Contains(questCompletionObject.name)) {
            // disable former communications
            info = false;
            talks = false;
            //talk for completion
            Debug.Log("you finished a quest for at " + this.gameObject.name);
            FindObjectOfType<DialogueManager>().StartDialogue(questCompletionSentances);
            if (leavesOnComplete) { this.gameObject.SetActive(false); }
        }
    }
    IEnumerator ShowInfo(string message, float delay)
    {
        infoText.text = message;
        yield return new WaitForSeconds(delay);
        infoText.text = null;
    }
}
