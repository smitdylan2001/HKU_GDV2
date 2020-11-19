using UnityEngine;

[CreateAssetMenu(fileName = "VariableFloat_", menuName = "Variables/VariableFloat")]
public class VariableFloat : BaseScriptableObject
{
    //Old value, New value
    public System.Action<float, float> OnValueChanged;
    [SerializeField] private float value;
    public float Value
    {
        get { return value; }
        set
        {
            OnValueChanged?.Invoke(this.value, value); 
            this.value = value;
        }
    }
}
