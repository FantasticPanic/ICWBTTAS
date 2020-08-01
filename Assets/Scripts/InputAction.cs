﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//base class
public abstract class InputAction : ScriptableObject
{
    public string keyword;

    public abstract void RespondToInput(GameController controller, string[] separatedInputWords);
}
