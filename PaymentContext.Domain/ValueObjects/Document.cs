using System;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document:ValueObject
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;

            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Validate(),"Document.Number","Documento  invalido")
            );
        }

        public string Number { get;private set; }
        public EDocumentType Type { get;private set; }

        private bool Validate(){
            if(Type == EDocumentType.DNI && Number.Length==8)
                return true;

            if(Type == EDocumentType.CE && Number.Length==11)
                return true;

            return false;
        }
    }
}