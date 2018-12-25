namespace Define.Tokenizer.Model
{
    public class TokenInfo
    {
        public TokenInfo(int startParagraphIndex, int endParagraphIndex, TokenType definitionType = default(TokenType))
        {
            StartParagraphIndex = startParagraphIndex;
            EndParagraphIndex = endParagraphIndex;
        }

        public int StartParagraphIndex { get; set; }

        public int EndParagraphIndex { get; set; }
    }
}