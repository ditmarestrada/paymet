using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Test
{
    [TestClass]
    public class StudentTests
    {

        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Subscription _subscription;

           
        public StudentTests(){
            _name = new Name("Bruce","Wayne");
            _document=new Document("43721174",EDocumentType.DNI);
            _email=new Email("ditmarestrada@gmail.com");
            _address=new Address("Calle Cirpriano Ruiz","109","","Los Olivos","Lima","Perú","10504");
            _student=new Student(_name,_document,_email);
            _subscription=new Subscription(null);
            
        }
        
        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription(){
        
            var payment=new PayPalPayment("123464564",DateTime.Now,DateTime.Now.AddDays(5),10,10,"Ditmar",_document,_address,_email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }
        [TestMethod]
         public void ShouldReturnErrorWhenSubscriptionHasNoPayment(){
        
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }
        [TestMethod]

        public void ShouldReturnSuccessWhenHadNoActiveSubscription(){
            
            var payment = new PayPalPayment("123464564",DateTime.Now,DateTime.Now.AddDays(5),10,10,"Ditmar",_document,_address,_email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Valid);
        }
    }
}
