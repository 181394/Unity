using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	private Rigidbody rb;
	private int ThrustForce;

	public int Rotationspeed;
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		ThrustForce = 1000;
	}
	
	// Update is called once per frame
	void Update ()
	{
		processInput();
	}

	void processInput()
	{
		if (Input.GetKey(KeyCode.Space))
			rb.AddRelativeForce(Vector3.up*ThrustForce * Time.deltaTime);
		if (Input.GetKey(KeyCode.S))
			transform.Rotate(Vector3.forward, -Rotationspeed * Time.deltaTime);
		else if (Input.GetKey(KeyCode.A))
			transform.Rotate(Vector3.forward, Rotationspeed * Time.deltaTime);

	}
}
