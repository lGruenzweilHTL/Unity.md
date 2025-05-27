public class MarkdownParser
{
    private IMarkdownRenderer _renderer;
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
        if (line.StartsWith("###") && line.Length > 5)
        {
            _renderer.Header3(line[4..]);
        }
        else if (line.StartsWith("##") && line.Length > 4)
        {
            _renderer.Header2(line[3..]);
        }
        else if (line.StartsWith("#") && line.Length > 3)
        {
            _renderer.Header1(line[2..]);
        }
        else if (line.StartsWith("---"))
        {
            _renderer.HorizontalRule();
        }
        else
        {
            _renderer.Text(line);
        }
    }
}