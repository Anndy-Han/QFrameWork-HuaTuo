using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;

namespace QFrameWork.Editor
{
    [CustomEditor(typeof(App))]
    [CanEditMultipleObjects]
    public class AppEditor: UnityEditor.Editor
    {
        App app;
        private string[] m_ProcedureTypeNames;
        private void OnEnable()
        {
            app = target as App;
            m_ProcedureTypeNames = Assembly.GetTypeNames(typeof(BaseProcedure));
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.LabelField("Base Property");
            EditorGUILayout.HelpBox(string.Format("{0}项目引擎    -EngineVersion: {1}", app.appMate.AppName,app.appMate.EngineVersion), MessageType.Info);

            serializedObject.Update();

            serializedObject.ApplyModifiedProperties();

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                GUILayout.Label("Available Procedures", EditorStyles.boldLabel);
                if (m_ProcedureTypeNames.Length > 0)
                {
                    foreach (string procedureTypeName in m_ProcedureTypeNames)
                    {
                        bool selected = true;
                        if (selected != EditorGUILayout.ToggleLeft(procedureTypeName, selected))
                        {

                        }
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("There is no available procedure.", MessageType.Warning);
                }
            }
        }
    }
}
