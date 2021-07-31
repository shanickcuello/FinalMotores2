using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public sealed class MacrosToolBar : EditorWindow, IHasCustomMenu
{
    private const string TITLE = "Toolbar";
    private const float WINDOW_HEIGHT = 24;
    private const float BUTTON_HEIGHT = 20;

    private MacrosToolBarSettings m_settings; //call scriptable object

    [MenuItem("Window/Macros ToolBar")]
    private static void Init()
    {
        var win = GetWindow<MacrosToolBar>(TITLE);

        var pos = win.position;
        pos.height = WINDOW_HEIGHT;
        win.position = pos;

        var minSize = win.minSize;
        minSize.y = WINDOW_HEIGHT;
        win.minSize = minSize;

        var maxSize = win.maxSize;
        maxSize.y = WINDOW_HEIGHT;
        win.maxSize = maxSize;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        var list = m_settings.List.Where(c => c.IsValid); //verifica que el comando sea valido

        foreach (var n in list)
        {
            var commandName = n.CommandName;
            var buttonName = n.ButtonName;
            var image = n.Image;
            var width = n.Width;
            var content = image != null ? new GUIContent(image) : new GUIContent(buttonName);
            var options = 0 < width
                ? new[] { GUILayout.Width(width), GUILayout.Height(BUTTON_HEIGHT) }
                : new[] { GUILayout.Width(EditorStyles.label.CalcSize(new GUIContent(buttonName)).x + 14), GUILayout.Height(BUTTON_HEIGHT) }
            ;
            if (GUILayout.Button(content, options))
            {
                EditorApplication.ExecuteMenuItem(commandName);
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    private void OnEnable()
    {
        //Toma el path del macro
        var mono = MonoScript.FromScriptableObject(this);
        var scriptPath = AssetDatabase.GetAssetPath(mono);
        var dir = Path.GetDirectoryName(scriptPath);
        var path = string.Format("{0}/Settings.asset", dir);

        m_settings = AssetDatabase.LoadAssetAtPath<MacrosToolBarSettings>(path);
    }

    public void AddItemsToMenu(GenericMenu menu)
    {
        menu.AddItem
        (
            new GUIContent("Settings"),
            false,
            () => EditorGUIUtility.PingObject(m_settings)
        );
    }
}
