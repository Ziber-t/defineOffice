using System.Linq;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.InlineDefinitions
{
    [TestClass]
    public class ArticleA
    {
        private TokenContext _definitionContext;
        private DefineTokenizer _definitionFinder;
        private const string TestFile = @"..\..\Finder\Inline Definitions\Test Files\02.X_With_Articles_A.docx";

        private const int NumberOfDefinitionsInFile = 33;

        [TestInitialize]
        public void TestInitialize()
        {
            _definitionContext = new TokenContext();
            _definitionFinder = new DefineTokenizer(_definitionContext);

            _definitionFinder.TokenizeFile(TestFile);
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Article_A_In_Brackets() // (a X)
        {
            Assert.IsNotNull(_definitionContext.Get("Melon"));
            Assert.IsNotNull(_definitionContext.Get("Melon2"));
            Assert.IsNotNull(_definitionContext.Get("Melon3"));
            Assert.IsNotNull(_definitionContext.Get("Melon4"));
            Assert.IsNotNull(_definitionContext.Get("Melon5"));
            Assert.IsNotNull(_definitionContext.Get("Melon6"));
            Assert.IsNotNull(_definitionContext.Get("Melon7"));
            Assert.IsNotNull(_definitionContext.Get("Melon8"));
            Assert.IsNotNull(_definitionContext.Get("Melon9"));
            Assert.IsNotNull(_definitionContext.Get("Melon10"));
            Assert.IsNotNull(_definitionContext.Get("Melon11"));
        }


        [TestMethod]
        public void Finds_All_Definitions_With_Article_A() // a X
        {
            Assert.IsNotNull(_definitionContext.Get("Watermelon"));
            Assert.IsNotNull(_definitionContext.Get("Watermelon2"));
            Assert.IsNotNull(_definitionContext.Get("Watermelon3"));
            Assert.IsNotNull(_definitionContext.Get("Watermelon4"));
            Assert.IsNotNull(_definitionContext.Get("Watermelon5"));
            Assert.IsNotNull(_definitionContext.Get("Watermelon6"));
            Assert.IsNotNull(_definitionContext.Get("Watermelon7"));
            Assert.IsNotNull(_definitionContext.Get("Watermelon8"));
            Assert.IsNotNull(_definitionContext.Get("Watermelon9"));
            Assert.IsNotNull(_definitionContext.Get("Watermelon10"));
            Assert.IsNotNull(_definitionContext.Get("Watermelon11"));
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Some_Text_And_Article_A_In_Brackets() // (some text a X)
        {
            Assert.IsNotNull(_definitionContext.Get("Strawberry"));
            Assert.IsNotNull(_definitionContext.Get("Strawberry2"));
            Assert.IsNotNull(_definitionContext.Get("Strawberry3"));
            Assert.IsNotNull(_definitionContext.Get("Strawberry4"));
            Assert.IsNotNull(_definitionContext.Get("Strawberry5"));
            Assert.IsNotNull(_definitionContext.Get("Strawberry6"));
            Assert.IsNotNull(_definitionContext.Get("Strawberry7"));
            Assert.IsNotNull(_definitionContext.Get("Strawberry8"));
            Assert.IsNotNull(_definitionContext.Get("Strawberry9"));
            Assert.IsNotNull(_definitionContext.Get("Strawberry10"));
            Assert.IsNotNull(_definitionContext.Get("Strawberry11"));
        }

        [TestMethod]
        public void Matches_Expected_Number_Of_Definitions()
        {
            Assert.AreEqual(NumberOfDefinitionsInFile, _definitionContext.GetAll().Count());
        }
    }
}
