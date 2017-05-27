using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRotation : MonoBehaviour
{
    [SerializeField] private Transform trackerTransform;

    private void Update()
    {
        transform.rotation = trackerTransform.rotation;
    }
}
