using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	private Rigidbody rb;
	private int ThrustForce;

	[SerializeField] float thrustForce = 1000f;
	[SerializeField] float Rotationspeed = 142f;
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Thrust();
		Rotate();
		
	}



	void FixedUpdate()
	{
		Landing();
	}




	void OnCollisionEnter(Collision collision)
	{
		print("Crashed!");
	}


	void Landing()
	{
		if (Input.GetKey(KeyCode.L))
		{
			rb.freezeRotation = true;
			print("LAND");
			if (rb.rotation.z > 0)
			{
				for (float i = rb.rotation.z; i > 0; i-=0.02f)
				{
					print(i);
					Vector3 eulVec = new Vector3(rb.rotation.x, rb.rotation.y, i);

					Quaternion rotQuaternion = Quaternion.Euler(eulVec*Time.deltaTime);
					rb.MoveRotation(rotQuaternion);
				}
			}else if (rb.rotation.z < 0)
			{
				for (float i = rb.rotation.z; i < 0; i+=0.02f)
				{
					print(i);
					Vector3 eulVec = new Vector3(rb.rotation.x, rb.rotation.y, i);

					Quaternion rotQuaternion = Quaternion.Euler(eulVec * Time.deltaTime);
					rb.MoveRotation(rotQuaternion);
				}
			}
		}
	}


	private void Rotate()
	{
		rb.freezeRotation = true;
		//        Rotation
		if (Input.GetKey(KeyCode.S))
			transform.Rotate(Vector3.forward, -Rotationspeed * Time.deltaTime);
		else if (Input.GetKey(KeyCode.A))
			transform.Rotate(Vector3.forward, Rotationspeed * Time.deltaTime);

		rb.freezeRotation = false;

		//        Restart With Key R
		if (Input.GetKey(KeyCode.R))
		{
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
