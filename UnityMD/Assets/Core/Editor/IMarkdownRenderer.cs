public interface IMarkdownRenderer
{
    void Header1(string text);
    void Header2(string text);
    void Header3(string text);
    // TODO: more header levels

    void Text(string text);
    void BoldText(string text);
    void ItalicText(string text);
    void BoldAndItalicText(string text);
    void MonospaceText(string text);
    void Link(string text, string url);
    void CodeBlock(string code);
    void Quote(string text);
    void HorizontalRule();
    void Space(float height = 10f);
    void ListItem(string text, int level);

    void BeginLine();
    void EndLine();
}