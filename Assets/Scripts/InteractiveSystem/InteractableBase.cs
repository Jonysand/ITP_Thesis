using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class of the interactable object
/// </summary>

public class InteractableBase : MonoBehaviour, IIteractable
{
    #region Variables
        [Header("Interactable Settings")]
        public float holdDuration;

        [Space]
        [SerializeField] bool holdInteract;
        [SerializeField] bool multipleUse;
        [SerializeField] bool isInteractable;
        [SerializeField] string tooltipMessage = "Interact";
    #endregion

    #region Properties
        public float HoldDuration => holdDuration;
        public bool HoldInteract => holdInteract;
        public bool MultipleUse => multipleUse;
        public bool IsInteractable {
            get{
                return isInteractable;
            }
            set{
                isInteractable = value;
            }
        }
        public string TooltipMessage => tooltipMessage;
    #endregion

    #region Methods
        public virtual void OnInteract()
        {
            Debug.Log("INTERACTED: " + gameObject.name);
        }
    #endregion
}
