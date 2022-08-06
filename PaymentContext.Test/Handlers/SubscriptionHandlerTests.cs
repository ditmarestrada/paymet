using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Test.Mocks;

namespace PaymentContext.Test.Handlers
{
         [TestClass]
    public class SubscriptionHandlerTests
    {
         [TestMethod]
        public void  ShouldReturnErrorWhenDocumentExists()        
        {
           var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
           var command = new CreateBoletoSubscriptionCommand();
        
           command.FirstName = "Bruce";
           command.LastName = "Wayne";
           command.Document = "12345678";
           command.Email = "ditmar1man@gmail.com";
           command.BarCode = "123456789";
           command.BoletoNumber = "12345654987";
           command.PaymentNumber = "123121";
           command.PaidDate = DateTime.Now;
           command.ExpireDate = DateTime.Now.AddMonths(1);
           command.Total = 60;
           command.TotalPaid = 60; 
           command.Payer = "WAYNE CORP";
           command.PayerDocument = "43721174";
           command.PayerDocumentType = EDocumentType.DNI;
           command.PayerEmail = "detrada@hotmail.com";
           command.Street = "calle Cirpriano Ruiz";
           command.Number = "asdd";
           command.Neighborhood = "asdasd";
           command.City = "Lima";
           command.State = "Lima";
           command.Country = "Perú";
           command.ZipCode = "15304";

           handler.Handle(command);
           Assert.AreEqual(false, handler.Valid);           

        }
    }
}