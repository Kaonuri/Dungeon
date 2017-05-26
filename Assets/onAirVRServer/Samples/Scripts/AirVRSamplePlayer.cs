using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class AirVRSamplePlayer : MonoBehaviour {
    private const float ThrowSpeed = 12.0f;
    private const float ThrowTorqueSpeed = 20.0f;

    private Transform _thisTransform;
    private CharacterController _thisCharacterController;
    private AirVRStereoCameraRig _cameraRig;
    private Transform _headLocator;
    private float _fallingSpeed;
    private AirVRSampleAudioSource _soundShot;

    public AirVRSampleCan canPrefab;
    public float gravity;
    public float speed;

    private void resetFalling() {
        _fallingSpeed = 0.0f;
    }

    private void updateFalling(float deltaTime) {
		if (_thisCharacterController.isGrounded) {
			_fallingSpeed = 0.0f;
		}
		else {
			_fallingSpeed += gravity * deltaTime;
		}
	}

    private Vector2 inputDirection {
        get {
            Vector2 result = AirVRInput.Get(_cameraRig, AirVRInput.Touchpad.Axis2D.Drag) + AirVRInput.Get(_cameraRig, AirVRInput.Gamepad.Axis2D.LThumbstick);
            return new Vector2(Mathf.Clamp(result.x, -1.0f, 1.0f), Mathf.Clamp(result.y, -1.0f, 1.0f));
        }
    }

    private void processMovement() {
        if (_thisCharacterController.enabled) {
            Vector3 moveDirection = inputDirection;
            if (moveDirection.sqrMagnitude > 1.0f) {
                moveDirection = moveDirection.normalized;
            }

            Vector3 velocity = speed * (_cameraRig as AirVRStereoCameraRig).centerEyeAnchor.TransformDirection(moveDirection);
            Vector3 horizontalDir = new Vector3(velocity.x, 0.0f, velocity.z).normalized;

            Vector3 movingDir = velocity.magnitude * horizontalDir * Time.deltaTime;
            if (_fallingSpeed > 0.0f) {
                _thisCharacterController.Move(movingDir + Mathf.Max(_fallingSpeed * Time.deltaTime, movingDir.magnitude / Mathf.Tan(_thisCharacterController.slopeLimit)) * Vector3.down);
            }
            else {
                _thisCharacterController.Move(movingDir);
            }
            updateFalling(Time.deltaTime);
        }
    }

    private void processInput() {
        if (AirVRInput.GetDown(_cameraRig, AirVRInput.Touchpad.Button.Touch) || AirVRInput.GetDown(_cameraRig, AirVRInput.Gamepad.Button.A)) {
            throwCan();
        }
    }

    public void throwCan() {
        Vector3 forward = _cameraRig.centerEyeAnchor.forward;

        AirVRSampleCan can = Instantiate(canPrefab, transform.position, _cameraRig.centerEyeAnchor.rotation) as AirVRSampleCan;
        can.Throw(forward * ThrowSpeed, Vector3.right * ThrowTorqueSpeed);

        _soundShot.Play();
    }

    void Awake() {
        _thisTransform = transform;
        _thisCharacterController = GetComponent<CharacterController>();
        _cameraRig = GetComponentInChildren<AirVRStereoCameraRig>();
        _headLocator = transform.Find("Body/HeadLocator");
        _soundShot = transform.Find("SoundShot").GetComponent<AirVRSampleAudioSource>();
    }

    void Update() {
        _headLocator.rotation = _cameraRig.centerEyeAnchor.rotation;
        
        processMovement();
        processInput();
    }

    public void SetPosition(Transform pos) {
        _thisTransform.position = pos.position;
        _thisTransform.rotation = pos.rotation;
    }

    public void EnableInteraction(bool enable) {
        _thisCharacterController.enabled = enable;

        if (enable == false) {
            resetFalling();
        }
    }
}
