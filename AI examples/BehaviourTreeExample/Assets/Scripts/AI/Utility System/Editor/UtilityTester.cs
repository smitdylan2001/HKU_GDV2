﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class UtilityTester : EditorWindow
{
    private GameObject agent;
    private List<FloatValue> variables = new List<FloatValue>();

    private List<bool> toggleGroups = new List<bool>();
    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/UtilityTester")]
    static void Init() 
    {
        // Get existing open window or if none, make a new one:
        UtilityTester window = (UtilityTester)EditorWindow.GetWindow(typeof(UtilityTester));
        window.Show();
    }

    private void OnEnable()
    {
        
    }

    void Update()
    {
        if (EditorApplication.isPlaying && !EditorApplication.isPaused)
        {
            Repaint();
        }
    }

    void OnGUI()
    {
        agent = EditorGUILayout.ObjectField("Agent", agent, typeof(UnityEngine.GameObject), true) as GameObject;
        if(agent == null) { return; }

        agent.GetComponent<Guard>().OnInitialize();
        
        var aiBehaviours = agent.GetComponents<AIBehaviour>();

        EditorGUILayout.LabelField("Behaviours", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 2;
        foreach (AIBehaviour m in aiBehaviours)
        {
            var rect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight);
            EditorGUI.indentLevel = 3;
            rect.width = rect.width - 150;
            rect.x += 150;
            EditorGUI.ProgressBar(rect, m.GetNormalizedScore(), m.GetType().Name);

        }
        EditorGUI.indentLevel = 0;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Utility Evaluators", EditorStyles.boldLabel);

        foreach (var ai in aiBehaviours)
        {
            EditorGUI.indentLevel = 1;
            EditorGUILayout.LabelField(ai.GetType().Name, EditorStyles.boldLabel);
            var evaluators = ai.Utilities;
            EditorGUI.indentLevel = 3;
            foreach (var ev in evaluators)
            {
                EditorGUILayout.CurveField(ev.VariableType.ToString(), ev.evaluationCurve);
            }
            EditorGUI.indentLevel = 0;
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Variables", EditorStyles.boldLabel);
        EditorGUI.indentLevel = 2;

        var bb = agent.GetComponent<BlackBoard>();
        variables = bb.FloatVariables;
        if (variables != null)
        {
            foreach (var kv in variables)
            {
                kv.Value = EditorGUILayout.Slider(kv.name, kv.Value, kv.MinValue, kv.MaxValue);
            }
        }
        EditorGUI.indentLevel = 0;
    } 
} 
