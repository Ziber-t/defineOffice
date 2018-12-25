using System.Linq;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.ListedDefinitions
{
    [TestClass]
    public class HasTheMeaning
    {
        private TokenContext _definitionContext;
        private DefineTokenizer _definitionFinder;
        private const string TestFile = @"..\..\Finder\Listed Definitions\Test Files\03.X_Has_The_Meaning.docx";
        private const int NumberOfDefinitionsInFile = 11;

        [TestInitialize]
        public void TestInitialize()
        {
            _definitionContext = new TokenContext();
            _definitionFinder = new DefineTokenizer(_definitionContext);
            _definitionFinder.TokenizeFile(TestFile);
        }

        [TestMethod]
        public void Finds_All_Listed_Definitions_With_Has_The_Meaning_Keyword()
        {
            Assert.IsNotNull(_definitionContext.Get("Orange"));
            Assert.IsNotNull(_definitionContext.Get("Orange2"));
            Assert.IsNotNull(_definitionContext.Get("Orange3"));
            Assert.IsNotNull(_definitionContext.Get("Orange4"));
            Assert.IsNotNull(_definitionContext.Get("Orange5"));
            Assert.IsNotNull(_definitionContext.Get("Orange6"));
            Assert.IsNotNull(_definitionContext.Get("Orange7"));
            Assert.IsNotNull(_definitionContext.Get("Orange8"));
            Assert.IsNotNull(_definitionContext.Get("Orange9"));
            Assert.IsNotNull(_definitionContext.Get("Orange10"));
            Assert.IsNotNull(_definitionContext.Get("Orange12"));

            // simple text without formatting or quotes followed by "has the meaning" should not be picked up
            Assert.IsNull(_definitionContext.Get("Orange11"));

            Assert.AreEqual(NumberOfDefinitionsInFile, _definitionContext.GetAll().Count());
        }
    }
}
