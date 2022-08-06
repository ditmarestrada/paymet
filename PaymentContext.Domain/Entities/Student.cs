using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions=new List<Subscription>();

            AddNotifications(name,document,email);

        }

        public Name Name { get;private set; }
        public Document Document { get;private set; }
        public Email Email { get;private set; }
        public Address Address { get;private set; }
        public IReadOnlyCollection<Subscription> Subscriptions {get{return _subscriptions.ToArray();}}        


        public void AddSubscription(Subscription subscription){

            var hasSubscriptionActive = HasSubscriptionActive();

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive,"Student.Subscriptions","Usted ya tiene una subscripcion activa")
                .IsLowerThan(0,subscription.Payments.Count,"Student.Subscription.Payment","Esta subscripcion no pose pago")
            );

            // Alternativa
            //    if(hasSubscriptionActive)
            //         AddNotification("Studen.Subscription","Usted ya cuenta con una susbcripción activa.");
           if(Valid)
                _subscriptions.Add(subscription);

        }

        private bool HasSubscriptionActive(){
             var hasSubscriptionActive = false;

            foreach (var sub in Subscriptions)
                if(sub.Active){
                    hasSubscriptionActive=true;
                    break;   
                }
             
             return hasSubscriptionActive;
        }
    }

    
}