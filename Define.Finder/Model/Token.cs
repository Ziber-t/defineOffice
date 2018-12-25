using System.Collections.Generic;

namespace Define.Tokenizer.Model
{
    public class Token
    {
        public string Name { get; set; }

        public List<TokenInfo> TokenInfos { get; set; }

        public Token()
        {
            TokenInfos = new List<TokenInfo>();
        }

        public override int GetHashCode()
        {
            unchecked // overflow is fine, just wrap
            {
                return 17 + 37*Name.GetHashCode();
            }
        }
    }
}
