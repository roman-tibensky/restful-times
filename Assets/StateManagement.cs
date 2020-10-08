using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "StateObject")]
public class StateManagement : ScriptableObject
{
    [SerializeField] string headerText;
    [SerializeField] [TextArea(10, 14)] string storyText;
    [SerializeField] [TextArea(2, 4)] string choiceText1;
    [SerializeField] [TextArea(2, 4)] string choiceText2;
    [SerializeField] [TextArea(2, 4)] string choiceText3;
    [SerializeField] int headerTextFontSize = 40;
    [SerializeField] int storyTextFontSize = 18;
    [SerializeField] int choiceText1FontSize = 15;
    [SerializeField] int choiceText2FontSize = 15;
    [SerializeField] int choiceText3FontSize = 15;
    [SerializeField] StateManagement[] choiceStates;
    [SerializeField] StateManagement[] choiceStatesAlt;
    [SerializeField] string tag;
    [SerializeField] int foodPoints;
    [SerializeField] int sleepPoints;
    [SerializeField] int vidyaPoints;
    [SerializeField] bool shouldFinishGame = false;
    [SerializeField] bool shouldRestartGame = false;

   
    [SerializeField] string winStateText;

    public string GameStateHeader () 
    {
        return headerText;
    }

    public string GameStateStory () 
    {
        return storyText;
    }

    public string GameStateChoiceText1 () 
    {
        return choiceText1;
    }

    public string GameStateChoiceText2 () 
    {
        return choiceText2;
    }

    public string GameStateChoiceText3 () 
    {
        return choiceText3;
    }

    public int GameStateHeaderTextFontSize () 
    {
        return headerTextFontSize;
    }
    public int GameStateStoryFontSize () 
    {
        return storyTextFontSize;
    }

    public int GameStateChoiceText1FontSize () 
    {
        return choiceText1FontSize;
    }

    public int GameStateChoiceText2FontSize () 
    {
        return choiceText2FontSize;
    }

    public int GameStateChoiceText3FontSize () 
    {
        return choiceText3FontSize;
    }


    public string GameStateTag () 
    {
        return tag;
    }

    public int GameStateFoodPoints () 
    {
        return foodPoints;
    }

    public int GameStateSleepPoints () 
    {
        return sleepPoints;
    }

    public int GameStateVidyaPoints () 
    {
        return vidyaPoints;
    }
    public string GameStateWinStateText () 
    {
        return winStateText;
    }

    public bool GameStateShouldFinishGame () 
    {
        return shouldFinishGame;
    }
    public bool GameStateShouldRestartGame () 
    {
        return shouldRestartGame;
    }

    public StateManagement ChoiceMade(int choicePosition) {
        return choiceStates.Length > choicePosition ? choiceStates[choicePosition] : null;
    }

    public void SetStateChoices(StateManagement[] states) {
        choiceStates = states;
    }

    public int ChoiceLength() {
        return choiceStates.Length;
    }
}