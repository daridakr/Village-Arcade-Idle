using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ListSOProgress))]
public class SOListProgressEditor : Editor
{
    private const string LockKey = "LockEditProgressKey";

    private ListSOProgress _progress;
    private bool _lock;

    private void Awake()
    {
        _progress = target as ListSOProgress;
        _progress.Load();

        _lock = EditorPrefs.GetBool(LockKey, false);
    }

    public override void OnInspectorGUI()
    {
        _lock = EditorGUILayout.Toggle("Lock edit", _lock);

        EditorPrefs.SetBool(LockKey, _lock);
        if (_lock)
            GUI.enabled = false;

        base.OnInspectorGUI();
        GUI.enabled = true;

        GUILayout.Space(10);

        serializedObject.Update();

        try
        {
            GUI.enabled = false;
            EditorGUILayout.TextField("List Progress: " + _progress.CurrentProgress);

            GUILayout.Space(15);

            EditorGUILayout.TextField("Save Data:");
            EditorGUI.indentLevel++;

            var settingsArr = new UnityEngine.Object[_progress.Data.Count()];
            int i = 0;

            foreach (var data in _progress.Data)
            {
                settingsArr[i] = data;
                settingsArr[i] = EditorGUILayout.ObjectField(data, typeof(UnityEngine.Object), false);
                i++;
            }

            EditorGUI.indentLevel--;
            GUI.enabled = true;
        }
        catch (Exception _) { }
    }
}
