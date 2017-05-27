using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TouchPadMovement : MonoBehaviour
{
    private AirVRStereoCameraRig cameraRig;

    public float speed;

    private void Awake()
    {
        cameraRig = GetComponentInChildren<AirVRStereoCameraRig>();        
    }

    private void Update()
    {
        if (cameraRig.isBoundToClient)
        {
            float axisX = AirVRInput.GetAxis(cameraRig, AirVRInput.Touchpad.Axis.DragX);
            float axisY = AirVRInput.GetAxis(cameraRig, AirVRInput.Touchpad.Axis.DragY);

            if (Mathf.Abs(axisX + axisY) > 0f)
            {
                Vector3 direction = new Vector3(axisY, 0f, -axisX);
                transform.position += direction * speed * Time.deltaTime;
            }          
        }
    }
}
