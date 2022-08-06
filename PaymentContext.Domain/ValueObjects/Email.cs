using System.Net.Http;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Email:ValueObject
    {
        public string Address { get;private set; }

        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract()
                .Requires()
                .IsEmail(address,"Email.Address","E-mail no valido")
            );
        }
    }
}