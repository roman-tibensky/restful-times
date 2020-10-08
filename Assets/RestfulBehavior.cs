using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestfulBehavior : MonoBehaviour
{
    [SerializeField] StateManagement startingState;
    [SerializeField] StateManagement baseGameState;
    [SerializeField] StateManagement endGameState;
    string initFoodText;
    string initSleepText;
    string initVidyaText;
    StateManagement initFood;
    StateManagement initSleep;
    StateManagement initVidya;

    bool shouldRestart;
    [SerializeField] Text headerText;
    [SerializeField] TMP_Text storyText;
    // [SerializeField] Text storyText;
    [SerializeField] Text choiceText1;
    [SerializeField] Text choiceText2;
    [SerializeField] Text choiceText3;
    // Start is called before the first frame update

    int foodPoints = 0;
    int sleepPoints = 0;
    int vidyaPoints = 0;

   
    string winStateText = "";

    string winStateHeader = "";
    
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
        winStateHeader = "";
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
        // if (option < optionCount) {
        if (option < currentState.ChoiceLength()) {
            currentState = currentState.ChoiceMade(option);
            if(currentState.GameStateTag() == "000") {
                if (shouldRestart) {
                    ResetTheGame();
                } else {

                }
            } else {
                FillWithCurrentState();
            }
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
        shouldRestart = currentState.GameStateShouldRestartGame();
    }
    
    void FillBaseStateAccordingToProgress()
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
        shouldRestart = currentState.GameStateShouldRestartGame();
    }
    
    void FillEndState()
    {
        optionCount = currentState.ChoiceLength();
        headerText.text = CalculateFinalHeader();
        storyText.text = CreateStoryResult();
        choiceText1.text = currentState.GameStateChoiceText1();
        choiceText2.text = currentState.GameStateChoiceText2();
        choiceText3.text = currentState.GameStateChoiceText3();
        headerText.fontSize = currentState.GameStateHeaderTextFontSize();
        storyText.fontSize = currentState.GameStateStoryFontSize();
        choiceText1.fontSize = currentState.GameStateChoiceText1FontSize();
        choiceText2.fontSize = currentState.GameStateChoiceText2FontSize();
        choiceText3.fontSize = currentState.GameStateChoiceText3FontSize();
        shouldRestart = currentState.GameStateShouldRestartGame();
    }

    string CalculateFinalHeader() {
        int finalScore = foodPoints + sleepPoints + vidyaPoints;

        switch(finalScore) {
            case 5:
                return finalScore + "/30 ...Are you okay?";
            case 10:
                return finalScore + "/30 Sloppy rest";
            case 15:
                return finalScore + "/30 Bad rest";
            case 20:
                return finalScore + "/30 Meh rest";
            case 25:
                return finalScore + "/30 Good rest";
            case 30:
                return finalScore + "/30 Wonderful rest";
            default:
                return "Unscorable, wth";
        }
    }

    string CreateStoryResult() {
        string finalText = "";
        if(foodPoints == 0) {
            finalText += "You haven't eaten.";
        } else if (foodPoints == 5) {
            finalText += "You've sort of eaten.";
        } else {
            finalText += "You've feasted like the gods themselves!";
        }

        if(vidyaPoints == 0) {
            finalText += " You've had no chance to unwind.";
        } else if (vidyaPoints == 5) {
            finalText += " You've faffed about a bit to give your brain some cool down time. It didn't work all that well.";
        } else {
            finalText += " You've had a blast yesterday like youhaven't had in a long while!";
        }

        if(vidyaPoints == 5) {
            finalText += " And you feel more tired than when you passed out. \n\r";
        } else {
            finalText += " And you've slept like a bay! \n\r";
        }

        return finalText + "In addition: \n\r" + winStateText;

    }

}
