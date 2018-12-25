using System.Linq;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.ListedDefinitions
{
    [TestClass]
    public class ShallBearTheTheMeaning
    {
        private TokenContext _definitionContext;
        private DefineTokenizer _definitionFinder;
        private const string TestFile = @"..\..\Finder\Listed Definitions\Test Files\07.X_Shall_Bear_The_Meaning.docx";
        private const int NumberOfDefinitionsInFile = 3;

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
            Assert.IsNotNull(_definitionContext.Get("Elevation"));
            Assert.IsNotNull(_definitionContext.Get("Transfer"));
            Assert.IsNotNull(_definitionContext.Get("Transferee"));

            Assert.AreEqual(NumberOfDefinitionsInFile, _definitionContext.GetAll().Count());
        }
    }
}
