using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{
    public List<InteractableObject> usuableObjectList;
    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();
    public GameController controller;
    //items in room
    [HideInInspector]
    public List<string> nounsInRoom = new List<string>();

    Dictionary<string, ActionResponses> useDictionary = new Dictionary<string, ActionResponses>();

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }
    //list of items in inventory
    List<string> nounsInInventory = new List<string>();
    public string GetObjectsNotInInventory(Room currentRoom, int i)
    {
        InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];

        if (!nounsInInventory.Contains(interactableInRoom.noun))
        {
            nounsInRoom.Add(interactableInRoom.noun);
            return interactableInRoom.description;
        }
        return null;
    }

    public void AddActionReponsesToUseDictionary()
    {
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            //go thougg all the nouns and get their names
            string noun = nounsInInventory[i];
            //loop over all nouns in inventory
            InteractableObject interactableObjectInInventory = GetInteractableObjectFromUsuableList(noun);
            if (interactableObjectInInventory == null)
                continue;

                for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++)
                {
                    Interaction interaction = interactableObjectInInventory.interactions[j];
                    if (interaction.actionResponses == null)
                        continue;
                    //check if there are nouns that reponsd to actionResponses
                if (!useDictionary.ContainsKey(noun))
                {
                    useDictionary.Add(noun, interaction.actionResponses);
                }
            }

        }
    }

    InteractableObject GetInteractableObjectFromUsuableList(string noun)
    {
        for (int i = 0; i < usuableObjectList.Count; i++)
        {
            if (usuableObjectList[i].noun == noun)
            {
                return usuableObjectList[i];
            }
           
        }
        return null;
    }

    public void DisplayInventory()
    {
        controller.LogStringWithReturn("You got these in your jacket: ");
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            controller.LogStringWithReturn(nounsInInventory[i]);
        }
    }

    public void ClearCollections()
    {
        examineDictionary.Clear();
        takeDictionary.Clear();
        nounsInRoom.Clear();
    }

    public Dictionary<string, string> Take(string[] separatedInputWords)
    {
        //check if noun is in room to take
        string noun = separatedInputWords[1];
        if (nounsInRoom.Contains(noun))
        {
            nounsInInventory.Add(noun);
            nounsInRoom.Remove(noun);
            AddActionReponsesToUseDictionary();
            return takeDictionary;
        }
        else
        {
            controller.LogStringWithReturn("There is no " + noun + " here to take.");
            return null;
        }
    }

    public void UseItem(string[] separatedInputWords)
    {
        string nounToUse = separatedInputWords[1];
        if (nounsInInventory.Contains(nounToUse))
        {
            if (useDictionary.ContainsKey(nounToUse))
            {
                bool actionResult = useDictionary[nounToUse].DoActionResponse(controller);
                if (!actionResult)
                {
                    controller.LogStringWithReturn("Hmmm nothing happens. ");
                }
            }
            else
            {
                controller.LogStringWithReturn("You can't use the " + nounToUse);
            }
        }
        else
        {
            controller.LogStringWithReturn("There is no " + nounToUse + " in your inventory to use");
        }
    }
}
