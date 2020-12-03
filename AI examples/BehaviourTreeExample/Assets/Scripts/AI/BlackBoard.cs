using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlackBoard : MonoBehaviour
{
	public static bool HasBeenAttacked = false; //TODO: Implement this properly in the behaviour tree 
	public static bool GuardBlinded = false; //TODO: Implement this properly in the utility system
	public static bool PlayerSeen = false; //TODO: Implement this properly

	[SerializeField] public List<FloatValue> FloatVariables = new List<FloatValue>();

	public Dictionary<VariableType, FloatValue> VariableDictionary { get; private set; } = new Dictionary<VariableType, FloatValue>();

	public void OnInitialize()
	{
		VariableDictionary = new Dictionary<VariableType, FloatValue>();
		foreach (var v in FloatVariables)
		{
			VariableDictionary.Add(v.Type, v);
		}
	}

	public FloatValue GetFloatVariableValueFromList(VariableType type)
	{
		var res = FloatVariables.Find(x => x.Type == type);
		return res;
	}
	public FloatValue GetFloatVariableValue(VariableType type)
	{
		if (VariableDictionary.ContainsKey(type))
		{
			return VariableDictionary[type];
		}
		return null;
	}
}

