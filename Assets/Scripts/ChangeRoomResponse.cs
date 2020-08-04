using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//change something about the room
[CreateAssetMenu(menuName ="TextAdventure/ActionResponses/ChangeRoom")]
public class ChangeRoomResponse : ActionResponses
{
    public Room roomToChangeTo;

    public override bool DoActionResponse(GameController controller)
    {
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            controller.roomNavigation.currentRoom = roomToChangeTo;
            controller.DisplayRoomText();
            return true;
        }
        return false;
    }
}
