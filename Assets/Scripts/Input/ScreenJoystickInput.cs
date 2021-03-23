using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Input/ScreenJoystick")]
public class ScreenJoystickInput : InputBase
{
    Joystick joystick;
    HoldableButton interactButton;
    
    float horizontalMove = 0f;
    float verticalMove = 0f;
    [SerializeField]
    float navigateSensitivity = 2.0f;
    float xRotation = 0f;
    float yRotation = 0f;
    Vector3 move;
    [SerializeField]
    float speed = 6.0f;

    public override void init()
    {
        base.init();
        GameObject[] gameControllers = GameObject.FindGameObjectsWithTag("GameController");
        foreach (GameObject controller in gameControllers)
        {
            joystick = controller.GetComponent<Joystick>();
            interactButton = controller.GetComponent<HoldableButton>();
        }
    }
    
    public override void Look()
    {   
        xRotation -= Input.GetTouch(0).deltaPosition.y * Time.deltaTime * navigateSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation = Input.GetTouch(0).deltaPosition.x * Time.deltaTime * navigateSensitivity;

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * yRotation);
    }

    public override void Move()
    {
        horizontalMove = joystick.Horizontal;
        verticalMove = joystick.Vertical;
        move = playerTransform.right * horizontalMove + playerTransform.forward * verticalMove;
        controller.Move(move * speed * Time.deltaTime);
    }

    public override bool? Interact()
    {
        if (interactButton.isPressed) return true;
        if (interactButton.isReleased) return false;
        return null;
    }
}
