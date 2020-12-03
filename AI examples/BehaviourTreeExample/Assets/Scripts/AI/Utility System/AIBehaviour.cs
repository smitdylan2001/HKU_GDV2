using System.Linq;
using UnityEngine;

public abstract class AIBehaviour : MonoBehaviour
{
    [SerializeField] public UtilityEvaluator[] Utilities;
    public float IncreasedAmount = 0;
     
    public void OnInitialize(BlackBoard bb)
    {
        foreach(UtilityEvaluator utility in Utilities)
		{
            utility.OnInitialize(bb);
		}
    }

    public float GetNormalizedScore()
    {
        return Mathf.Clamp01(Utilities.ToList().Sum(x => (x.GetNormalizedScore() + IncreasedAmount)) / Utilities.Length);
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void Execute() { }
}
