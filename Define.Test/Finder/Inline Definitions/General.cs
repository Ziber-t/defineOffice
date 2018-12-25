using System;
using Define.Tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Define.Test.Finder.InlineDefinitions
{
    [TestClass]
    public class General
    {
        private TokenContext _definitionContext;
        private DefineTokenizer _definitionFinder;
        private const string TestFile = @"..\..\Finder\Inline Definitions\Test Files\05.General.docx";

        [TestInitialize]
        public void TestInitialize()
        {
            _definitionContext = new TokenContext();
            _definitionFinder = new DefineTokenizer(_definitionContext);

            _definitionFinder.TokenizeFile(TestFile);
        }

        [TestMethod]
        public void TestAllInlineCases()
        {
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs1"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs2"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs3"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs4"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs5"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs6"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs7"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs8"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs9"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs10"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs11"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition1"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition2"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition3"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition4"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition5"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition6"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition7"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition8"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition9"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition10"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition11"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs12"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs13"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs14"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs15"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs16"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs17"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs18"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs19"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs20"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs21"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs22"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition12"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition13"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition14"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition15"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition16"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition17"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition18"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition19"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition20"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition21"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition22"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs23"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs24"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs25"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs26"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs27"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs28"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs29"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs30"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs31"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs32"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs33"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition23"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition24"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition25"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition26"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition27"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition28"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition29"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition30"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition31"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition32"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition33"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs34"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs35"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs36"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs37"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs38"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs39"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs40"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs41"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs42"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs43"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs44"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition35"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition36"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition37"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition38"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition39"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition40"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition41"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition42"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition43"));
            Assert.IsNotNull(_definitionContext.Get("Permitted Acquisition44"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs45"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs46"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs47"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs48"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs49"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs50"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs51"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs52"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs53"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs54"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs55"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs56"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs57"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs58"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs59"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs60"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs61"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs62"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs63"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs64"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs65"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs66"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs67"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs68"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs69"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs70"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs71"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs72"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs73"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs74"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs75"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs76"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs77"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs78"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs79"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs80"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs81"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs82"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs83"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs84"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs85"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs86"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs87"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs88"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs89"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs90"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs91"));
            Assert.IsNotNull(_definitionContext.Get("Unicredit1"));
            Assert.IsNotNull(_definitionContext.Get("Unicredit2"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs92"));
            Assert.IsNotNull(_definitionContext.Get("Unicredit3"));
            Assert.IsNotNull(_definitionContext.Get("Unicredit4"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs93"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs94"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs95"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs96"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs97"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs98"));
            Assert.IsNotNull(_definitionContext.Get("Unicredit7"));
            Assert.IsNotNull(_definitionContext.Get("Unicredit8"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs99"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs100"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs101"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs102"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs103"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs104"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs105"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs106"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs107"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Costs108"));
            Assert.IsNotNull(_definitionContext.Get("Acquisition Cost113"));

            Assert.IsNull(_definitionContext.Get("Unicredit5"));
            Assert.IsNull(_definitionContext.Get("Unicredit6"));
            Assert.IsNull(_definitionContext.Get("Acquisition Cost109"));
            Assert.IsNull(_definitionContext.Get("Acquisition Cost110"));
            Assert.IsNull(_definitionContext.Get("Acquisition Cost111"));
            Assert.IsNull(_definitionContext.Get("Acquisition Cost112"));
            Assert.IsNull(_definitionContext.Get("Permitted Acquisition34"));
        }
    }
}
