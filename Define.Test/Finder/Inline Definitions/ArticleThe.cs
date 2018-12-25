using System.Linq;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.InlineDefinitions
{
    [TestClass]
    public class ArticleThe
    {
        private TokenContext _definitionContext;
        private DefineTokenizer _definitionFinder;
        private const string TestFile = @"..\..\Finder\Inline Definitions\Test Files\01.X_With_Article_The.docx";

        private const int NumberOfDefinitionsInFile = 44;

        [TestInitialize]
        public void TestInitialize()
        {
            _definitionContext = new TokenContext();
            _definitionFinder = new DefineTokenizer(_definitionContext);

            _definitionFinder.TokenizeFile(TestFile);
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Article_The_In_Brackets() // (the X)
        {
            Assert.IsNotNull(_definitionContext.Get("Banana"));
            Assert.IsNotNull(_definitionContext.Get("Banana2"));
            Assert.IsNotNull(_definitionContext.Get("Banana3"));
            Assert.IsNotNull(_definitionContext.Get("Banana4"));
            Assert.IsNotNull(_definitionContext.Get("Banana5"));
            Assert.IsNotNull(_definitionContext.Get("Banana6"));
            Assert.IsNotNull(_definitionContext.Get("Banana7"));
            Assert.IsNotNull(_definitionContext.Get("Banana8"));
            Assert.IsNotNull(_definitionContext.Get("Banana9"));
            Assert.IsNotNull(_definitionContext.Get("Banana10"));
            Assert.IsNotNull(_definitionContext.Get("Banana11"));
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Article_The() // the X
        {
            Assert.IsNotNull(_definitionContext.Get("Apricot"));
            Assert.IsNotNull(_definitionContext.Get("Apricot2"));
            Assert.IsNotNull(_definitionContext.Get("Apricot3"));
            Assert.IsNotNull(_definitionContext.Get("Apricot4"));
            Assert.IsNotNull(_definitionContext.Get("Apricot5"));
            Assert.IsNotNull(_definitionContext.Get("Apricot6"));
            Assert.IsNotNull(_definitionContext.Get("Apricot7"));
            Assert.IsNotNull(_definitionContext.Get("Apricot8"));
            Assert.IsNotNull(_definitionContext.Get("Apricot9"));
            Assert.IsNotNull(_definitionContext.Get("Apricot10"));
            Assert.IsNotNull(_definitionContext.Get("Apricot11"));
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Some_Text_And_Article_The_In_Brackets() // (some text the X)
        {
            Assert.IsNotNull(_definitionContext.Get("Kiwi"));
            Assert.IsNotNull(_definitionContext.Get("Kiwi2"));
            Assert.IsNotNull(_definitionContext.Get("Kiwi3"));
            Assert.IsNotNull(_definitionContext.Get("Kiwi4"));
            Assert.IsNotNull(_definitionContext.Get("Kiwi5"));
            Assert.IsNotNull(_definitionContext.Get("Kiwi6"));
            Assert.IsNotNull(_definitionContext.Get("Kiwi7"));
            Assert.IsNotNull(_definitionContext.Get("Kiwi8"));
            Assert.IsNotNull(_definitionContext.Get("Kiwi9"));
            Assert.IsNotNull(_definitionContext.Get("Kiwi10"));
            Assert.IsNotNull(_definitionContext.Get("Kiwi11"));
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Some_Text_And_Comma_And_Article_The_In_Brackets() // (some text, the X)
        {
            Assert.IsNotNull(_definitionContext.Get("Carrot"));
            Assert.IsNotNull(_definitionContext.Get("Carrot2"));
            Assert.IsNotNull(_definitionContext.Get("Carrot3"));
            Assert.IsNotNull(_definitionContext.Get("Carrot4"));
            Assert.IsNotNull(_definitionContext.Get("Carrot5"));
            Assert.IsNotNull(_definitionContext.Get("Carrot6"));
            Assert.IsNotNull(_definitionContext.Get("Carrot7"));
            Assert.IsNotNull(_definitionContext.Get("Carrot8"));
            Assert.IsNotNull(_definitionContext.Get("Carrot9"));
            Assert.IsNotNull(_definitionContext.Get("Carrot10"));
            Assert.IsNotNull(_definitionContext.Get("Carrot11"));
        }


        [TestMethod]
        public void Matches_Expected_Number_Of_Definitions()
        {
            Assert.AreEqual(NumberOfDefinitionsInFile, _definitionContext.GetAll().Count());
        }
    }
}
