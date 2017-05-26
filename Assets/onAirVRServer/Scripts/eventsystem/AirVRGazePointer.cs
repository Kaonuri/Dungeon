using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVRGazePointer : AirVRPointer {
    // implements AirVRPointer
    protected override AirVRInput.Pointer pointer {
        get {
            return AirVRInput.Pointer.Gaze;
        }
    }

    public override bool primaryButtonPressed {
        get {
            return AirVRInput.GetDown(cameraRig, AirVRInput.Touchpad.Button.Touch) || AirVRInput.GetDown(cameraRig, AirVRInput.Gamepad.Button.A);
        }
    }

    public override bool primaryButtonReleased {
        get {
            return AirVRInput.GetUp(cameraRig, AirVRInput.Touchpad.Button.Touch) || AirVRInput.GetUp(cameraRig, AirVRInput.Gamepad.Button.A);
        }
    }

}
