using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSingleton : MonoBehaviour
{
    // Ensuring singleton
    public static PlayerSingleton instance;
    void Awake()
    {
        // Initialized singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // init other miscs
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Daily routine
    public enum DailyState{
        // get up form the bed
        // prompt of going to work
        Bedroom_Morning,
        // prompt of working
        Office_Start,
        // working on the task
        working,
        // prompt of going home
        Office_End,
        // prompt of going to sleep
        Bedroom_Night,
        // have 1 minutes to explore
        // alarm triggered, if not waking up in time, will have special side effect
        Imaging
    }
    public static DailyState nextState(DailyState currentState)
    {
        switch (currentState)
        {
            case DailyState.Bedroom_Morning:
            {
                return DailyState.Office_Start;
            }
            case DailyState.Office_Start:
            {
                return DailyState.working;
            }
            case DailyState.working:
            {
                return DailyState.Office_End;
            }
            case DailyState.Office_End:
            {
                return DailyState.Bedroom_Night;
            }
            case DailyState.Bedroom_Night:
            {
                return DailyState.Imaging;
            }
            case DailyState.Imaging:
            {
                return DailyState.Bedroom_Morning;
            }
            default:
                return DailyState.Bedroom_Morning;
        }
    }

    public DailyState state = DailyState.Bedroom_Morning;
    [SerializeField] Text promptText = null;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (state)
        {
            case DailyState.Bedroom_Morning:
            {
                string promptString = "";
                promptString += ("8:00 am" + '\n');
                promptString += ("It's time to go to work");
                StartCoroutine(CommonAnimations.ShowText(promptString, promptText));
                break;
            }
            case DailyState.Office_Start:
            {
                string promptString = "";
                promptString += ("9:00 am" + '\n');
                promptString += ("Now, finish your work!");
                StartCoroutine(CommonAnimations.ShowText(promptString, promptText));
                break;
            }
            case DailyState.working:
            {
                
                break;
            }
            case DailyState.Office_End:
            {
                string promptString = "";
                promptString += ("9:00 pm" + '\n');
                promptString += ("Your work is done.");
                StartCoroutine(CommonAnimations.ShowText(promptString, promptText));
                break;
            }
            case DailyState.Bedroom_Night:
            {
                string promptString = "";
                promptString += ("10:00 pm" + '\n');
                promptString += ("Time to sleep.");
                StartCoroutine(CommonAnimations.ShowText(promptString, promptText));
                break;
            }
            case DailyState.Imaging:
            {
                break;
            }
            default:
                break;
        }
    }
}
