using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePosition : MonoBehaviour
{
    [SerializeField] private Transform trackerTransform;
    [SerializeField] private Vector3 offset;

	private void Update ()
	{
	    transform.position = trackerTransform.position + offset;
	}
}
