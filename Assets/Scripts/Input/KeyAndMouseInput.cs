using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Input/KeyAndMouse")]
public class KeyAndMouseInput : InputBase
{
    [SerializeField]
    float mouseSensitibity = 100.0f;
    [SerializeField]
    float speed = 6.0f;

    float xRotation = 0f;

    public override void init()
    {
        base.init();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitibity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitibity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }

    public override void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = playerTransform.right * x + playerTransform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    public override bool? Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
            return true;
        if (Input.GetKeyUp(KeyCode.E))
            return false;
        return null;  
    }
}
