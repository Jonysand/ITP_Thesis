using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// storing the interaction input data
/// </summary>

[CreateAssetMenu(fileName = "InteractionInputData", menuName = "InteractionSystem/InputData")]
public class InteractionInputData : ScriptableObject
{
    bool m_interactedClicked;
    bool m_interactedRelease;

    public bool InteractClicked
    {
        get => m_interactedClicked;
        set => m_interactedClicked = value;
    }

    public bool InteractRelease
    {
        get => m_interactedRelease;
        set => m_interactedRelease = value;
    }

    public void Reset()
    {
        m_interactedClicked = false;
        m_interactedRelease = false;
    }
}
