using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OfficeTaskManager : MonoBehaviour
{
    // Target Text
    [SerializeField] Text promptInfo = null;
    [SerializeField] Text targetDigitsText = null;
    List<uint> targetDigitList = new List<uint>();

    void ResetDigits()
    {
        int digitCount = Random.Range(6, 9);
        targetDigitList = new List<uint>();
        for(int i=0;i<digitCount;i++)
        {
            targetDigitList.Add((Random.value > 0.5f)? 1U:0U);
        }
        targetDigitsText.text = string.Join("  ", targetDigitList);
        inputDigitList.Clear();
    }


    // Input Digit
    [SerializeField] Text inputDigitsText = null;
    List<uint> inputDigitList = new List<uint>();
    bool InputEnabled = false;
    public void OneClicked()
    {
        if (InputEnabled)
            OnKeyInput(1U);
    }
    public void ZeroClicked()
    {
        if (InputEnabled)
            OnKeyInput(0U);
    }
    void OnKeyInput(uint inputDigit)
    {
        inputDigitList.Add(inputDigit);

        // check from the start
        for(int i=0;i<inputDigitList.Count;i++)
        {
            if (inputDigitList[i]!=targetDigitList[i])
            {
                OnInputError();
                return;
            }
        }
        if (inputDigitList.Count == targetDigitList.Count)
            OnInputFinised();
        
    }


    // Blinking cursor
	float m_TimeStamp;
	bool cursor = false;
	string cursorChar = "";
    void BlinkingCursor()
    {
        if (Time.time - m_TimeStamp >= 0.5)
        {
            m_TimeStamp = Time.time;
            if (cursor == false)
            {
                cursor = true;
                if (inputDigitList.Count < targetDigitList.Count)
                {
                    cursorChar = "_";
                }
            }
            else
            {
                cursor = false;
                if (cursorChar.Length != 0)
                {
                    cursorChar = " ";
                }
            }
        }
    }
    

    [SerializeField] Animator AllUIAnimator = null;
    void OnInputError()
    {
        AllUIAnimator.SetTrigger("Shake");
        ResetDigits();
    }


    void OnInputFinised()
    {
        totalTaskCount -= 1;
        if (totalTaskCount<=0)
            StartCoroutine(OnAllTaskFinished());
        else
            ResetDigits();
    }


    [SerializeField] Animator SceneTransAnimator;
    [SerializeField] float TransitionTime = 1.0f;
    IEnumerator OnAllTaskFinished()
    {
        Debug.Log("all task finished");
        // exit scene
        SceneTransAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        Cursor.lockState = CursorLockMode.Locked;
        PlayerSingleton.instance.state = PlayerSingleton.nextState(PlayerSingleton.instance.state);
        SceneManager.LoadScene("Demo_Real_Office", LoadSceneMode.Single);
    }


    int totalTaskCount = 0;
    IEnumerator Start()
    {
        // Init the whole process
        totalTaskCount = Random.Range(5, 8);
        targetDigitsText.text = "";

        // dialogues out
        yield return StartCoroutine(CommonAnimations.ShowText(promptInfo.text, promptInfo));
        yield return new WaitForSeconds(1.0f);
        
        ResetDigits();

        // enable input
        Cursor.lockState = CursorLockMode.None;
        InputEnabled = true;
    }

    void Update()
    {
        // update input area
        BlinkingCursor();
        // update input string on screen
        string inputDigitString = string.Join("  ", inputDigitList);
        if (inputDigitList.Count < targetDigitList.Count)
        {
            inputDigitString += ("  "+cursorChar);
        }
        inputDigitsText.text = inputDigitString;
    }
}
