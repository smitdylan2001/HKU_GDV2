using UnityEngine;

[CreateAssetMenu(fileName = "VariableGameObject_", menuName = "Variables/VariableGameObject")]
public class VariableGameObject : BaseScriptableObject
{
    //Old value, New value
    public System.Action<GameObject, GameObject> OnValueChanged;
    [SerializeField] private GameObject value;
    public GameObject Value
    {
        get { return value; }
        set
        {
            OnValueChanged?.Invoke(this.value, value); 
            this.value = value;
        }
    }
}
