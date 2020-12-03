using System.Linq;
using UnityEngine;

public abstract class AIBehaviour : MonoBehaviour
{
    [SerializeField] public UtilityEvaluator[] utilities;
    public float IncreasedAmount;
     
    public void OnInitialize(BlackBoard bb)
    {
        foreach(UtilityEvaluator utility in utilities)
		{
            utility.OnInitialize(bb);
		}
    }

    public float GetNormalizedScore()
    {
        return Mathf.Clamp01(utilities.ToList().Sum(x => (x.GetNormalizedScore() + IncreasedAmount)) / utilities.Length);
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void Execute() { }
}
