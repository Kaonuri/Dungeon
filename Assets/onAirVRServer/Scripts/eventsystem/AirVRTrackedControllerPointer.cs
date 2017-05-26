using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVRTrackedControllerPointer : AirVRPointer {
    // implements AirVRPointer
    protected override AirVRInput.Pointer pointer {
        get {
            return AirVRInput.Pointer.TrackedController;
        }
    }

    public override bool primaryButtonPressed {
        get {
            return AirVRInput.GetDown(cameraRig, AirVRInput.TrackedController.Button.TouchpadClick) || AirVRInput.GetDown(cameraRig, AirVRInput.TrackedController.Button.IndexTrigger);
        }
    }

    public override bool primaryButtonReleased {
        get {
            return AirVRInput.GetUp(cameraRig, AirVRInput.TrackedController.Button.TouchpadClick) || AirVRInput.GetUp(cameraRig, AirVRInput.TrackedController.Button.IndexTrigger);
        }
    }
}
