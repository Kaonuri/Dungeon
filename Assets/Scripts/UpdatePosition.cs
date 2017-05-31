using UnityEngine;

public class UpdatePosition : MonoBehaviour
{
    [SerializeField] private Transform trackerTransform;
    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        float offsetMagnitude = Vector3.Magnitude(offset);

//        Vector3 offsetPosition = trackerTransform.TransformPoint(offset);

        Vector3 direction = (trackerTransform.transform.forward + trackerTransform.up).normalized * -1;
        transform.position = trackerTransform.position + trackerTransform.localToWorldMatrix.MultiplyVector(offset); //direction * offsetMagnitude);

//        transform.position = offsetPosition;
    }
}
