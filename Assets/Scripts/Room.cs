using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//assets for the rooms that the player enters into

[CreateAssetMenu(menuName ="TextAdventure/Room")]
public class Room : ScriptableObject
{
    
    [TextArea]
    public string description;
    public string roomName;

    //reference to Exit.cs
    public Exit[] exits;
    public InteractableObject[] interactableObjectsInRoom;

}
