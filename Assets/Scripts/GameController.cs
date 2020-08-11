using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text displayText;
    public InputAction[] inputActions;

    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();
    [HideInInspector] public InteractableItems interactableItems;
    [HideInInspector] public bool isAbleToDo = false;

    //will contain the string for inputs
    List<string> actionLog = new List<string>();

    // Start is called before the first frame update
    void Awake()
    {
        interactableItems = GetComponent<InteractableItems>();
        //get the RoomNavigation componenet
        roomNavigation = GetComponent<RoomNavigation>();
    }

    void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());

        displayText.text = logAsText;
    }

    public void DisplayRoomText()
    {
        //clear the information of the previous room
        ClearCollectionForNewRoom();
        //get the information of the current room
        UnpackRoom();
        //
        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());

        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;

        LogStringWithReturn(combinedText);
    }

    private void UnpackRoom()
    {
        //get the exits for each room
        roomNavigation.UnpackExitsInRoom();

        PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);
    }

    void PrepareObjectsToTakeOrExamine(Room currentRoom)
    {
        for (int i = 0; i < currentRoom.interactableObjectsInRoom.Length; i++)
        {
            string descriptionNotInInventory = interactableItems.GetObjectsNotInInventory(currentRoom, i);
            if (descriptionNotInInventory != null)
            {
                interactionDescriptionsInRoom.Add(descriptionNotInInventory);
            }
            InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];

            //get key for different inputActions
            for (int j = 0; j < interactableInRoom.interactions.Length; j++)
            {
                Interaction interaction = interactableInRoom.interactions[j];
                if (interaction.inputAction.keyword == "examine")
                {
                    //pass in the name of the noun and give back the response
                    interactableItems.examineDictionary.Add(interactableInRoom.noun, interaction.textResponse);
                   
                }
                if (interaction.inputAction.keyword == "take")
                {
                    //pass in the name of the noun and give back the response
                    interactableItems.takeDictionary.Add(interactableInRoom.noun, interaction.textResponse);
                  
                }
            }
        }
    }

    //if the input contains a noun (item) in the database
    public string TestVerbDictionaryWithNoun(Dictionary<string, string> verbDictionary, string verb, string noun)
    {
        
        if (verbDictionary.ContainsKey(noun))
        {
            
            return verbDictionary[noun];
            
        }
 
        return "You can't " + verb + " " + noun;
    }

    void ClearCollectionForNewRoom()
    {
        //clear the previous descriptions of the exits in the room
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
        interactableItems.ClearCollections();
    }
    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    public string UnknownInput()
    {

        return "you don't know how";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
