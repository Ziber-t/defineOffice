using Define.Common.Diagnostic;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.Paragraph_Matcher
{
    [TestClass]
    public class ParagraphCount
    {
        [TestMethod]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\Greek_Airports.docx", 7590)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\LMA.docx", 3479)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\RSK.docx", 3776)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\Global_Universe.docx", 4226)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\Salalah.docx", 1927)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\ArEva.docx", 840)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\Bolt.docx", 1384)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\Coriant.docx", 117)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\DD.docx", 2857)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\DRAFT.docx", 576)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\GS_GTLK.docx", 1955)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\Izmir.docx", 5888)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\Nevis.docx", 4008)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\Offering_Circular.docx", 1351)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\Tenancy.docx", 222)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\Tesco.docx", 1601)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\Adelaide.docx", 4524)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\BK.docx", 1838)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\15_Sub_Part_Agreement.docx", 402)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\Chassis.docx", 475)]
        [DataRow(@"..\..\Finder\Paragraph Matcher\Test Files\Univeg.docx", 3825)]
        public void Paragraph_Amount_Is_Correct(string file, int expectedParagraphCount)
        {
            var definitionContext = new TokenContext();
            var definitionFinder = new DefineTokenizer(definitionContext);
            definitionFinder.TokenizeFile(file);

            Assert.AreEqual(expectedParagraphCount, DiagnosticInformation.OpenXmlParagraphCount);
        }
    }
}
