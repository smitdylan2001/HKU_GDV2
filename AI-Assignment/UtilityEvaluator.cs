using UnityEngine;
public abstract class UtilityEvaluator : BaseScriptableObject
{
    public VariableFloat VariableType;

    [SerializeField] public AnimationCurve evaluationCurve;
    public abstract void OnInitialize();
    public abstract float GetValue();
    public abstract float GetMaxValue();
    public float GetNormalizedScore()
    {
        return Mathf.Clamp01(evaluationCurve.Evaluate(GetValue() / GetMaxValue()));
    }

}
