using Define.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test
{
    [TestClass]
    public class StringExtensions
    {
        [TestMethod]
        public void Singularizes_last_word_in_phrase()
        {
            var s = "Finance Documents";

            Assert.AreEqual("Finance Document", s.ToSingularForm());
        }

        [TestMethod]
        public void Singularizes_simple_word()
        {
            var s = "Documents";

            Assert.AreEqual("Document", s.ToSingularForm());
        }

        [TestMethod]
        public void Pluralizes_last_word_in_phrase()
        {
            var s = "Finance Document";

            Assert.AreEqual("Finance Documents", s.ToPluralForm());
        }

        [TestMethod]
        public void Pluralizes_simple_word()
        {
            var s = "Document";

            Assert.AreEqual("Documents", s.ToPluralForm());
        }
    }
}
