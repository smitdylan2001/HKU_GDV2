using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid
{
	public Vector3 Velocity;
	public Vector3 Position;
	public List<Transform> Neighbors;

	private GameObject _boidGO;
	private Rigidbody _boidRB;

    public Boid(Vector3 position, Vector3 velocity)
	{
		Position = position;
		Velocity = velocity;

		_boidGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		_boidGO.GetComponent<SphereCollider>().isTrigger = false;
		_boidGO.transform.position = position;
		_boidGO.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

		_boidRB = _boidGO.AddComponent<Rigidbody>();
		_boidRB.useGravity = false;
	}

	//move boid into the right direction
	public void UpdatePhysics()
	{ 
		_boidGO.transform.position = Position;
	}	
}
