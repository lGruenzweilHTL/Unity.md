using UnityEngine;

public class EditorRenderer : IMarkdownRenderer
{
    public void Header1(string text)
    {
        GUILayout.Label(text, MarkdownStyles.Header1Style);
    }
    public void Header2(string text)
    {
        GUILayout.Label(text, MarkdownStyles.Header2Style);
    }
    public void Header3(string text)
    {
        GUILayout.Label(text, MarkdownStyles.Header3Style);
    }
    public void Text(string text)
    {
        GUILayout.Label(text, MarkdownStyles.NormalTextStyle);
    }
    public void Link(string text, string url)
    {
        if (GUILayout.Button(text, MarkdownStyles.LinkStyle))
        {
            Application.OpenURL(url);
        }
    }
    public void CodeBlock(string code)
    {
        GUILayout.BeginVertical(MarkdownStyles.CodeBlockBodyStyle);
        GUILayout.Label(code, MarkdownStyles.CodeBlockTextStyle);
        GUILayout.EndVertical();
    }
    public void Quote(string text)
    {
        GUILayout.BeginVertical(MarkdownStyles.QuoteBodyStyle);
        GUILayout.Label(text, MarkdownStyles.QuoteTextStyle);
        GUILayout.EndVertical();
    }
    public void HorizontalRule()
    {
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
    }
    public void Space(float height = 10f)
    {
        GUILayout.Space(height);
    }
    public void ListItem(string text, int level)
    {
        // TODO: bullets based on level
        GUILayout.Label($"\u2022 {text}");
    }
}