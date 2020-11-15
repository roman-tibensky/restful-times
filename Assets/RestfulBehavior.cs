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
    StateManagement[] stateChoices;
    StateManagement[] stateChoicesAlt;

    delegate void ClickDelegate(int num);
    ClickDelegate clickDelegate;
    
    void Start()
    {
        clickDelegate = GetStateAccordingToInput;
        currentState = startingState;
        ResetTheGame();
    }

    // Update is called once per frame
    void Update()
    {   
        ManageChoices();
    }


    void ResetTheGame() 
    {
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
        if (option < stateChoices.Length) {
            AddPointsAndResults();
            currentState = ChoiceMade(option);
            if(currentState.GameStateTag() == "000") {
                if (shouldRestart) {
                    ResetTheGame();
                } else {
                    FillBaseStateAccordingToProgress();
                }
            } else if(currentState.GameStateTag() == "END") {
                FillEndState();
            } else {
                FillWithCurrentState();
            }
        }
    }

    public StateManagement ChoiceMade(int choicePosition) {
        // float rnd = ;
        // string[] words = {stateChoicesAlt.Length.ToString(), " <= ",  choicePosition.ToString(),  ", ",  rnd.ToString() };
        // Debug.Log(string.Concat(words));
        if(stateChoicesAlt.Length <= choicePosition || Random.value < 0.5f) {
            return stateChoices.Length > choicePosition ? stateChoices[choicePosition] : null;
        } else {
            return stateChoicesAlt[choicePosition];
        }
    }

    void AddPointsAndResults() {
        if (foodPoints == 0) {
            foodPoints = currentState.GameStateFoodPoints();
        }
        if (vidyaPoints == 0) {
            vidyaPoints = currentState.GameStateVidyaPoints();
        }
        if (sleepPoints == 0) {
            sleepPoints = currentState.GameStateSleepPoints();
        }
        if (currentState.GameStateWinStateText().Trim() != "") {
            winStateText += "- " + currentState.GameStateWinStateText() + "\n\r";
        }
    }

    void FillWithCurrentState()
    {
        
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
        stateChoices= currentState.GameStateChoices();
        stateChoicesAlt= currentState.GameStateChoicesAlt();
    }
    
    void FillBaseStateAccordingToProgress()
    {
        if( foodPoints!= 0 && vidyaPoints !=0) {
            headerText.text = "Well, that sure was a day";
            storyText.text = "You ate, you tried to relax. It's time to pass out, I'd say";
            choiceText1.text = "1) Oh god. Yes. Please.";
            stateChoices = new StateManagement[] {baseGameState.ChoiceMade(1)};
            headerText.fontSize = baseGameState.GameStateHeaderTextFontSize();
            storyText.fontSize = baseGameState.GameStateStoryFontSize();
            choiceText1.fontSize = baseGameState.GameStateChoiceText1FontSize();
            choiceText2.fontSize = baseGameState.GameStateChoiceText2FontSize();
            choiceText3.fontSize = baseGameState.GameStateChoiceText3FontSize();
            shouldRestart = baseGameState.GameStateShouldRestartGame();
        } else if (vidyaPoints != 0) {
            headerText.text = vidyaPoints == 10 ? "Don't you look chilled out" : "Not sure if I'd call all of that relaxing";
            storyText.text = "Well, your hunger still hasn't been tended to and passing out sounds more tempting by the minute. So what will it be?";
            choiceText1.text = "1) Feeeeeeeeed meeeeeeee!";
            choiceText2.text = "2) Just... let me sleep. I've had enough for one day.";
            stateChoices = new StateManagement[] {baseGameState.ChoiceMade(0), baseGameState.ChoiceMade(1)};
            headerText.fontSize = baseGameState.GameStateHeaderTextFontSize();
            storyText.fontSize = baseGameState.GameStateStoryFontSize();
            choiceText1.fontSize = baseGameState.GameStateChoiceText1FontSize();
            choiceText2.fontSize = baseGameState.GameStateChoiceText2FontSize();
            choiceText3.fontSize = baseGameState.GameStateChoiceText3FontSize();
            shouldRestart = baseGameState.GameStateShouldRestartGame();
        } else if (foodPoints != 0) {
            headerText.text = foodPoints == 10 ? "The stomach growls seize" : "The stomach growls subside";
            storyText.text = "You managed to acquire sustenance, but your brain is still abuzz. You could try to relax a little, or you could just try and sleep regardless.";
            choiceText1.text = "1) Just... let me sleep. I've had enough for one day.";
            choiceText2.text = "2) Can't go wrong with a quick game or two.";
            stateChoices = new StateManagement[] {baseGameState.ChoiceMade(1), baseGameState.ChoiceMade(2)};
            headerText.fontSize = baseGameState.GameStateHeaderTextFontSize();
            storyText.fontSize = baseGameState.GameStateStoryFontSize();
            choiceText1.fontSize = baseGameState.GameStateChoiceText1FontSize();
            choiceText2.fontSize = baseGameState.GameStateChoiceText2FontSize();
            choiceText3.fontSize = baseGameState.GameStateChoiceText3FontSize();
            shouldRestart = baseGameState.GameStateShouldRestartGame();
        } else {
//            currentState = baseGameState;
            FillWithCurrentState();
        }
    }
    
    void FillEndState()
    {
        stateChoices= currentState.GameStateChoices();
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
                return finalScore + "/30 Unhealthy rest";
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
            finalText += "You've feasted like the gods themselves.";
        }

        if(vidyaPoints == 0) {
            finalText += " You've had no chance to unwind.";
        } else if (vidyaPoints == 5) {
            finalText += " You've faffed about a bit to give your brain some cool down time. It didn't work all that well.";
        } else {
            finalText += " You've had a blast yesterday like you haven't had in a long while.";
        }

        if(sleepPoints == 5) {
            finalText += " You feel more tired than when you passed out. \n\r";
        } else {
            finalText += " You've slept like a baby. \n\r";
        }

        return finalText + "In addition: \n\r" + winStateText;

    }

}
