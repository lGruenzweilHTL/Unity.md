using System;
using System.Linq;

public class MarkdownParser
{
    private readonly IMarkdownRenderer _renderer;

    public MarkdownParser(IMarkdownRenderer renderer)
    {
        _renderer = renderer;
    }

    public void RenderMarkdownText(string[] lines)
    {
        foreach (var line in lines)
        {
            RenderLine(line);
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
        else if (line.StartsWith("-") && line.Length >= 2)
        {
            _renderer.ListItem(line[1..], 0);
        }
        else
        {
            RenderText(line);
        }
    }

    private void RenderText(string text)
    {
        _renderer.BeginLine();
        int currentIndex = 0;

        while (currentIndex < text.Length)
        {
            if (text[currentIndex] == '*' && currentIndex + 1 < text.Length && text[currentIndex + 1] == '*')
            {
                // Bold text
                int endIndex = text.IndexOf("**", currentIndex + 2, StringComparison.Ordinal);
                if (endIndex != -1)
                {
                    _renderer.BoldText(text[(currentIndex + 2)..endIndex]);
                    currentIndex = endIndex + 2;
                    continue;
                }
            }
            else if (text[currentIndex] == '*')
            {
                // Italic text
                int endIndex = text.IndexOf('*', currentIndex + 1);
                if (endIndex != -1)
                {
                    _renderer.ItalicText(text[(currentIndex + 1)..endIndex]);
                    currentIndex = endIndex + 1;
                    continue;
                }
            }
            else if (text[currentIndex] == '[')
            {
                RenderLink(ref currentIndex, text);
                continue;
            }

            // Handle unmatched or plain `*`
            if (text[currentIndex] == '*')
            {
                _renderer.Text("*");
                currentIndex++;
                continue;
            }

            // Render normal text
            int nextSpecialChar = text.IndexOfAny(new[] { '*', '[', ']' }, currentIndex);
            if (nextSpecialChar == -1)
            {
                _renderer.Text(text[currentIndex..]);
                break;
            }
            else
            {
                _renderer.Text(text[currentIndex..nextSpecialChar]);
                currentIndex = nextSpecialChar;
            }
        }

        _renderer.EndLine();
    }

    private void RenderLink(ref int currentIndex, string text)
    {
        // [test]
        int closingBracketIndex = text.IndexOf(']', currentIndex);
        if (closingBracketIndex != -1 && closingBracketIndex + 1 < text.Length &&
            text[closingBracketIndex + 1] == '(')
        {
            int closingParenthesisIndex = text.IndexOf(')', closingBracketIndex + 2);
            if (closingParenthesisIndex != -1)
            {
                string linkText = text[(currentIndex + 1)..closingBracketIndex];
                string linkUrl = text[(closingBracketIndex + 2)..closingParenthesisIndex];
                _renderer.Link(linkText, linkUrl);
                currentIndex = closingParenthesisIndex + 1;
            }
            else
            {
                // Handle incomplete link (missing closing parenthesis)
                _renderer.Text(text[currentIndex..(closingBracketIndex + 1)]);
                currentIndex = closingBracketIndex + 1;
            }
        }
        // Handle incomplete link (missing closing bracket)
        else
        {
            currentIndex = text.Length;
        }
    }
}