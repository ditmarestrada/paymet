using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Test.ValueObjects
{

    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void  ShouldReturnErrorWhenDniIsInvalid()        
        {
            var doc=new Document("452",EDocumentType.DNI);
            Assert.IsTrue(doc.Invalid);
        }

         [TestMethod]
        public void  ShouldReturnSuccessWhenDniIsValid()        
        {
           var doc=new Document("43721174",EDocumentType.DNI);
            Assert.IsTrue(doc.Valid);
        }
        [TestMethod]
        public void  ShouldReturnErrorWhenCEIsInvalid()        
        {
          var doc=new Document("452",EDocumentType.CE);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("10437211720")]
        [DataRow("10437211701")]
        [DataRow("20454575544")]
        public void  ShouldReturnSuccessWhenCEIsValid(string cn)        
        {
           var doc=new Document(cn,EDocumentType.CE);
            Assert.IsTrue(doc.Valid);
        }
    }
}