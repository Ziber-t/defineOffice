using System;
using System.Linq;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.ListedDefinitions
{
    [TestClass]
    public class Ranges
    {
        private TokenContext _definitionContext;
        private DefineTokenizer _definitionFinder;
        private const string TestFile = @"..\..\Finder\Listed Definitions\Test Files\06.Ranges.docx";
        private const int NumberOfDefinitionsInFile = 12;

        [TestInitialize]
        public void TestInitialize()
        {
            _definitionContext = new TokenContext();
            _definitionFinder = new DefineTokenizer(_definitionContext);

            _definitionFinder.TokenizeFile(TestFile);
        }

        [TestMethod]
        public void Get_All_Definitions_From_File()
        {
            var count = _definitionContext.GetAll().Count();

            Assert.AreEqual(NumberOfDefinitionsInFile, count);
        }

        [TestMethod]
        public void Get_Correct_Range_For_Paragraph_Which_Ends_With_Period()
        {
            var definition = _definitionContext.Get("Accounting Principles");

            Assert.IsNotNull(definition);
            Assert.AreEqual(1, definition.TokenInfos.Count());
            Assert.AreEqual(0, definition.TokenInfos.FirstOrDefault().StartParagraphIndex);
            Assert.AreEqual(0, definition.TokenInfos.FirstOrDefault().EndParagraphIndex);
        }

        [TestMethod]
        public void Get_Correct_Range_For_Paragraph_Which_Ends_With_Semicolon()
        {
            var definition = _definitionContext.Get("Ancillary Commitment");

            Assert.IsNotNull(definition);
            Assert.AreEqual(1, definition.TokenInfos.Count());
            Assert.AreEqual(1, definition.TokenInfos.FirstOrDefault().StartParagraphIndex);
            Assert.AreEqual(1, definition.TokenInfos.FirstOrDefault().EndParagraphIndex);
        }

        [TestMethod]
        public void Get_Correct_Range_For_List()
        {
            var definition = _definitionContext.Get("Ancillary Outstandings");

            Assert.IsNotNull(definition);
            Assert.AreEqual(1, definition.TokenInfos.Count());
            Assert.AreEqual(3, definition.TokenInfos.FirstOrDefault().StartParagraphIndex);
            Assert.AreEqual(7, definition.TokenInfos.FirstOrDefault().EndParagraphIndex);
        }

        [TestMethod]
        public void Get_Correct_Range_For_Multi_Level_List()
        {
            var definition = _definitionContext.Get("Cash Equivalent Investments");

            Assert.IsNotNull(definition);
            Assert.AreEqual(1, definition.TokenInfos.Count());
            Assert.AreEqual(8, definition.TokenInfos.FirstOrDefault().StartParagraphIndex);
            Assert.AreEqual(19, definition.TokenInfos.FirstOrDefault().EndParagraphIndex);
        }

        [TestMethod]
        public void Get_Correct_Range_For_Multi_Level_List_With_Table_And_Ongoing_Paragraph()
        {
            var definition = _definitionContext.Get("Margin");

            Assert.IsNotNull(definition);
            Assert.AreEqual(1, definition.TokenInfos.Count());
            Assert.AreEqual(20, definition.TokenInfos.FirstOrDefault().StartParagraphIndex);
            Assert.AreEqual(42, definition.TokenInfos.FirstOrDefault().EndParagraphIndex);
        }

        [TestMethod]
        public void Get_Correct_Range_For_Definitions_With_Square_Brackets_In_The_End()
        {
            var definition = _definitionContext.Get("Permitted Guarantee");

            Assert.IsNotNull(definition);
            Assert.AreEqual(1, definition.TokenInfos.Count());
            Assert.AreEqual(44, definition.TokenInfos.FirstOrDefault().StartParagraphIndex);
            Assert.AreEqual(50, definition.TokenInfos.FirstOrDefault().EndParagraphIndex);
        }

        [TestMethod]
        public void Get_Correct_Range_For_Definition_Followed_By_Heading_Paragraph()
        {
            var definition = _definitionContext.Get("Permitted Joint Venture");

            Assert.IsNotNull(definition);
            Assert.AreEqual(1, definition.TokenInfos.Count());
            Assert.AreEqual(51, definition.TokenInfos.FirstOrDefault().StartParagraphIndex);
            Assert.AreEqual(59, definition.TokenInfos.FirstOrDefault().EndParagraphIndex);
        }

        [TestMethod]
        public void Get_Correct_Range_For_Definition_With_Table_And_Heading_Style_In_Table()
        {
            var definition = _definitionContext.Get("De-Leveraging Target");

            Assert.IsNotNull(definition);
            Assert.AreEqual(61, definition.TokenInfos.FirstOrDefault().StartParagraphIndex);
            Assert.AreEqual(83, definition.TokenInfos.FirstOrDefault().EndParagraphIndex);
        }
    }
}
