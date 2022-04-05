using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Text;
using System;
using System.IO;
using System.Reflection;

namespace Quick.Code
{
    public class GenerateCodeWindow : EditorWindow
    {
        [MenuItem("QFrameWork/Generate View Code")]
        public static void OpenWindow()
        {
            if (codeWindow == null)
                codeWindow = EditorWindow.GetWindow(typeof(GenerateCodeWindow)) as GenerateCodeWindow;

            codeWindow.titleContent = new GUIContent("QuickCode");
            codeWindow.Show();
        }
        private static GenerateCodeWindow codeWindow = null;
        private SerializedObject serializedObj;

        //选择的根游戏体
        private GameObject root;
        //ui控件列表
        private List<UIBehaviour> uiWidgets = new List<UIBehaviour>();
        //ui游戏对象列表
        private List<GameObject> uiObjects = new List<GameObject>();
        //视图宽度一半
        private float halfViewWidth;
        //视图高度一半
        private float halfViewHeight;

        private Vector2 scrollWidgetPos;
        private Vector2 scrollObjectPos;
        private Vector2 scrollTextPos;

        private int selectedBar = 0;
        private bool isMono = true;

        #region 代码生成
        private StringBuilder codeStateText;
        private StringBuilder codeEventText;
        private StringBuilder codeAssignText;
        private StringBuilder codeAllText;

        //缓存所有变量名和对应控件对象，对重名作处理
        private Dictionary<string, object> variableNameDic = new Dictionary<string, object>();
        //变量编号
        private int variableNum;
        //需要注册事件的控件,可通过toggle选择
        private Dictionary<string, bool> selectedEventWidgets = new Dictionary<string, bool>();
        //UI 类名
        private string className;
        //生成脚本的类型
        private Type scriptType;
        #endregion

        #region 代码格式分类
        private string methodStartFmt { get { return CodeConfig.methodStartFmt; } }
        private string methodEnd { get { return CodeConfig.methodEnd; } }
        private string assignCodeFmt { get { return CodeConfig.assignCodeFmt; } }
        private string assignGameObjectCodeFmt { get { return CodeConfig.assignGameObjectCodeFmt; } }
        private string assignRootCodeFmt { get { return CodeConfig.assignRootCodeFmt; } }
        private string onClickSerilCode { get { return CodeConfig.onClickSerilCode; } }
        private string onValueChangeSerilCode { get { return CodeConfig.onValueChangeSerilCode; } }
        private string btnCallbackSerilCode { get { return CodeConfig.btnCallbackSerilCode; } }
        private string eventCallbackSerilCode { get { return CodeConfig.eventCallbackSerilCode; } }
        #endregion

        void OnEnable()
        {
            serializedObj = new SerializedObject(this);
        }

        void OnGUI()
        {
            serializedObj.Update();

            if (codeWindow == null)
            {
                codeWindow = GetWindow<GenerateCodeWindow>();
            }
            halfViewWidth = EditorGUIUtility.currentViewWidth / 2f;
            halfViewHeight = codeWindow.position.height / 2f;

            using (new EditorGUILayout.HorizontalScope())
            {
                //左半部分
                using (EditorGUILayout.VerticalScope vScope = new EditorGUILayout.VerticalScope(GUILayout.Width(EditorGUIUtility.currentViewWidth * 0.5f)))
                {
                    GUI.backgroundColor = Color.white;
                    Rect rect = vScope.rect;
                    rect.height = codeWindow.position.height;
                    GUI.Box(rect, "");

                    DrawSelectUI();
                    DrawFindWidget();
                    DrawWidgetList();
                    DrawCustomObjectList();
                }
                //右半部分
                using (new EditorGUILayout.VerticalScope(GUILayout.Width(EditorGUIUtility.currentViewWidth * 0.5f)))
                {
                    DrawCodeGenTitle();
                    DrawCodeGenToolBar();
                }
            }
            serializedObj.ApplyModifiedProperties();
        }

        /// <summary>
        /// 绘制 选择要分析的UI
        /// </summary>
        private void DrawSelectUI()
        {
            EditorGUILayout.Space();
            using (EditorGUILayout.HorizontalScope hScope = new EditorGUILayout.HorizontalScope())
            {
                GUI.backgroundColor = Color.white;
                Rect rect = hScope.rect;
                rect.height = EditorGUIUtility.singleLineHeight;
                GUI.Box(rect, "");

                EditorGUILayout.LabelField("选择待处理UI:", GUILayout.Width(halfViewWidth / 4f));
                GameObject lastRoot = root;
                root = EditorGUILayout.ObjectField(root, typeof(GameObject), true) as GameObject;

                if (lastRoot != null && lastRoot != root)
                {
                    uiWidgets.Clear();
                    uiObjects.Clear();
                }
            }
        }

        /// <summary>
        /// 绘制 查找UI控件
        /// </summary>
        private void DrawFindWidget()
        {
            EditorGUILayout.Space();
            using (EditorGUILayout.HorizontalScope hScope = new EditorGUILayout.HorizontalScope())
            {
                GUI.backgroundColor = Color.white;
                Rect rect = hScope.rect;
                rect.height = EditorGUIUtility.singleLineHeight;
                GUI.Box(rect, "");

                if (GUILayout.Button("查找UI控件", GUILayout.Width(halfViewWidth / 2f)))
                {
                    if (root == null)
                    {
                        Debug.LogWarning("请先选择一个UI物体!");
                        return;
                    }

                    RecursiveUI(root.transform, (tran) =>
                    {
                        UIBehaviour[] widgets = tran.GetComponents<UIBehaviour>();
                        for (int i = 0; i < widgets.Length; i++)
                        {
                            var widget = widgets[i];
                            if (widget != null && !uiWidgets.Contains(widget))
                            {
                                uiWidgets.Add(widget);
                            }
                        }
                    });
                }

                if (GUILayout.Button("清除控件"))
                {
                    uiWidgets.Clear();
                }
                if (GUILayout.Button("清除其他"))
                {
                    uiObjects.Clear();
                }
            }
        }

        /// <summary>
        /// 绘制 控件列表
        /// </summary>
        private void DrawWidgetList()
        {
            EditorGUILayout.Space();;
        }

        private UIBehaviour DrawWidget(Rect position, UIBehaviour item)
        {
            item = (UIBehaviour)EditorGUI.ObjectField(position, item, typeof(UIBehaviour), true);
            return item;
        }

        /// <summary>
        /// 绘制 其他ui gameobject,比如某些节点要控制下层的隐藏显示
        /// </summary>
        private void DrawCustomObjectList()
        {
            EditorGUILayout.Space();
        }

        private GameObject DrawCustomObject(Rect position, GameObject item)
        {
            item = (GameObject)EditorGUI.ObjectField(position, item, typeof(GameObject), true);
            return item;
        }

        private void DrawCodeGenTitle()
        {
            EditorGUILayout.Space();
            using (var hScope = new EditorGUILayout.HorizontalScope(GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                GUI.backgroundColor = Color.white;
                Rect rect = hScope.rect;
                GUI.Box(rect, "");

                EditorGUILayout.LabelField("代码生成:");
            }
        }

        private void DrawCodeGenToolBar()
        {
            EditorGUILayout.Space();
            selectedBar = GUILayout.Toolbar(selectedBar, new string[] { "C#"});
            switch (selectedBar)
            {
                case 0:
                    DrawCsPage();
                    break;
                default:
                    break;
            }
        }

        private void DrawCsPage()
        {
            EditorGUILayout.Space();
            isMono = GUILayout.Toggle(isMono, "继承MonoBehaviour");
            EditorGUILayout.Space();
            if (GUILayout.Button("变量声明", GUILayout.Width(halfViewWidth / 3f)))
            {
                BuildStatementCode();
            }

            EditorGUILayout.Space();
            using (EditorGUILayout.VerticalScope vScope = new EditorGUILayout.VerticalScope())
            {
                GUI.backgroundColor = Color.white;
                GUI.Box(vScope.rect, "");

                EditorGUILayout.LabelField("选择需要注册事件回调的控件:");
                DrawEventWidget();

                EditorGUILayout.Space();
                if (GUILayout.Button("注册事件", GUILayout.Width(halfViewWidth / 3f)))
                {
                    BuildEventCode();
                }
            }

            EditorGUILayout.Space();
            using (EditorGUILayout.HorizontalScope hScope = new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("查找赋值"))
                {
                    BuildAssignmentCode();
                }

                if (GUILayout.Button("复制代码"))
                {
                    TextEditor p = new TextEditor();
                    codeAllText = new StringBuilder(codeStateText.ToString());
                    codeAllText.Append(codeAssignText);
                    codeAllText.Append(codeEventText);
                    p.text = codeAllText.ToString();
                    p.OnFocus();
                    p.Copy();

                    EditorUtility.DisplayDialog("提示", "代码复制成功", "OK");
                }
                if (GUILayout.Button("生成脚本"))
                {
                    CreateCsUIScript();
                }
            }
            EditorGUILayout.Space();
            DrawPreviewText();
        }

        /// <summary>
        /// 遍历UI
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="callback">回调</param>
        public void RecursiveUI(Transform parent, UnityAction<Transform> callback)
        {
            if (callback != null)
                callback(parent);

            if (parent.childCount >= 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i);

                    RecursiveUI(child, callback);
                }
            }
        }

        private string BuildStatementCode()
        {
            variableNum = 0;
            variableNameDic.Clear();

            codeStateText = null;
            codeStateText = new StringBuilder();

            //控件列表
            for (int i = 0; i < uiWidgets.Count; i++)
            {
                if (uiWidgets[i] == null) continue;

                Type type = uiWidgets[i].GetType();
                if (type == null)
                {
                    Debug.LogError("BuildUICode type error !");
                    return "";
                }
                string typeName = type.Name;
                if (typeName =="Widget")
                {
                    continue;
                }
                string variableName = uiWidgets[i].name.ToLower() + typeName;
                ++variableNum;
                if (variableNameDic.ContainsKey(variableName))
                {
                    variableName += variableNum;
                }
                variableNameDic.Add(variableName, uiWidgets[i]);
                codeStateText.AppendFormat(CodeConfig.stateCodeFmt, typeName, variableName);
            }

            for (int i = 0; i < uiObjects.Count; i++)
            {
                if (uiObjects[i] == null) continue;

                Type type = uiObjects[i].GetType();
                if (type == null)
                {
                    Debug.LogError("BuildUICode type error !");
                    return "";
                }

                string typeName = type.Name;
                string variableName = string.Format("go_{0}", uiObjects[i].name);
                variableName = variableName.Replace(' ', '_');   //命名有空格的情况
                //重名处理
                ++variableNum;
                if (variableNameDic.ContainsKey(variableName))
                {
                    variableName += variableNum;
                }
                variableNameDic.Add(variableName, uiObjects[i]);
                codeStateText.AppendFormat(CodeConfig.stateCodeFmt, typeName, variableName);
            }
            return codeStateText.ToString();
        }

        private void DrawEventWidget()
        {
            using (EditorGUILayout.HorizontalScope hScope = new EditorGUILayout.HorizontalScope())
            {
                //筛选当前UI的事件控件
                foreach (var elem in Enum.GetValues(typeof(CodeConfig.EventWidgetType)))
                {
                    for (int i = 0; i < uiWidgets.Count; i++)
                    {
                        if (uiWidgets[i] == null) continue;

                        Type type = uiWidgets[i].GetType();
                        if (type == null)
                        {
                            Debug.LogError("BuildUICode type error !");
                            continue;
                        }

                        if (type.Name == elem.ToString() && !selectedEventWidgets.ContainsKey(type.Name))
                        {
                            selectedEventWidgets.Add(type.Name, true);
                        }
                    }
                }

                //绘制toggle,注意不能遍历dic的同时赋值
                List<string> list = new List<string>(selectedEventWidgets.Keys);
                foreach (string wedagetName in list)
                {
                    selectedEventWidgets[wedagetName] = EditorGUILayout.ToggleLeft(wedagetName, selectedEventWidgets[wedagetName],
                        GUILayout.Width(halfViewWidth / 8f));
                }
            }
        }

        /// <summary>
        /// 构建注册控件事件的代码
        /// </summary>
        /// <returns></returns>
        private string BuildEventCode()
        {
            codeEventText = null;
            codeEventText = new StringBuilder();

            codeEventText.AppendFormat(methodStartFmt, "AddEvent");

            bool hasEventWidget = false;    //标识是否有控件注册了事件
            for (int i = 0; i < uiWidgets.Count; i++)
            {
                if (uiWidgets[i] == null) continue;

                //剔除不是事件或者是事件但未勾选toggle的控件
                string typeName = uiWidgets[i].GetType().Name;
                if (!selectedEventWidgets.ContainsKey(typeName) || !selectedEventWidgets[typeName])
                {
                    continue;
                }

                foreach (var vName in variableNameDic.Keys)
                {
                    if (uiWidgets[i].Equals(variableNameDic[vName]))
                    {
                        string variableName = vName;
                        if (!string.IsNullOrEmpty(variableName))
                        {
                            string methodName = variableName.Substring(variableName.IndexOf('_') + 1);
                            if (uiWidgets[i] is Button)
                            {
                                string onClickStr = string.Format(onClickSerilCode, variableName, methodName);
                                if (hasEventWidget)
                                {
                                    string str = codeEventText.ToString();
                                    codeEventText.Insert(str.LastIndexOf(';') + 1, "\n" + onClickStr);
                                }
                                else
                                {
                                    codeEventText.Append(onClickStr);
                                }
                                codeEventText.AppendFormat(btnCallbackSerilCode, methodName);

                                hasEventWidget = true;
                            }
                            else
                            {
                                string addEventStr = string.Format(onValueChangeSerilCode, variableName, methodName);
                                if (hasEventWidget)
                                {
                                    codeEventText.Insert(codeEventText.ToString().LastIndexOf(';') + 1, addEventStr);
                                }
                                else
                                {
                                    codeEventText.Append(addEventStr);
                                }

                                string paramType = "";
                                foreach (string widgetType in CodeConfig.eventCBParamDic.Keys)
                                {
                                    if (typeName == widgetType)
                                    {
                                        paramType = CodeConfig.eventCBParamDic[widgetType];
                                        break;
                                    }
                                }

                                if (!string.IsNullOrEmpty(paramType))
                                {
                                    codeEventText.AppendFormat(eventCallbackSerilCode, methodName, paramType);
                                }

                                hasEventWidget = true;
                            }
                        }
                        break;
                    }
                }
            }

            string codeStr = codeEventText.ToString();
            if (hasEventWidget)
            {
                codeEventText.Insert(codeStr.LastIndexOf(';') + 1, methodEnd);
            }
            else
            {
                codeEventText.Append(methodEnd);
            }
            return codeEventText.ToString();
        }

        /// <summary>
        /// 当前操作生成的代码预览
        /// </summary>
        private void DrawPreviewText()
        {
            EditorGUILayout.Space();

            using (var ver = new EditorGUILayout.VerticalScope())
            {
                GUI.backgroundColor = Color.white;
                GUI.Box(ver.rect, "");

                EditorGUILayout.HelpBox("代码预览:", MessageType.None);
                using (var scr = new EditorGUILayout.ScrollViewScope(scrollTextPos))
                {
                    scrollTextPos = scr.scrollPosition;

                    if (codeStateText != null && !string.IsNullOrEmpty(codeStateText.ToString()) && selectedBar == 0)
                    {
                        //GUILayout.TextArea(codeStateText.ToString());
                        GUILayout.Label(codeStateText.ToString());
                    }

                    if (codeAssignText != null && !string.IsNullOrEmpty(codeAssignText.ToString()))
                    {
                        GUILayout.Label(codeAssignText.ToString());
                    }

                    if (codeEventText != null && !string.IsNullOrEmpty(codeEventText.ToString()))
                    {
                        //GUILayout.TextArea(codeEventText.ToString());
                        GUILayout.Label(codeEventText.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 生成C# UI脚本
        /// </summary>
        private void CreateCsUIScript()
        {
            string path = EditorPrefs.GetString("create_script_folder", "");
            path = EditorUtility.SaveFilePanel("Create Script", path, root.name + ".cs", "cs");
            if (string.IsNullOrEmpty(path)) return;

            int index = path.LastIndexOf('/');
            className = path.Substring(index + 1, path.LastIndexOf('.') - index - 1);

            StringBuilder scriptBuilder = new StringBuilder();
            scriptBuilder.Append(CodeConfig.usingNamespace);
            if (isMono)
            {
                scriptBuilder.AppendFormat(CodeConfig.classMonoStart, className);
            }
            else
            {
                scriptBuilder.AppendFormat(CodeConfig.classQStart, className);
                scriptBuilder.AppendFormat(CodeConfig.classCtorStart, className);
                scriptBuilder.Append(CodeConfig.classCtorEnd);
            }
            scriptBuilder.Append(codeStateText);
            scriptBuilder.Append(codeAssignText);
            scriptBuilder.Append(codeEventText);
            scriptBuilder.Append(CodeConfig.classEnd);

            File.WriteAllText(path, scriptBuilder.ToString(), new UTF8Encoding(false));
            AssetDatabase.Refresh();

            ShowNotification(new GUIContent("脚本生成成功,生成路径为:" + path));
            EditorPrefs.SetString("create_script_folder", path);
        }

        /// <summary>
        /// 给UI变量绑定实例
        /// </summary>
        private void BindSerializeWidget()
        {
            if (EditorApplication.isCompiling)
            {
                EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此操作", "OK");
                return;
            }

            if (root == null || string.IsNullOrEmpty(className))
            {
                EditorUtility.DisplayDialog("警告", "请先按顺序生成UI脚本再执行此操作", "OK");
                return;
            }

            if (scriptType == null)
            {
                EditorUtility.DisplayDialog("警告", "请先按顺序挂载UI脚本再执行此操作", "OK");
                return;
            }

            object target = null;
            target = root.GetComponent(scriptType);
            if (target == null)
            {
                target = root.AddComponent(scriptType);
            }

            //奇怪，资源刷新以后variableNameDic被清空了，先再获取一遍
            BuildStatementCode();

            foreach (var name in variableNameDic.Keys)
            {
                if (string.IsNullOrEmpty(name)) continue;
                scriptType.InvokeMember(name,
                                BindingFlags.SetField |
                                BindingFlags.Instance |
                                BindingFlags.NonPublic,
                                null, target, new object[] { variableNameDic[name] }, null, null, null);
            }
        }


        private void BuildAssignmentCode()
        {
            codeAssignText = new StringBuilder();
            codeAssignText.AppendFormat(methodStartFmt, "InitUI");

            var allPath = GetChildrenPaths(root);

            if (variableNameDic == null)
            {
                return;
            }

            //格式：变量名 = transform.Find("").Getcomponent<>();
            foreach (var name in variableNameDic.Keys)
            {
                var obj = variableNameDic[name];
                if (obj == null) continue;

                string path = "";
                bool isRootComponent = false;
                foreach (var tran in allPath.Keys)
                {
                    if (tran == null) continue;

                    UIBehaviour behav = obj as UIBehaviour;
                    if (behav != null)
                    {
                        //判断是否挂在根上，根上不需要路径
                        isRootComponent = behav.gameObject == root;
                        if (isRootComponent) break;

                        if (behav.gameObject == tran.gameObject)
                        {
                            path = allPath[tran];
                            break;
                        }
                    }
                    else
                    {
                        if (tran.gameObject == obj)
                        {
                            path = allPath[tran];
                            break;
                        }
                    }
                }

                if (obj is GameObject)
                {
                    codeAssignText.AppendFormat(assignGameObjectCodeFmt, name, path);
                }
                else
                {
                    if (isRootComponent)
                    {
                        codeAssignText.AppendFormat(assignRootCodeFmt, name, obj.GetType().Name);
                    }
                    else
                    {
                        codeAssignText.AppendFormat(assignCodeFmt, name, path, obj.GetType().Name);
                    }
                }
            }
            codeAssignText.Append(methodEnd);
        }

        private Dictionary<Transform, string> GetChildrenPaths(GameObject rootGo)
        {
            Dictionary<Transform, string> pathDic = new Dictionary<Transform, string>();
            string path = string.Empty;
            Transform[] tfArray = rootGo.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < tfArray.Length; i++)
            {
                Transform node = tfArray[i];

                string str = node.name;
                while (node.parent != null && node.gameObject != rootGo && node.parent.gameObject != rootGo)
                {
                    str = string.Format("{0}/{1}", node.parent.name, str);
                    node = node.parent;
                }
                path += string.Format("{0}\n", str);

                if (!pathDic.ContainsKey(tfArray[i]))
                {
                    pathDic.Add(tfArray[i], str);
                }
            }
            return pathDic;
        }
    }
}
