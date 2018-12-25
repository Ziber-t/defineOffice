using System.Linq;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.ListedDefinitions
{
    [TestClass]
    public class ShallHaveTheMeaning
    {
        private TokenContext _definitionContext;
        private DefineTokenizer _definitionFinder;
        private const string TestFile = @"..\..\Finder\Listed Definitions\Test Files\04.X_Shall_Have_The_Meaning.docx";
        private const int NumberOfDefinitionsInFile = 11;

        [TestInitialize]
        public void TestInitialize()
        {
            _definitionContext = new TokenContext();
            _definitionFinder = new DefineTokenizer(_definitionContext);
            _definitionFinder.TokenizeFile(TestFile);
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Shall_Have_The_Meaning_Keyword()
        {
            Assert.IsNotNull(_definitionContext.Get("Mango"));
            Assert.IsNotNull(_definitionContext.Get("Mango2"));
            Assert.IsNotNull(_definitionContext.Get("Mango3"));
            Assert.IsNotNull(_definitionContext.Get("Mango4"));
            Assert.IsNotNull(_definitionContext.Get("Mango5"));
            Assert.IsNotNull(_definitionContext.Get("Mango6"));
            Assert.IsNotNull(_definitionContext.Get("Mango7"));
            Assert.IsNotNull(_definitionContext.Get("Mango8"));
            Assert.IsNotNull(_definitionContext.Get("Mango9"));
            Assert.IsNotNull(_definitionContext.Get("Mango10"));
            Assert.IsNotNull(_definitionContext.Get("Mango11"));

            // simple text in quotes without formatting should not be picked up
            Assert.IsNull(_definitionContext.Get("Indian Mango"));
            
            // simple text without formatting or quotes followed by "has the meaning" should not be picked up
            Assert.IsNull(_definitionContext.Get("Mango12"));

            Assert.AreEqual(NumberOfDefinitionsInFile, _definitionContext.GetAll().Count());
        }
    }
}
