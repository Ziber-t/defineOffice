using System.Linq;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.ListedDefinitions
{
    [TestClass]
    public class Tables
    {
        private TokenContext _definitionContext;
        private DefineTokenizer _definitionFinder;
        private const string TestFile = @"..\..\Finder\Listed Definitions\Test Files\05.Tables.docx";
        private const int NumberOfDefinitionsInFile = 5;

        [TestInitialize]
        public void TestInitialize()
        {
            _definitionContext = new TokenContext();
            _definitionFinder = new DefineTokenizer(_definitionContext);

            _definitionFinder.TokenizeFile(TestFile);
        }

        [TestMethod]
        public void Finds_All_Definitions_In_Tables()
        {
            Assert.IsNotNull(_definitionContext.Get("Excluded Territories"));
            Assert.IsNotNull(_definitionContext.Get("PM"));
            Assert.IsNotNull(_definitionContext.Get("PRA"));
            Assert.IsNotNull(_definitionContext.Get("Prospectus"));
            Assert.IsNotNull(_definitionContext.Get("PD Regulation"));

            Assert.IsNull(_definitionContext.Get("this document")); // get only capitalized words
            Assert.IsNull(_definitionContext.Get("Column 1")); // both columns in bold
            Assert.IsNull(_definitionContext.Get("Parent1")); // both column are regular font
            Assert.IsNull(_definitionContext.Get("Name of Original Borrower")); // both columns are regular font
            Assert.IsNull(_definitionContext.Get("Borrower")); // both columns are italic
            Assert.IsNull(_definitionContext.Get("Parent2")); // 2nd column is italic
            Assert.IsNull(_definitionContext.Get("TPL")); // 2nd column doesn't exist
        }
    }
}
