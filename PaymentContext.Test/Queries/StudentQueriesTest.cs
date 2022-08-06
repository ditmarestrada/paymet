using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Test.Queries
{
    [TestClass]
    public class StudentQueriesTest
    {
        private IList<Student> _students;

        public StudentQueriesTest(){
            _students = new List<Student>();
            for( var i=0; i <= 10; i++) {
                _students.Add(new Student(
                    new Name("Alumno",i.ToString()),
                    new Document("1111111"+i.ToString(), EDocumentType.DNI),
                    new Email(i.ToString()+"@gmail.com")
                ));
            }
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists(){
            var exp = StudentQueries.GetStudentInfo("12345678");
            var studentFounds = _students.AsQueryable().Where(exp).FirstOrDefault();
                        
            Assert.AreEqual(null, studentFounds);
            
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentExists(){
            var exp = StudentQueries.GetStudentInfo("11111111");
            var studentFounds = _students.AsQueryable().Where(exp).FirstOrDefault();
            
            Assert.AreNotEqual(null, studentFounds);
            
        }
    }
}