using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid
{
	private GameObject _boidGO;
	private Rigidbody _boidRB;
	public Vector3 Velocity;
	public Vector3 Position;
	public List<Transform> Neighbors;


    public Boid(Vector3 position, Vector3 velocity)
	{
		_boidGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Position = position;
		_boidGO.transform.position = position;
		Velocity = velocity;
		_boidRB = _boidGO.AddComponent<Rigidbody>();
		_boidRB.useGravity = false;
		_boidGO.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		_boidGO.GetComponent<SphereCollider>().isTrigger = false;
	}

	public void UpdatePhysics()
	{
		//_boidRB.velocity = Velocity;
		_boidGO.transform.position = Position;
	}	
}
