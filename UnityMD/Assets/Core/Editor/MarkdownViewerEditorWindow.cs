using UnityEditor;
using UnityEngine;

public class MarkdownViewerEditorWindow : EditorWindow
{
    private string _markdownText;
    private MarkdownParser _parser;
    
    [MenuItem("Tools/Markdown Viewer")]
    public static void GetWindow() 
    {
        MarkdownViewerEditorWindow window = GetWindow<MarkdownViewerEditorWindow>("Markdown Viewer");
        window.minSize = new Vector2(400, 300);
        var renderer = new EditorRenderer();
        window._parser = new MarkdownParser(renderer);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Markdown Viewer", EditorStyles.boldLabel);
        GUILayout.Space(10);

        // Display a text area for markdown input
        _markdownText = EditorGUILayout.TextArea(_markdownText, GUILayout.Height(50));
        
        if (string.IsNullOrEmpty(_markdownText)) return;
        _parser.RenderMarkdownText(_markdownText.Split('\n'));
    }
}
