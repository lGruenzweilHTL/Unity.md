using System.Linq;
using UnityEngine;

public static class MarkdownStyles
{
    private static GUIStyle RtfLabel => new(GUI.skin.label)
    {
        richText = true,
        wordWrap = true,
        fontSize = 14,
        margin = new RectOffset(0, 0, 0, 0),
        padding = new RectOffset(0, 0, 0, 0),
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
    
    public static GUIStyle StrikethroughTextStyle => new(NormalTextStyle)
    {
        normal = { background = CreateStrikethroughTexture() }
    };
    public static GUIStyle BoldTextStyle => new(NormalTextStyle)
    {
        fontStyle = FontStyle.Bold
    };

    public static GUIStyle ItalicTextStyle => new(NormalTextStyle)
    {
        fontStyle = FontStyle.Italic
    };

    public static GUIStyle BoldAndItalicTextStyle => new(NormalTextStyle)
    {
        fontStyle = FontStyle.BoldAndItalic
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
    public static GUIStyle MonospaceBodyStyle => new(GUI.skin.box)
    {
        padding = new RectOffset(0, 0, 0, 0),
        margin = new RectOffset(0, 0, 0, 0),
    };
    public static GUIStyle CodeBlockTextStyle => new(RtfLabel)
    {
        fontSize = 14,
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
    public static GUIStyle ListItemStyle => new(NormalTextStyle)
    {
        padding = new RectOffset(10, 10, 5, 5),
        margin = new RectOffset(0, 0, 5, 5),
    };

    public static GUIStyle HorizontalRuleStyle => new(GUI.skin.box)
    {
        fixedHeight = 5,
        margin = new RectOffset(0, 0, 10, 10),
        normal = { background = Texture2D.whiteTexture },
    };

    // TODO: tables
    
    private static Texture2D CreateStrikethroughTexture()
    {
        var texture = new Texture2D(5, 5);
        texture.SetPixels(
            Enumerable.Repeat(Color.clear, 10)
                .Concat(Enumerable.Repeat(Color.white, 5))
                .Concat(Enumerable.Repeat(Color.clear, 10))
                .ToArray());
        texture.Apply(); // Apply changes to the texture
        return texture;
    }
}