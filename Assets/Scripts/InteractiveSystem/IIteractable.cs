using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface of an interactable object class
/// </summary>

public interface IIteractable
{
    float HoldDuration{get;}
    bool HoldInteract{get;}
    bool MultipleUse{get;}
    bool IsInteractable{get;}

    string TooltipMessage{get;}

    void OnInteract();
}
