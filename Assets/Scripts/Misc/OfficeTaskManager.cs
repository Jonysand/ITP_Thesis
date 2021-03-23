using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (inputDigitList.Equals(targetDigitList))
        {
            OnInputFinised();
        }else
        {
            OnInputError();
        }
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
                    cursorChar += "_";
                }
            }
            else
            {
                cursor = false;
                if (cursorChar.Length != 0)
                {
                    cursorChar = cursorChar.Substring(0, cursorChar.Length - 1);
                }
            }
        }
    }
    

    void OnInputError()
    {

    }


    void OnInputFinised()
    {
        totalTaskCount -= 1;
        if (totalTaskCount==0)
            OnAllTaskFinished();
    }

    void OnAllTaskFinished()
    {

    }


    // Animations
    IEnumerator ShowText(string fullText, Text UItext)
    {
        float deltaTime = 0.1f;
        for (int i=0;i<fullText.Length;i++)
        {
            UItext.text = fullText.Substring(0, i+1);
            yield return new WaitForSeconds(deltaTime);
        }
    }


    int totalTaskCount = 0;
    IEnumerator Start()
    {
        // Init the whole process
        totalTaskCount = Random.Range(5, 8);
        targetDigitsText.text = "";

        // dialogues out
        yield return StartCoroutine(ShowText(promptInfo.text, promptInfo));
        yield return new WaitForSeconds(1.0f);
        
        ResetDigits();

        InputEnabled = true;
    }

    void Update()
    {
        // update input area
        BlinkingCursor();
        string inputDigitString = string.Join("  ", inputDigitList);
        // update input string on screen
        if (inputDigitList.Count < targetDigitList.Count)
        {
            inputDigitString += ("  "+cursorChar);
        }
        inputDigitsText.text = inputDigitString;
    }
}
