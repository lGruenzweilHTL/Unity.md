using UnityEngine;

public static class MarkdownStyles
{
    private static GUIStyle RtfLabel => new(GUI.skin.label)
    {
        richText = true,
        wordWrap = true,
        fontSize = 14
    };
    
    public static GUIStyle Header1Style => new(RtfLabel)
    {
        fontSize = 20,
        fontStyle = FontStyle.Bold,
    };

    public static GUIStyle Header2Style => new(RtfLabel)
    {
        fontSize = 18,
        fontStyle = FontStyle.Bold,
    };
    public static GUIStyle Header3Style => new(RtfLabel)
    {
        fontSize = 16,
        fontStyle = FontStyle.Bold,
    };
    public static GUIStyle NormalTextStyle => new(RtfLabel)
    {
        fontSize = 14,
        fontStyle = FontStyle.Normal,
    };
    public static GUIStyle LinkStyle => new(RtfLabel)
    {
        fontSize = 14,
        fontStyle = FontStyle.Normal,
        normal = { textColor = Color.blue },
    };
    public static GUIStyle CodeBlockBodyStyle => new(GUI.skin.box)
    {
        padding = new RectOffset(10, 10, 10, 10),
        margin = new RectOffset(0, 0, 10, 10),
    };
    public static GUIStyle CodeBlockTextStyle => new(RtfLabel)
    {
        fontSize = 14,
        fontStyle = FontStyle.Italic,
        normal = { textColor = Color.gray }
    };
    
    public static GUIStyle QuoteBodyStyle => new(GUI.skin.box)
    {
        padding = new RectOffset(10, 10, 10, 10),
        margin = new RectOffset(0, 0, 10, 10),
    };
    public static GUIStyle QuoteTextStyle => new(RtfLabel)
    {
        fontSize = 14,
        fontStyle = FontStyle.Italic,
        normal = { textColor = Color.gray },
        padding = new RectOffset(10, 10, 10, 10),
        margin = new RectOffset(0, 0, 10, 10),
        border = new RectOffset(1, 1, 1, 1),
    };
    public static GUIStyle ListItemStyle => new(RtfLabel)
    {
        fontSize = 14,
        fontStyle = FontStyle.Normal,
        padding = new RectOffset(10, 10, 5, 5),
        margin = new RectOffset(0, 0, 5, 5),
    };
    
    // TODO: tables
}