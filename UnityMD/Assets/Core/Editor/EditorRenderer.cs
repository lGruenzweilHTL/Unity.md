using UnityEngine;

public class EditorRenderer : IMarkdownRenderer
{
    private static void Label(string text, GUIStyle style)
    {
        GUILayout.Label(text, style, GUILayout.ExpandWidth(false));
    }
    public void Header1(string text)
    {
        Label(text, MarkdownStyles.Header1Style);
    }
    public void Header2(string text)
    {
        Label(text, MarkdownStyles.Header2Style);
    }
    public void Header3(string text)
    {
        Label(text, MarkdownStyles.Header3Style);
    }
    public void Text(string text)
    {
        Label(text, MarkdownStyles.NormalTextStyle);
    }

    public void BoldText(string text)
    {
        Label(text, MarkdownStyles.BoldTextStyle);
    }

    public void ItalicText(string text)
    {
        Label(text, MarkdownStyles.ItalicTextStyle);
    }

    public void BoldAndItalicText(string text)
    {
        Label(text, MarkdownStyles.BoldAndItalicTextStyle);
    }

    public void StrikethroughText(string text)
    {
        Label(text, MarkdownStyles.StrikethroughTextStyle);
    }

    public void MonospaceText(string text)
    {
        GUILayout.BeginHorizontal(MarkdownStyles.MonospaceBodyStyle);
        Label(text, MarkdownStyles.CodeBlockTextStyle);
        GUILayout.EndHorizontal();
    }

    public void Link(string text, string url)
    {
        if (GUILayout.Button(text, MarkdownStyles.LinkStyle, GUILayout.ExpandWidth(false)))
        {
            Application.OpenURL(url);
        }
    }
    public void CodeBlock(string[] lines)
    {
        GUILayout.BeginVertical(MarkdownStyles.CodeBlockBodyStyle);
        foreach (var line in lines)
        {
            Label(line, MarkdownStyles.CodeBlockTextStyle);
        }
        GUILayout.EndVertical();
    }
    public void Quote(string text)
    {
        GUILayout.BeginVertical(MarkdownStyles.QuoteBodyStyle);
        Label(text, MarkdownStyles.QuoteTextStyle);
        GUILayout.EndVertical();
    }
    public void HorizontalRule()
    {
        GUILayout.Box("", MarkdownStyles.HorizontalRuleStyle, GUILayout.ExpandWidth(true));
    }
    public void Space(float height = 10f)
    {
        GUILayout.Space(height);
    }
    public void ListItem(string text, int level)
    {
        char[] bullets = {'\u2022', '\u25e6', '\u25ab'};
        char bullet = bullets[Mathf.Clamp(level, 0, bullets.Length - 1)];
        Label($"{bullet} {text}", MarkdownStyles.ListItemStyle);
    }

    public void BeginLine()
    {
        GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
    }

    public void EndLine()
    {
        GUILayout.EndHorizontal();
    }
}