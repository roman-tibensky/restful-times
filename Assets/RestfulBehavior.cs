using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestfulBehavior : MonoBehaviour
{
    [SerializeField] StateManagement startingState;
    [SerializeField] Text headerText;
    [SerializeField] Text storyText;
    [SerializeField] Text choiceText1;
    [SerializeField] Text choiceText2;
    [SerializeField] Text choiceText3;
    // Start is called before the first frame update

    [SerializeField] int foodPoints = 0;
    [SerializeField] int sleepPoints = 0;
    [SerializeField] int vidyaPoints = 0;

   
    [SerializeField] string winStateText = "";
    
    StateManagement currentState;
    int optionCount;

    delegate void ClickDelegate(int num);
    ClickDelegate clickDelegate;
    
    void Start()
    {
        clickDelegate = GetStateAccordingToInput;
        ResetTheGame();
    }

    // Update is called once per frame
    void Update()
    {   
        ManageChoices();
    }


    void ResetTheGame() 
    {
        currentState = startingState;
        FillWithCurrentState();
        foodPoints = 0;
        sleepPoints = 0;
        vidyaPoints = 0;
        winStateText = "";
    }


    void ManageChoices()
    {
        
        if(
            Input.GetKeyDown(KeyCode.Alpha1) || 
            Input.GetKeyDown(KeyCode.Keypad1)
        ) {
            GetStateAccordingToInput(0);
        }

        if(
            Input.GetKeyDown(KeyCode.Alpha2) || 
            Input.GetKeyDown(KeyCode.Keypad2)

        ) {
            GetStateAccordingToInput(1);
        }

        if(
            Input.GetKeyDown(KeyCode.Alpha3) || 
            Input.GetKeyDown(KeyCode.Keypad3)
        ) {
            GetStateAccordingToInput(2);
        }
    }


    public void GetStateAccordingToInput(int option) {
        if (option < optionCount) {
            currentState = currentState.ChoiceMade(option);
            FillWithCurrentState();
        }
    }

    void FillWithCurrentState()
    {
        optionCount = currentState.ChoiceLength();
        headerText.text = currentState.GameStateHeader();
        storyText.text = currentState.GameStateStory();
        choiceText1.text = currentState.GameStateChoiceText1();
        choiceText2.text = currentState.GameStateChoiceText2();
        choiceText3.text = currentState.GameStateChoiceText3();
        headerText.fontSize = currentState.GameStateHeaderTextFontSize();
        storyText.fontSize = currentState.GameStateStoryFontSize();
        choiceText1.fontSize = currentState.GameStateChoiceText1FontSize();
        choiceText2.fontSize = currentState.GameStateChoiceText2FontSize();
        choiceText3.fontSize = currentState.GameStateChoiceText3FontSize();
    }

}
