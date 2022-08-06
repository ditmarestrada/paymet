using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Test.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {
            //throw new System.NotImplementedException();
        }

        public bool DocumentExists(string document)
        {
            if(document=="12345678")
                return true;

            return false;
        }

        public bool EmailExists(string email)
        {
            if(email=="ditmarestrada@gmail.com")
                return true;
            return false;
        }
    }
}