using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputBase : ScriptableObject
{
    public bool enabled = false;
    protected Transform playerTransform = null;
    protected CharacterController controller = null;
    protected Camera camera = null;

    public virtual void init() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        controller = playerTransform.gameObject.GetComponent<CharacterController>();
        camera = Camera.main;
        enabled = true;
    }

    public abstract void Look();
    public abstract void Move();

    public abstract bool? Interact(); // True: clicked; False: Released; Null: no interaction

    protected Vector3 apply_gravity(Vector3 move)
    {
        move.y += Physics.gravity.y;
        bool _isGrounded = Physics.CheckSphere(playerTransform.position, 0.1f, 0, QueryTriggerInteraction.Ignore);
        if (_isGrounded && move.y < 0)
            move.y = 0f;
        return move;
    }
}
