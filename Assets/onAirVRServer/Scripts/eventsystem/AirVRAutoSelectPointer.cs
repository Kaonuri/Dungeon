using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVRAutoSelectPointer : AirVRPointer {
    private AirVRInput.Pointer _currentPointer = AirVRInput.Pointer.Gaze;

    protected override void Update() {
        AirVRInput.Pointer pointer = AirVRInput.IsPointerAvailable(cameraRig, AirVRInput.Pointer.TrackedController) ? AirVRInput.Pointer.TrackedController : AirVRInput.Pointer.Gaze;
        if (pointer != _currentPointer) {
            if (AirVRInput.IsPointerEnabled(cameraRig, _currentPointer)) {
                AirVRInput.DisablePointer(cameraRig, _currentPointer);
            }
            _currentPointer = pointer;
        }

        base.Update();
    }

    // implements AirVRAutoSelectPointer
    protected override AirVRInput.Pointer pointer {
        get {
            return _currentPointer;
        }
    }

    public override bool primaryButtonPressed {
        get {
            switch (pointer) {
                case AirVRInput.Pointer.Gaze:
                    return AirVRInput.GetDown(cameraRig, AirVRInput.Touchpad.Button.Touch) || AirVRInput.GetDown(cameraRig, AirVRInput.Gamepad.Button.A);
                case AirVRInput.Pointer.TrackedController:
                    return AirVRInput.GetDown(cameraRig, AirVRInput.TrackedController.Button.TouchpadClick) || AirVRInput.GetDown(cameraRig, AirVRInput.TrackedController.Button.IndexTrigger);
            }
            return false;
        }
    }

    public override bool primaryButtonReleased {
        get {
            switch (pointer) {
                case AirVRInput.Pointer.Gaze:
                    return AirVRInput.GetUp(cameraRig, AirVRInput.Touchpad.Button.Touch) || AirVRInput.GetUp(cameraRig, AirVRInput.Gamepad.Button.A);
                case AirVRInput.Pointer.TrackedController:
                    return AirVRInput.GetUp(cameraRig, AirVRInput.TrackedController.Button.TouchpadClick) || AirVRInput.GetUp(cameraRig, AirVRInput.TrackedController.Button.IndexTrigger);
            }
            return false;
        }
    }
}
