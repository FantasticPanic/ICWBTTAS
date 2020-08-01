using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{

    public InputField inputField;

    GameController gameController;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }
    void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        gameController.LogStringWithReturn(userInput);

        //mirror input and then process it
        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterCharacters);

        for (int i = 0; i < gameController.inputActions.Length; i++)
        {
            InputAction inputAction = gameController.inputActions[i];
            if (inputAction.keyword == separatedInputWords[0])
            {
                inputAction.RespondToInput(gameController, separatedInputWords);
            }
        }
        InputComplete();

    }

    void InputComplete()
    {
        gameController.DisplayLoggedText();
        inputField.ActivateInputField();
    }
}
