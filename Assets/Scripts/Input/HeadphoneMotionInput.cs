using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HearXR;

[CreateAssetMenu(menuName = "Input/HeadphoneMotion")]
public class HeadphoneMotionInput : InputBase
{
    public override void init() {
        base.init();
        // init headphone motion
        // This call initializes the native plugin.
        HeadphoneMotion.Init();

        // Check if headphone motion is available on this device.
        if (HeadphoneMotion.IsHeadphoneMotionAvailable())
        {
            // Subscribe to the rotation callback.
            // Alternatively, you can subscribe to OnHeadRotationRaw event to get the 
            // x, y, z, w values as they come from the API.
            HeadphoneMotion.OnHeadRotationQuaternion += HandleHeadRotationQuaternion;
            
            // Start tracking headphone motion.
            HeadphoneMotion.StartTracking();
        }else{
            Debug.Log("HM not avaiable");
        }
    }

    private void HandleHeadRotationQuaternion(Quaternion rotation)
    {
        // rotate
        // camera.transform.localRotation = rotation;

        // head rotates vertically
        camera.transform.localEulerAngles = new Vector3(rotation.eulerAngles.x, 0.0f, 0.0f);
        // body rotates horizontally
        playerTransform.localEulerAngles = new Vector3(0.0f, rotation.eulerAngles.y, 0.0f);
    }

    public override void Look()
    {
        //camera.transform.rotation = HMRotation;
        return;
    }

    public override void Move()
    {
        return;
    }

    public override bool? Interact()
    {
        return null;
    }
}
