using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    InputBase inputLook = null;
    [SerializeField]
    InputBase inputMove = null;
    public InteractionInputData interactionInputData;


    // Start is called before the first frame update
    void Start()
    {
        // Input init
        interactionInputData.Reset();
        inputLook.init();
        if (!inputLook.Equals(inputMove))
            inputMove.init();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        if (inputLook && inputLook.enabled)
        {
            inputLook.Look();
        }
        if (inputMove && inputMove.enabled)
        {
            inputMove.Move();
        }
    }
}
