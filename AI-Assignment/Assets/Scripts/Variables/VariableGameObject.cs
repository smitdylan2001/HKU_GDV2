using UnityEngine;

[CreateAssetMenu(fileName = "VariableGameObject_", menuName = "Variables/VariableGameObject")]
public class VariableGameObject : BaseScriptableObject
{
    //Old value, New value
    public System.Action<GameObject, GameObject> OnValueChanged;
    [SerializeField] private GameObject _value;

    public GameObject Value
    {
        get { return _value; }
        set
        {
            OnValueChanged?.Invoke(this._value, value); 
            this._value = value;
        }
    }

	public override void OnReset()
	{
		
	}
}
