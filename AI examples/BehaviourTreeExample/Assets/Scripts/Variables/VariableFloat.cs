using UnityEngine;

[CreateAssetMenu(fileName = "VariableFloat_", menuName = "Variables/VariableFloat")]
public class VariableFloat : BaseScriptableObject
{
    //Old value, New value
    public System.Action<float, float> OnValueChanged;
    [SerializeField] private float _value;
    public float Value
    {
        get { return _value; }
        set
        {
            OnValueChanged?.Invoke(this._value, value); 
            this._value = value;
        }
    }

	public override void OnReset() { }
}
