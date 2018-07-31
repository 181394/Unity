using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Rocket : MonoBehaviour
{
	private Rigidbody rb;
	private int ThrustForce;

	private bool landed;
	private bool crashed;

	[SerializeField] float thrustForce = 1000f;
	[SerializeField] float Rotationspeed = 142f;
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		landed = true;
		crashed = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Thrust();
		Rotate();
		Restart();
		ToggleGravity();

	}

	private void ToggleGravity()
	{
		if (Input.GetKey(KeyCode.G))
		{
			rb.useGravity = !rb.useGravity;
		}
	}

	void FixedUpdate()
	{
		Landing();
	}




	void OnCollisionEnter(Collision collision)
	{
		switch (collision.gameObject.tag)
		{
			case "Friend":
				break;
			case "Obsticle":
				crashed = true;
				break;
			
		}
	}


	void Landing()
	{
	   
		if (Input.GetKey(KeyCode.L) && (rb.rotation.z > 0.01f || rb.rotation.z < -0.01f))
		{
			landed = false;}

		if (!landed)
		{
		    rb.constraints = RigidbodyConstraints.FreezeAll;
		    rb.constraints = RigidbodyConstraints.FreezeRotation;
			rb.freezeRotation = true;
			print("LAND");
			if (rb.rotation.z > 0.01f)
			{
				transform.Rotate(Vector3.back, 160* Time.deltaTime);
			}else if (rb.rotation.z < -0.01f)
			{
				transform.Rotate(Vector3.back, -160 * Time.deltaTime);
			}
			else
			{
				landed = true;
			}
		}
	}


	private void Rotate()
	{
		rb.freezeRotation = true;
		//        Rotation
		if (Input.GetKey(KeyCode.S))
		{

			transform.Rotate(Vector3.forward, -Rotationspeed * Time.deltaTime);
		}else if (Input.GetKey(KeyCode.A))
		{

			transform.Rotate(Vector3.forward, Rotationspeed * Time.deltaTime);
		}
		rb.freezeRotation = false;

	}

	private void Restart()
	{
		if (Input.GetKey(KeyCode.R) || crashed)
		{
			landed = true;
			crashed = false;
			rb.freezeRotation = true;
			transform.position = new Vector3(0, 3.043795f, 0);
			transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		}
	}

	private void Thrust()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
			if (!GetComponent<AudioSource>().isPlaying)
				GetComponent<AudioSource>().Play();
		}
		else if (GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Stop();

	}
}
