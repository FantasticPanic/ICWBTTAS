using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Exit
{
    
    public string keyString;  //what direction the user has to go in
    public string exitDescription;  //a descriptionof where the exit is
    public Room valueRoom;         // where the user will end up if this exit is chosen



}
