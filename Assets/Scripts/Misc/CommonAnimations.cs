using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonAnimations : MonoBehaviour
{
    public static IEnumerator ShowText(string fullText, Text UItext)
    {
        float deltaTime = 0.1f;
        for (int i=0;i<fullText.Length;i++)
        {
            UItext.text = fullText.Substring(0, i+1);
            yield return new WaitForSeconds(deltaTime);
        }
    }
}
