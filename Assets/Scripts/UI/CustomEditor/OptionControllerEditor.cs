using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector.Editor;

[CustomEditor(typeof(OptionController))]
public class OptionControllerEditor : OdinEditor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        OptionController controller = (OptionController)target;

        EditorGUILayout.Space();
        if (controller.useEnum)
        {
            controller.enumString = EditorGUILayout.TextField("Enum", controller.enumString);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = assembly.GetType(controller.enumString);
                if (type == null)
                    continue;
                if (type.IsEnum)
                {
                    List<string> values = new List<string>();
                    string[] names = type.GetEnumNames();
                    controller.sliderMax = names.Length - 1;
                    if (controller.hideIndex != -1)
                        controller.sliderMax -= 1;
                    controller.sliderMin = 0;
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (controller.hideIndex == i)
                            continue;
                        values.Add(names[i]);
                    }
                    controller.customValues = values;
                }
            }
        }
    }
}
#endif