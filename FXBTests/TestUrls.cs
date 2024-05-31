using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rappen.XTB.FetchXmlBuilder.AppCode;

namespace FXBTests
{
    [TestClass]
    public class TestUrls
    {
        private const string docsUrl = "https://docs.microsoft.com/en-us/powerapps/developer/model-driven-apps/developer-tools";
        private const string docsUrlResult = "https://docs.microsoft.com/en-us/powerapps/developer/model-driven-apps/developer-tools?WT.mc_id=DX-MVP-5002475&utm_source=FetchXMLBuilder&utm_medium=XrmToolBox";
        private const string docsUrlWithAnchor = "https://docs.microsoft.com/en-us/powerapps/developer/model-driven-apps/clientapi/client-scripting-best-practices#write-your-code-for-multiple-browsers";
        private const string docsUrlWithAnchorResult = "https://docs.microsoft.com/en-us/powerapps/developer/model-driven-apps/clientapi/client-scripting-best-practices?WT.mc_id=DX-MVP-5002475&utm_source=FetchXMLBuilder&utm_medium=XrmToolBox#write-your-code-for-multiple-browsers";
        private const string docsUrlWithQueryAndAnchor = "https://docs.microsoft.com/en-us/powerapps/developer/model-driven-apps/clientapi/client-scripting-best-practices?hey=ho#write-your-code-for-multiple-browsers";
        private const string docsUrlWithQueryAndAnchorResult = "https://docs.microsoft.com/en-us/powerapps/developer/model-driven-apps/clientapi/client-scripting-best-practices?hey=ho&WT.mc_id=DX-MVP-5002475&utm_source=FetchXMLBuilder&utm_medium=XrmToolBox#write-your-code-for-multiple-browsers";
        private const string randomUrl = "https://fetchxmlbuilder.com";
        private const string randomUrlResult = "https://fetchxmlbuilder.com/?utm_source=FetchXMLBuilder&utm_medium=XrmToolBox";

        [TestMethod]
        public void TestDocsUrl()
        {
            string result = Utils.ProcessURL(docsUrl);
            Assert.AreEqual(docsUrlResult, result);
        }

        [TestMethod]
        public void TestDocsUrlWithAnchor()
        {
            string result = Utils.ProcessURL(docsUrlWithAnchor);
            Assert.AreEqual(docsUrlWithAnchorResult, result);
        }

        [TestMethod]
        public void TestDocsUrlWithQueryAndAnchor()
        {
            string result = Utils.ProcessURL(docsUrlWithQueryAndAnchor);
            Assert.AreEqual(docsUrlWithQueryAndAnchorResult, result);
        }

        [TestMethod]
        public void TestRandomUrl()
        {
            string result = Utils.ProcessURL(randomUrl);
            Assert.AreEqual(randomUrlResult, result);
        }
    }
}
