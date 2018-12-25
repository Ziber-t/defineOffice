using System.Linq;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.ListedDefinitions
{
    [TestClass]
    public class ShallMean
    {
        private TokenContext _definitionContext;
        private DefineTokenizer _definitionFinder;
        private const string TestFile = @"..\..\Finder\Listed Definitions\Test Files\02.X_Shall_Mean.docx";
        private const int NumberOfDefinitionsInFile = 11;

        [TestInitialize]
        public void TestInitialize()
        {
            _definitionContext = new TokenContext();
            _definitionFinder = new DefineTokenizer(_definitionContext);

            _definitionFinder.TokenizeFile(TestFile);
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Shall_Mean_Keyword()
        {
            Assert.IsNotNull(_definitionContext.Get("Pineapple"));
            Assert.IsNotNull(_definitionContext.Get("Pineapple2"));
            Assert.IsNotNull(_definitionContext.Get("Pineapple3"));
            Assert.IsNotNull(_definitionContext.Get("Pineapple4"));
            Assert.IsNotNull(_definitionContext.Get("Pineapple5"));
            Assert.IsNotNull(_definitionContext.Get("Pineapple6"));
            Assert.IsNotNull(_definitionContext.Get("Pineapple7"));
            Assert.IsNotNull(_definitionContext.Get("Pineapple8"));
            Assert.IsNotNull(_definitionContext.Get("Pineapple9"));
            Assert.IsNotNull(_definitionContext.Get("Pineapple10"));
            Assert.IsNotNull(_definitionContext.Get("Pineapple12"));

            Assert.IsNull(_definitionContext.Get("Pineapple11")); // simple text without formatting or quotes followed by "shall mean" should not be picked up

            Assert.AreEqual(NumberOfDefinitionsInFile, _definitionContext.GetAll().Count());
        }
    }
}
