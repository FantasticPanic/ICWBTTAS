using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public Room currentRoom;

    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();
    private GameController controller;

    public object AttemptToChangeRoom { get; internal set; }

    //get the controller componenet
    private void Awake()
    {
        controller = GetComponent<GameController>();

    }

    // get the exit array in the current room 
    public void UnpackExitsInRoom()
    {
        //for each exit in the room asset
        for (int i = 0; i < currentRoom.exits.Length; i++)
        {
            //get the direction of the exit and where the user will end up
            exitDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);
            //for the controller, get a description of the exit
            controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
        }
    }
    //if the user decides to change rooms
    public void AttemptToChangeRooms(string directionNoun)
    {
        //get the direction noun 
        if (exitDictionary.ContainsKey(directionNoun))
        {
            //get the key of the room to the given direction
            currentRoom = exitDictionary[directionNoun];
            controller.LogStringWithReturn("You head off to the " + directionNoun);
            controller.DisplayRoomText();
        }
        else {
            controller.LogStringWithReturn("You cannot go to the " + directionNoun);
        }
    }

    public void ClearExits()
    {
        //clear the dictionary
        exitDictionary.Clear();

    }
}
