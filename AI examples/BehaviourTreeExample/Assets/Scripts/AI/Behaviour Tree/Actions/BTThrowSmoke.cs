using UnityEngine;

public class BTThrowSmoke : BTBaseNode
{
	private GameObject _smoke;
	private GameObject _enemy;

	public BTThrowSmoke(GameObject smoke, GameObject enemy)
	{
		_smoke = smoke;
		_enemy = enemy;
	}

	public override TaskStatus Run()
	{
		Debug.Log("throw");
		_smoke.transform.position = _enemy.transform.position;
		_smoke.SetActive(true);
		
		return TaskStatus.Success;
	}

}
