using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{


    public InputField inputField;
    private string isAllowed = "you don't know how to ";
    GameController gameController;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
        //when user presses enter
        inputField.onEndEdit.AddListener(AcceptStringInput);
        
    }
    void AcceptStringInput(string userInput)
    {
       

        //put the user input in lowercase


        //mirror input and then process it
        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterCharacters);
       
        for (int i = 0; i < gameController.inputActions.Length; i++)
        {
            InputAction inputAction = gameController.inputActions[i];
            if (inputAction.keyword == separatedInputWords[0])
            {
                isAllowed = "";
                userInput = userInput.ToLower();
                gameController.LogStringWithReturn(isAllowed + userInput);
                inputAction.RespondToInput(gameController, separatedInputWords);
                Debug.Log("input action found");     
            }
           

        }

        userInput = userInput.ToLower();
       gameController.LogStringWithReturn(isAllowed + userInput);
        InputComplete();
      

    }

    void InputComplete()
    {
        gameController.DisplayLoggedText();
        //player will be back on input field after hitting return
        inputField.ActivateInputField();
        inputField.text = null;
    }
}
