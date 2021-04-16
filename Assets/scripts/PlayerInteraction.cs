using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject currentInterObj = null;
    public Interaction currentInterObjScript = null;
    private List<string> questLines = new List<string>();
    private List<string> inventory = new List<string>();

    private void Start()
    {
        // creating base string for system to look at
        questLines.Add("");
        inventory.Add("");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentInterObj)
        {
            if (currentInterObjScript.pickup)
            {
                currentInterObjScript.Pickup();
                inventory.Add(currentInterObjScript.name);
            }
            if (currentInterObjScript.questPickup)
            {
                // check to see if current questlines contain the questline this item is apart of
                if (questLines.Contains(currentInterObjScript.questLine)) 
                {
                    currentInterObjScript.QuestPickup();
                    inventory.Add(currentInterObjScript.name);
                }
            }
            if (currentInterObjScript.questGiver)
            {
                questLines.Add(currentInterObjScript.questName);
                currentInterObjScript.QuestCompleTalks(inventory); // pass inventoy and check on the other side, // Smoother
            }
            if (currentInterObjScript.info )
            {
                currentInterObjScript.Info();
            }
            if (currentInterObjScript.talks )
            {
                currentInterObjScript.Talks();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("InterObject"))
        {
            currentInterObj = collision.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<Interaction>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InterObject"))
        {
            currentInterObj = null;
        }
    }
}
