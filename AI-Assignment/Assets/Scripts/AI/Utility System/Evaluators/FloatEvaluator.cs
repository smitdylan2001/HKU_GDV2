using UnityEngine;

[CreateAssetMenu(fileName = "FloatEvaluator", menuName = "Evaluators/FloatEvaluator")]
public class FloatEvaluator : UtilityEvaluator
{
    private FloatValue _floatValue;

    public override void OnInitialize(BlackBoard bb)
    {
        _floatValue = bb.GetFloatVariableValueFromList(VariableType);
    }

    public override float GetMaxValue()
    {
        return _floatValue.MaxValue;
    }

    public override float GetValue()
    {
        return _floatValue.Value;
    }
}
