using System.Linq;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.InlineDefinitions
{
    [TestClass]
    public class ArticleAn
    {
        private TokenContext _definitionContext;
        private DefineTokenizer _definitionFinder;
        private const string TestFile = @"..\..\Finder\Inline Definitions\Test Files\03.X_With_Article_An.docx";

        private const int NumberOfDefinitionsInFile = 33;

        [TestInitialize]
        public void TestInitialize()
        {
            _definitionContext = new TokenContext();
            _definitionFinder = new DefineTokenizer(_definitionContext);

            _definitionFinder.TokenizeFile(TestFile);
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Article_An_In_Brackets() // (an X)
        {
            Assert.IsNotNull(_definitionContext.Get("Avocado"));
            Assert.IsNotNull(_definitionContext.Get("Avocado2"));
            Assert.IsNotNull(_definitionContext.Get("Avocado3"));
            Assert.IsNotNull(_definitionContext.Get("Avocado4"));
            Assert.IsNotNull(_definitionContext.Get("Avocado5"));
            Assert.IsNotNull(_definitionContext.Get("Avocado6"));
            Assert.IsNotNull(_definitionContext.Get("Avocado7"));
            Assert.IsNotNull(_definitionContext.Get("Avocado8"));
            Assert.IsNotNull(_definitionContext.Get("Avocado9"));
            Assert.IsNotNull(_definitionContext.Get("Avocado10"));
            Assert.IsNotNull(_definitionContext.Get("Avocado11"));
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Article_An() // an X
        {
            Assert.IsNotNull(_definitionContext.Get("Almond"));
            Assert.IsNotNull(_definitionContext.Get("Almond2"));
            Assert.IsNotNull(_definitionContext.Get("Almond3"));
            Assert.IsNotNull(_definitionContext.Get("Almond4"));
            Assert.IsNotNull(_definitionContext.Get("Almond5"));
            Assert.IsNotNull(_definitionContext.Get("Almond6"));
            Assert.IsNotNull(_definitionContext.Get("Almond7"));
            Assert.IsNotNull(_definitionContext.Get("Almond8"));
            Assert.IsNotNull(_definitionContext.Get("Almond9"));
            Assert.IsNotNull(_definitionContext.Get("Almond10"));
            Assert.IsNotNull(_definitionContext.Get("Almond11"));
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Some_Text_And_Article_An_In_Brackets() // (some text an X)
        {
            Assert.IsNotNull(_definitionContext.Get("Eggplant"));
            Assert.IsNotNull(_definitionContext.Get("Eggplant2"));
            Assert.IsNotNull(_definitionContext.Get("Eggplant3"));
            Assert.IsNotNull(_definitionContext.Get("Eggplant4"));
            Assert.IsNotNull(_definitionContext.Get("Eggplant5"));
            Assert.IsNotNull(_definitionContext.Get("Eggplant6"));
            Assert.IsNotNull(_definitionContext.Get("Eggplant7"));
            Assert.IsNotNull(_definitionContext.Get("Eggplant8"));
            Assert.IsNotNull(_definitionContext.Get("Eggplant9"));
            Assert.IsNotNull(_definitionContext.Get("Eggplant10"));
            Assert.IsNotNull(_definitionContext.Get("Eggplant11"));
        }

        [TestMethod]
        public void Matches_Expected_Number_Of_Definitions()
        {
            Assert.AreEqual(NumberOfDefinitionsInFile, _definitionContext.GetAll().Count());
        }
    }
}
