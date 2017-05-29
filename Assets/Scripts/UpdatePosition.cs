using UnityEngine;

public class UpdatePosition : MonoBehaviour
{
    [SerializeField] private Transform trackerTransform;
    [SerializeField] private Vector3 offset;

	private void LateUpdate ()
	{
	    Vector3 back = trackerTransform.forward * -1f;
	    Vector3 down = trackerTransform.up * -1f;

//        transform.position = trackerTransform.transform.position - trackerTransform.forward * offset.z;	    
//        print(Vector3.Distance(transform.position, trackerTransform.position));	  

	    transform.position = trackerTransform.position + offset;
	}
}
