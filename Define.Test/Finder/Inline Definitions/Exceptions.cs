using System;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.InlineDefinitions
{
    [TestClass]
    public class Exceptions
    {
        private TokenContext _definitionContext;
        private DefineTokenizer _definitionFinder;
        private const string TestFile = @"..\..\Finder\Inline Definitions\Test Files\06.Exceptions.docx";

        [TestInitialize]
        public void TestInitialize()
        {
            _definitionContext = new TokenContext();
            _definitionFinder = new DefineTokenizer(_definitionContext);

            _definitionFinder.TokenizeFile(TestFile);
        }

        [TestMethod]
        public void Doesnt_Pickup_Exceptions()
        {
            Assert.IsNull(_definitionContext.Get("Borrower"));
            Assert.IsNull(_definitionContext.Get("Facility Agent"));
            Assert.IsNull(_definitionContext.Get("Security Agent"));
            Assert.IsNull(_definitionContext.Get("Conditions Precedent"));
        }
    }
}
