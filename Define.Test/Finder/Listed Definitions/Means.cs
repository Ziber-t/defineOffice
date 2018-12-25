using System.Linq;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.ListedDefinitions
{
    [TestClass]
    public class Means
    {
        private TokenContext _definitionContext;
        private DefineTokenizer _definitionFinder;
        private const string TestFile = @"..\..\Finder\Listed Definitions\Test Files\01.X_Means.docx";
        private const int NumberOfDefinitionsInFile = 17;

        [TestInitialize]
        public void TestInitialize()
        {
            _definitionContext = new TokenContext();
            _definitionFinder = new DefineTokenizer(_definitionContext);

            _definitionFinder.TokenizeFile(TestFile);
        }

        [TestMethod]
        public void Finds_All_Definitions_With_Means_Keyword()
        {
            // the following definitions shall be picked up
            Assert.IsNotNull(_definitionContext.Get("Apple"));
            Assert.IsNotNull(_definitionContext.Get("Apple2"));
            Assert.IsNotNull(_definitionContext.Get("Apple3"));
            Assert.IsNotNull(_definitionContext.Get("Apple4"));
            Assert.IsNotNull(_definitionContext.Get("Apple5"));
            Assert.IsNotNull(_definitionContext.Get("Apple6"));
            Assert.IsNotNull(_definitionContext.Get("Apple7"));
            Assert.IsNotNull(_definitionContext.Get("Apple8"));
            Assert.IsNotNull(_definitionContext.Get("Apple9"));
            Assert.IsNotNull(_definitionContext.Get("Apple10"));
            Assert.IsNotNull(_definitionContext.Get("Apple12"));
            Assert.IsNotNull(_definitionContext.Get("Act"));
            Assert.IsNotNull(_definitionContext.Get("Associated Person"));
            Assert.IsNotNull(_definitionContext.Get("Treaty State"));
            Assert.IsNotNull(_definitionContext.Get("Cash Equivalent Investments"));
            Assert.IsNotNull(_definitionContext.Get("Debt Service Reserve Account"));
            Assert.IsNotNull(_definitionContext.Get("Treaty"));
            //Assert.IsNotNull(_definitionContext.Get("DSRA"));

            // the following definitions shall not be picked up
            Assert.IsNull(_definitionContext.Get("Apple11")); // simple text without formatting or quotes followed by "means" should not be found
            Assert.IsNull(_definitionContext.Get("determines"));
            Assert.IsNull(_definitionContext.Get("determine"));
            Assert.IsNull(_definitionContext.Get("Tax gross-up and indemnities"));
            

            Assert.AreEqual(NumberOfDefinitionsInFile, _definitionContext.GetAll().Count());
        }
    }
}
