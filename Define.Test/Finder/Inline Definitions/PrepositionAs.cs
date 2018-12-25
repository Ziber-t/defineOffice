using System.Linq;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.InlineDefinitions
{
    [TestClass]
    public class PrepositionAs
    {
        private TokenContext _definitionContext;
        private DefineTokenizer _definitionFinder;
        private const string TestFile = @"..\..\Finder\Inline Definitions\Test Files\04.X_With_Preposition_As.docx";

        private const int NumberOfDefinitionsInFile = 32;

        [TestInitialize]
        public void TestInitialize()
        {
            _definitionContext = new TokenContext();
            _definitionFinder = new DefineTokenizer(_definitionContext);

            _definitionFinder.TokenizeFile(TestFile);
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Preposition_As_In_Brackets() // (as X)
        {
            Assert.IsNotNull(_definitionContext.Get("Grapefruit"));
            Assert.IsNotNull(_definitionContext.Get("Grapefruit2"));
            Assert.IsNotNull(_definitionContext.Get("Grapefruit3"));
            Assert.IsNotNull(_definitionContext.Get("Grapefruit4"));
            Assert.IsNotNull(_definitionContext.Get("Grapefruit5"));
            Assert.IsNotNull(_definitionContext.Get("Grapefruit6"));
            Assert.IsNotNull(_definitionContext.Get("Grapefruit7"));
            Assert.IsNotNull(_definitionContext.Get("Grapefruit8"));
            Assert.IsNotNull(_definitionContext.Get("Grapefruit9"));
            Assert.IsNotNull(_definitionContext.Get("Grapefruit10"));
            Assert.IsNotNull(_definitionContext.Get("Grapefruit11"));

        }

        [TestMethod]
        public void Finds_All_Definitions_With_Preposition_As() // as X
        {
            Assert.IsNotNull(_definitionContext.Get("Grapes"));
            Assert.IsNotNull(_definitionContext.Get("Grapes2"));
            Assert.IsNotNull(_definitionContext.Get("Grapes3"));
            Assert.IsNotNull(_definitionContext.Get("Grapes4"));
            Assert.IsNotNull(_definitionContext.Get("Grapes5"));
            Assert.IsNotNull(_definitionContext.Get("Grapes6"));
            Assert.IsNotNull(_definitionContext.Get("Grapes7"));
            Assert.IsNotNull(_definitionContext.Get("Grapes8"));
            Assert.IsNotNull(_definitionContext.Get("Grapes9"));
            Assert.IsNotNull(_definitionContext.Get("Grapes11"));

            Assert.IsNull(_definitionContext.Get("Grapes10"));
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Some_Text_And_Preposition_As_In_Brackets() // (some text as X)
        {
            Assert.IsNotNull(_definitionContext.Get("Raspberry"));
            Assert.IsNotNull(_definitionContext.Get("Raspberry2"));
            Assert.IsNotNull(_definitionContext.Get("Raspberry3"));
            Assert.IsNotNull(_definitionContext.Get("Raspberry4"));
            Assert.IsNotNull(_definitionContext.Get("Raspberry5"));
            Assert.IsNotNull(_definitionContext.Get("Raspberry6"));
            Assert.IsNotNull(_definitionContext.Get("Raspberry7"));
            Assert.IsNotNull(_definitionContext.Get("Raspberry8"));
            Assert.IsNotNull(_definitionContext.Get("Raspberry9"));
            Assert.IsNotNull(_definitionContext.Get("Raspberry10"));
            Assert.IsNotNull(_definitionContext.Get("Raspberry11"));
        }

        [TestMethod]
        public void Matches_Expected_Number_Of_Definitions()
        {
            Assert.AreEqual(NumberOfDefinitionsInFile, _definitionContext.GetAll().Count());
        }
    }
}
