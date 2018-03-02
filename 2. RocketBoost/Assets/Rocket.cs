using System;
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
		Thrust();
		Rotate();

	}

	private void Rotate()
	{
		//        Rotation
		if (Input.GetKey(KeyCode.S))
			transform.Rotate(Vector3.forward, -Rotationspeed * Time.deltaTime);
		else if (Input.GetKey(KeyCode.A))
			transform.Rotate(Vector3.forward, Rotationspeed * Time.deltaTime);

		//        Restart With Key R
		if (Input.GetKey(KeyCode.R))
		{
			transform.position = new Vector3(0, 3.043795f, 0);
			transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		}
	}

	private void Thrust()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			rb.AddRelativeForce(Vector3.up * ThrustForce * Time.deltaTime);
			if (!GetComponent<AudioSource>().isPlaying)
				GetComponent<AudioSource>().Play();
		}
		else if (GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Stop();

	}
}
