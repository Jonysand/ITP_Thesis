using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Connecting input system and interaction system

public class InputToInteraction : MonoBehaviour
{
    [SerializeField]
    InputBase inputBase = null;
    [SerializeField]
    InteractionInputData interactionInputData = null;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        bool? interactionInfo = inputBase.Interact();
        if (interactionInfo!=null)
        {
            if (interactionInfo.Value)
            {
                interactionInputData.InteractClicked = true;
                interactionInputData.InteractRelease = false;
            }
            else
            {
                interactionInputData.InteractRelease = true;
                interactionInputData.InteractClicked = false;
            }
        }
        else
        {
            interactionInputData.InteractClicked = false;
            interactionInputData.InteractRelease = false;
        }
    }
}
