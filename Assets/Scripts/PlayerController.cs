using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 5f;
	private Vector3 direction;


	private void Awake()
	{

	}
	
	private void Update ()
	{
		direction = new Vector3(Input.GetAxis ("Vertical"), 0f, Input.GetAxis("Horizontal") * -1f);

		transform.localPosition += direction.normalized * speed * Time.deltaTime;
	}
}
