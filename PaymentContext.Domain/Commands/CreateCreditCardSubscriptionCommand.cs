using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreateCreditCardSubscriptionCommand:Notifiable,ICommand
    {
      public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string CardHolderName { get;private set; }
        public string CardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }        
        public string PaymentNumber { get;  set; }         
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string PayerDocument { get; private set; }
        public EDocumentType PayerDocumentType { get; private set; }
        public string PayerEmail { get; private set; }
         public string Street { get;private set; }
        public string Number { get;private set; }
        public string Neighborhood { get;private set; }
        public string City { get;private set; }
        public string State { get;private set; }
        public string Country { get;private set; }
        public string ZipCode { get;private set; }
         public void Validate()
        {
             AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName,3,"Name.FirstName","Debe ser de almenos 3 caracteres")
                .HasMinLen(LastName,3,"Name.LastName","Debe ser de almenos 3 caracteres")
                .HasMaxLen(FirstName,40,"Name.FirstName","Debe ser de maximo 40 caracteres")
                .HasMaxLen(LastName,40,"Name.LastName","Debe ser de máximo 40 caracteres")
            );
        }
    }
}