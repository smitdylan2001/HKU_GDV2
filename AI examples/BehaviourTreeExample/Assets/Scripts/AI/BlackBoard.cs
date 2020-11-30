using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    public static bool HasBeenAttacked = false; //TODO: Implement this properly in the behaviour tree 

    [SerializeField] private List<FloatValue> floatVariables = new List<FloatValue>();
    public Dictionary<VariableType, FloatValue> VariableDictionary { get; private set; } = new Dictionary<VariableType, FloatValue>();

    public void OnInitialize()
    {
        VariableDictionary = new Dictionary<VariableType, FloatValue>();
        foreach (var v in floatVariables)
        {
            VariableDictionary.Add(v.Type, v);
        }
    }

    public FloatValue GetFloatVariableValue(string name)
    {
        var res = floatVariables.Find(x => x.name == name);
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

