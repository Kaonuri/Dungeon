using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AirVRPointer : MonoBehaviour {
    public static List<AirVRPointer> pointers = new List<AirVRPointer>();

    private Texture2D _cookie;

    [SerializeField] private AirVRCameraRig _cameraRig;
    [SerializeField] private string _cookieTextureFilename;
    [SerializeField] private float _depthScaleMultiplier;

    protected float depthScaleMultiplier {
        get {
            return _depthScaleMultiplier;
        }
    }

    private void Awake() {
        pointers.Add(this);
    }

    private IEnumerator Start() {
        if (string.IsNullOrEmpty(_cookieTextureFilename) == false) {
            WWW www = new WWW("file://" + System.IO.Path.Combine(Application.streamingAssetsPath, _cookieTextureFilename));
            yield return www;

            if (string.IsNullOrEmpty(www.error)) {
                _cookie = www.texture;
            }
        }
    }

    protected virtual void Update() {
        if (AirVRInput.IsPointerAvailable(_cameraRig, pointer) && AirVRInput.IsPointerEnabled(_cameraRig, pointer) == false && cookie != null) {
            AirVRInput.EnablePointer(_cameraRig, pointer, cookie, depthScaleMultiplier);
        }
    }

    private void OnDisable() {
        if (AirVRInput.IsPointerEnabled(_cameraRig, pointer)) {
            AirVRInput.DisablePointer(_cameraRig, pointer);
        }
    }

    private void OnDestroy() {
        pointers.Remove(this);
    }

    protected Texture2D cookie {
        get {
            return _cookie;
        }
    }

    protected abstract AirVRInput.Pointer pointer { get; }

    public AirVRCameraRig cameraRig {
        get {
            return _cameraRig;
        }
    }

    public bool interactable {
        get {
            return AirVRInput.IsPointerEnabled(_cameraRig, pointer);
        }
    }

    public abstract bool primaryButtonPressed { get; }
    public abstract bool primaryButtonReleased { get; }

    public Ray GetWorldRay() {
        switch (pointer) {
            case AirVRInput.Pointer.Gaze:
                return new Ray(_cameraRig.headPose.position, _cameraRig.headPose.forward);
            case AirVRInput.Pointer.TrackedController:
                Vector3 position = Vector3.zero;
                Quaternion orientation = Quaternion.identity;
                AirVRInput.GetPointerPositionAndRotation(_cameraRig, pointer, out position, out orientation);
                return new Ray(position, orientation * Vector3.forward);
        }
        return new Ray();
    }

    public void UpdateRaycastResult(Ray ray, RaycastResult raycastResult) {
        if (raycastResult.isValid) {
            AirVRInput.UpdatePointerRaycastHitResult(_cameraRig, pointer, ray.origin, raycastResult.worldPosition, raycastResult.worldNormal);
        }
        else {
            AirVRInput.UpdatePointerRaycastHitResult(_cameraRig, pointer, Vector3.zero, Vector3.zero, Vector3.zero);
        }
    }
}
