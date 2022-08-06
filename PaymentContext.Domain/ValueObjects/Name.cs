using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name: ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName,3,"Name.FirstName","Debe ser de almenos 3 caracteres")
                .HasMinLen(LastName,3,"Name.LastName","Debe ser de almenos 3 caracteres")
                .HasMaxLen(FirstName,40,"Name.FirstName","Debe ser de maximo 40 caracteres")
                .HasMaxLen(LastName,40,"Name.LastName","Debe ser de máximo 40 caracteres")
            );

        }

        public string FirstName { get;private set; }
        public string LastName { get;private set; }
        
        public override string ToString(){
            return $"{FirstName} {LastName}";
        }
    }
}