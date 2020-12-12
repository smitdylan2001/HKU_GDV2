using UnityEngine;

[CreateAssetMenu(fileName = "FloatValue_" , menuName = "Variables/FloatValue")]
public class FloatValue : BaseScriptableObject
{
    public VariableType Type;

    [SerializeField] private float _value;
    public float Value { get => _value; set => this._value = value; }

    [SerializeField] private float minValue = 0;
    public float MinValue { get { return minValue; } }

    [SerializeField] private float maxValue;
    public float MaxValue { get { return maxValue; } }

    public override void OnReset()
    {
        _value = maxValue;
    }

}
