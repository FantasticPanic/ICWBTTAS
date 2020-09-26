using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{


    public InputField inputField;
    private bool isAllowed = false;
    
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
        userInput = userInput.ToLower();

        //mirror input and then process it
        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterCharacters);
       
        //for each of the inputActions in the game controller
        for (int i = 0; i < gameController.inputActions.Length; i++)
        {
            InputAction inputAction = gameController.inputActions[i];
            //if the inputAction matches the first verb 
            if (inputAction.keyword == separatedInputWords[0])
            {
                isAllowed = true;
                gameController.audioSource.clip = gameController.inputSounds[1];
                gameController.audioSource.Play();
                gameController.LogStringWithReturn(userInput);
                inputAction.RespondToInput(gameController, separatedInputWords);
                Debug.Log("input action found");
            }
                
        }

        if (isAllowed == false)
        {
            gameController.audioSource.clip = gameController.inputSounds[0];
            gameController.audioSource.Play();
            gameController.LogStringWithReturn("You don't know how to " + userInput);
        }
        
        InputComplete();
      
    }

    void InputComplete()
    {
        gameController.DisplayLoggedText();
        //player will be back on input field after hitting return
        inputField.ActivateInputField();
        inputField.text = null;
        isAllowed = false;
    }
}
