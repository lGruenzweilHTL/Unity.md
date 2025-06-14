using System;
using System.Collections.Generic;

public class MarkdownParser
{
    private readonly IMarkdownRenderer _renderer;
    
    private bool _openBold, _openItalic;

    public MarkdownParser(IMarkdownRenderer renderer)
    {
        _renderer = renderer;
    }

    public void RenderMarkdownText(string[] lines)
    {
        bool inCodeBlock = false;
        List<string> currentCodeBlock = new();
        foreach (var line in lines)
        {
            if (line == "```" && inCodeBlock)
            {
                // End of code block
                inCodeBlock = false;
                _renderer.CodeBlock(currentCodeBlock.ToArray());
                currentCodeBlock.Clear();
                continue;
            }
            else if (line.StartsWith("```"))
            {
                // Start of code block
                inCodeBlock = true;
                continue;
            }
            if (inCodeBlock)
            {
                currentCodeBlock.Add(line);
                continue;
            }

            RenderLine(line);
        }
        
        // Clean up any remaining code block lines
        if (currentCodeBlock.Count > 0)
        {
            _renderer.CodeBlock(currentCodeBlock.ToArray());
            currentCodeBlock.Clear();
        }
    }

    private void RenderLine(string line)
    {
        if (line.StartsWith("###") && line.Length >= 5)
        {
            _renderer.Header3(line[4..]);
        }
        else if (line.StartsWith("##") && line.Length >= 4)
        {
            _renderer.Header2(line[3..]);
        }
        else if (line.StartsWith("#") && line.Length >= 3)
        {
            _renderer.Header1(line[2..]);
        }
        else if (line.StartsWith("---"))
        {
            _renderer.HorizontalRule();
        }
        else if (string.IsNullOrWhiteSpace(line))
        {
            _renderer.Space();
        }
        else if (line.StartsWith(">"))
        {
            _renderer.Quote(line[1..].Trim());
        }
        else
        {
            // Unordered lists
            if (line.StartsWith('-')) line = " \u2022 " + line[1..];
            else if (line.StartsWith("\t-")) line = "    \u25e6 " + line[2..];
            else if (line.StartsWith("\t\t-")) line = "       \u25ab " + line[3..];
            
            // Ordered lists
            line = HandleOrderedLists(line);
            RenderText(line);
        }
    }

    private static string HandleOrderedLists(string line)
    {
        int numTabs = 0;
        while (numTabs < line.Length && line[numTabs] == '\t') numTabs++;
        if (numTabs >= 0 && line.Length > numTabs + 2 && char.IsDigit(line[numTabs]) && line[numTabs + 1] == '.')
        {
            return $"{new string(' ', (numTabs * 3) + 1)}{line[numTabs]}. {line[(numTabs + 2)..]}";
        }

        return line;
    }

    private void RenderText(string text)
    {
        _renderer.BeginLine();
        string textToRender = "";

        for (var i = 0; i < text.Length; i++)
        {
            // Strikethrough text
            if (text[i] == '~')
            {
                _renderer.Text(textToRender);
                textToRender = "";
                int endIndex = text.IndexOf("~", i + 1, StringComparison.Ordinal);
                if (endIndex != -1)
                {
                    _renderer.StrikethroughText(text.Substring(i + 1, endIndex - i - 1));
                    i = endIndex; // Move past the closing '~'
                }
                else
                {
                    textToRender += text[i]; // If no closing '~', treat as normal text
                }
            }
            // Bold text
            else if (text[i] == '*' && i + 1 < text.Length && text[i + 1] == '*')
            {
                _openBold = !_openBold;
                textToRender += _openBold ? "<b>" : "</b>";
                i++; // Skip the next '*'
            }
            // Italic text
            else if (text[i] == '*')
            {
                _openItalic = !_openItalic;
                textToRender += _openItalic ? "<i>" : "</i>";
            }
            // Links
            else if (text[i] == '[')
            {
                _renderer.Text(textToRender);
                textToRender = "";
                if (!RenderLink(ref i, text)) textToRender += text[i];
            }
            // Monospace text
            else if (text[i] == '`')
            {
                _renderer.Text(textToRender);
                textToRender = "";
                int endIndex = text.IndexOf('`', i + 1);
                if (endIndex != -1)
                {
                    _renderer.MonospaceText(text.Substring(i + 1, endIndex - i - 1));
                    i = endIndex; // Move past the closing backtick
                }
                else
                {
                    textToRender += text[i]; // If no closing backtick, treat as normal text
                }
            }
            // Normal text
            else
            {
                textToRender += text[i];
            }
        }
        
        // TODO: code blocks, quotes, underlines, tables, checkbox lists, info boxes, images
        if (!string.IsNullOrWhiteSpace(textToRender))
            _renderer.Text(textToRender);
        
        _renderer.EndLine();
    }
    
    private bool RenderLink(ref int currentIndex, string text)
    {
        int endIndex = text.IndexOf(')', currentIndex);
        if (endIndex == -1) return false;

        string linkText = text.Substring(currentIndex, endIndex - currentIndex);
        int urlStart = linkText.IndexOf('[');
        int urlEnd = linkText.IndexOf(']');

        if (urlStart == -1 || urlEnd == -1 || urlEnd <= urlStart) return false;

        string displayText = linkText.Substring(urlStart + 1, urlEnd - urlStart - 1);
        string url = linkText[(urlEnd + 2)..].Trim();

        _renderer.Link(displayText, url);
        currentIndex = endIndex; // Move past the closing parenthesis
        return true;
    }
}