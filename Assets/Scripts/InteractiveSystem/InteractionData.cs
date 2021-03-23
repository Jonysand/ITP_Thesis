using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// storing the data of the object that is being interacted with
/// </summary>

[CreateAssetMenu(fileName = "Interaction Data", menuName = "InteractionSystem/InteractionData")]
public class InteractionData : ScriptableObject
{
    InteractableBase m_interactable;

    public InteractableBase Interactable
    {
        get => m_interactable;
        set => m_interactable = value;
    }

    public void Interact()
    {
        m_interactable.OnInteract();
        ResetData();
    }

    public bool IsSameInteractable(InteractableBase _newInteractable) => m_interactable == _newInteractable;

    public void ResetData() => m_interactable = null;

    public bool IsEmpty() => m_interactable == null;
}

