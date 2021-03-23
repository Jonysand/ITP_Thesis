using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller of the Interaction UI
/// </summary>

public class InteractionUIPanel : MonoBehaviour
{
    [SerializeField]
    Image progressBar;
    [SerializeField]
    Text tooltipText;

    public void SetToolTip(string tooltipStr)
    {
        tooltipText.text = tooltipStr;
    }

    public void UpdateProgressBar(float fillAmount)
    {
        progressBar.fillAmount = fillAmount;
    }

    public void ResetUI()
    {
        progressBar.fillAmount = 0f;
        tooltipText.text = "";
    }
}
