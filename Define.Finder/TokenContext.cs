using Define.Common.Extensions;
using Define.Common.Helpers;
using Define.Tokenizer.Model;
using System.Collections.Generic;
using System.Linq;

namespace Define.Tokenizer
{
    public class TokenContext
    {
        private readonly Dictionary<string, Token> _definitions;

        private const string Ending = "(s)";

        public TokenContext()
        {
            _definitions = new Dictionary<string, Token>();
        }

        public void Add(string text, int startParagraphIndex, int endParagraphIndex, TokenType tokenType)
        {
            text = text.ToSingularForm();

            LogHelper.Debug($"Definition '{text}' found in boundaries {startParagraphIndex}-{endParagraphIndex}");

            _definitions.TryGetValue(text, out var definition);

            if (definition == null)
            {
                LogHelper.Debug($"Definition '{text}' has not been stored before. Storing...");

                var newDefinition = new Token
                {
                    Name = text,
                };

                newDefinition.TokenInfos.Add(new TokenInfo(startParagraphIndex, endParagraphIndex, tokenType));

                _definitions[text] = newDefinition;
            }
            else
            {

                if (definition.TokenInfos.Any(t => startParagraphIndex >= t.StartParagraphIndex && endParagraphIndex <= t.EndParagraphIndex))
                {
                    LogHelper.Debug($"Definition '{text}' and its range has been stored before.");

                    return;
                }

                LogHelper.Debug($"Definition '{text}' has been stored before. Storing points.");

                definition.TokenInfos.Add(new TokenInfo(startParagraphIndex, endParagraphIndex, tokenType));
            }

            if (text.EndsWith(Ending))
            {
                var indexOfEnding = text.LastIndexOf(Ending);
                text = text.Substring(0, indexOfEnding);
                Add(text, startParagraphIndex, endParagraphIndex, tokenType);
            }
        }

        public IEnumerable<Token> GetAll()
        {
            return _definitions.Values;
        }

        public Token Get(string name)
        {
            name = name.ToSingularForm();

            _definitions.TryGetValue(name, out var definition);

            return definition;
        }

        public void Clear()
        {
            _definitions.Clear();
        }
    }
}
