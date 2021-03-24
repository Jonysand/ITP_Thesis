using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Connect everything of the interaction system together
/// </summary>

public class InteractionController : MonoBehaviour
{
    #region Variables
        [Space, Header("Data")]
        [SerializeField] InteractionInputData interactionInputData = null;
        [SerializeField] InteractionData interactionData = null;

        [Space, Header("UI")]
        [SerializeField] InteractionUIPanel uiPanel = null;
        
        [Space, Header("Raycast settings")]
        [SerializeField] float rayDistance;
        [SerializeField] float raySphereRadius;
        [SerializeField] LayerMask interactableLayer;

        #region Private
            Camera m_cam;
            bool m_interacting;
            float m_hold_timer = 0f;
        #endregion

    #endregion


    #region  Build In Methods
        private void Awake() {
            m_cam = Camera.main;
        }

        private void Update() {
            CheckForInteractable();
            CheckForInteractableInput();
        }
    #endregion


    #region Custom methods
        void CheckForInteractable()
        {
            Ray _ray = new Ray(m_cam.transform.position, m_cam.transform.forward);
            RaycastHit _hitInfo;
            bool _hitSomething = Physics.SphereCast(_ray, raySphereRadius, out _hitInfo, rayDistance, interactableLayer);

            // debug
            Debug.DrawRay(_ray.origin, _ray.direction, _hitSomething?Color.green:Color.red);
            // -----

            if (_hitSomething)
            {
                InteractableBase _interactable = _hitInfo.transform.GetComponent<InteractableBase>();
                if (_interactable != null && _interactable.IsInteractable)
                {
                    if (interactionData.IsEmpty() || (!interactionData.IsSameInteractable(_interactable)))
                    {
                        interactionData.Interactable = _interactable;
                        uiPanel.SetToolTip(_interactable.TooltipMessage);
                    }
                }
            }
            else
            {
                uiPanel.ResetUI();
                interactionData.ResetData();
            }

        }

        void CheckForInteractableInput()
        {
            if (interactionData.IsEmpty())
            {
                return;
            }

            if (interactionInputData.InteractClicked)
            {
                m_interacting = true;
                m_hold_timer = 0f;
            }
            if (interactionInputData.InteractRelease)
            {
                m_interacting = false;
                m_hold_timer = 0f;
                uiPanel.UpdateProgressBar(0f);
            }

            if (m_interacting)
            {
                if (!interactionData.Interactable.IsInteractable)
                    return;
                
                if (interactionData.Interactable.HoldInteract)
                {
                    m_hold_timer += Time.deltaTime;
                    float heldPercent = m_hold_timer/interactionData.Interactable.HoldDuration;
                    uiPanel.UpdateProgressBar(heldPercent);
                    if (heldPercent > 1.0f)
                    {
                        interactionData.Interact();
                        m_interacting = false;
                    }
                }
                else
                {
                    interactionData.Interact();
                    m_interacting = false;
                }
            }
        }
    #endregion
}
