using UnityEngine;

public class Boids : MonoBehaviour
{
    [SerializeField] private int _boidAmount = 50;
    [SerializeField] private int _spawnPlace = 10;
    [SerializeField] private int _spawnSpeed = 4;
    [SerializeField] private int _distance = 3;
    [SerializeField] private float _cohesionWeight = 1;
    [SerializeField] private float _seperationnWeight = 1;
    [SerializeField] private float _alignmentWeight = 1;
    private Boid[] _boids;
    private Vector3 v1, v1Sum, v2, v2Sum, v3, v3Sum;
    private GameObject _testObject;


    void Start()
    {
        InitializeBoids(_boidAmount);
        _testObject = GameObject.Find("tes");
    }

    void InitializeBoids(int amount)
	{
        _boids = new Boid[_boidAmount];
        for (int i = 0; i < amount; i++)
		{
            _boids[i] = new Boid(new Vector3(Random.Range(-_spawnPlace, _spawnPlace), Random.Range(-_spawnPlace, _spawnPlace), Random.Range(-_spawnPlace, _spawnPlace)), new Vector3(Random.Range(-_spawnSpeed, _spawnSpeed), Random.Range(-_spawnSpeed, _spawnSpeed), Random.Range(-_spawnSpeed, _spawnSpeed)));
		}
	}

    void FixedUpdate()
    {
        foreach (Boid i in _boids)
		{
            v1 = Cohesion(i);
            v2 = Separation(i);
            v3 = Alignment(i);

            i.Velocity = i.Velocity + v1 + v2 + v3;
            i.Position = i.Position + i.Velocity;

            i.UpdatePhysics();
        }
        _testObject.transform.position = v1Sum;
    }


    Vector3 Cohesion(Boid boid)
	{
        v1Sum = Vector3.zero;

        foreach (Boid i in _boids)
		{
            v1Sum += i.Position;
		}
        v1Sum = v1Sum / (_boidAmount);

        return (v1Sum - boid.Position) / 100 * _cohesionWeight;
	}


    Vector3 Separation(Boid boid)
	{
        v2Sum = Vector3.zero;

        foreach (Boid i in _boids)
		{
            if (boid != i)
			{
                if (Mathf.Abs(i.Position.x - boid.Position.x) < _distance
                    && Mathf.Abs(i.Position.y - boid.Position.y) < _distance
                        && Mathf.Abs(i.Position.z - boid.Position.z) < _distance)
                {
                    v2Sum = v2Sum- (i.Position - boid.Position);
                }
			}
		}

        return v2Sum / 50 * _seperationnWeight;
	}

    Vector3 Alignment(Boid boid)
	{
        v3Sum = Vector3.zero;

        foreach (Boid i in _boids)
		{
            if(boid != i)
			{
                v3Sum += i.Velocity;
			}
		}

        v3Sum = v3Sum / _boidAmount;

        return (v3Sum - boid.Velocity) / 8 * _alignmentWeight;
	}
}
