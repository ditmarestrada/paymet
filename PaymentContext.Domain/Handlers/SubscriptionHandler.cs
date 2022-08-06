using System;
using System.Data;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Hadlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
        Notifiable,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService){
            _repository = repository;
            _emailService =  emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();
            if(command.Invalid){
                AddNotifications(command);
                return new CommandResult(false, "No fue posible realizar la suscripción");
            }
           // Verificar si el docuemnto ya esta registrado
           if(_repository.DocumentExists(command.Document))
                AddNotification("Document","Este DNI ya esta en uso");

           // verificar si el email ya esta registrado
           if(_repository.EmailExists(command.Email))
                AddNotification("Email","Este E-mail ya esta en uso");

           // Generar los Values Objects

            var name = new Name(command.FirstName,command.LastName);
            var document=new Document(command.Document,EDocumentType.DNI);
            var email=new Email(command.Email);
            var address=new Address(command.Street, command.Number, command.Neighborhood, command.City,command.State,command.Country, command.ZipCode);

           //Generar las Entidades

            var student = new Student( name, document, email);            
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode, 
                command.BoletoNumber, 
                command.PaidDate,
                command.ExpireDate, 
                command.Total,
                command.TotalPaid,
                command.Payer, 
                new Document(command.Document, command.PayerDocumentType),
                address, 
                email
            );

            // Relaciones
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

           // Agrupar las validaciones
           AddNotifications(name,document, email, address, student, subscription, payment);

           // Verificar las notificaciones
           if(Invalid)
                return new CommandResult(false,"No fue posible realizar su subscripción");

           //Guardar las informacion
           _repository.CreateSubscription(student);

           // Notificar via Email

           _emailService.Send(student.Name.ToString(),student.Email.Address, "Bienvenido al ditlearn.pe", "Su subscripcion fue creada");

           // Retornar infomracion

           return new CommandResult(true,"Suscripción realizada de manera exitosa.");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();
            if(command.Invalid){
                AddNotifications(command);
                return new CommandResult(false, "No fue posible realizar la suscripción");
            }
           // Verificar si el docuemnto ya esta registrado
           if(_repository.DocumentExists(command.Document))
                AddNotification("Document","Este DNI ya esta en uso");

           // verificar si el email ya esta registrado
           if(_repository.EmailExists(command.Email))
                AddNotification("Email","Este E-mail ya esta en uso");

           // Generar los Values Objects

            var name = new Name(command.FirstName,command.LastName);
            var document=new Document(command.Document,EDocumentType.DNI);
            var email=new Email(command.Email);
            var address=new Address(command.Street, command.Number, command.Neighborhood, command.City,command.State,command.Country, command.ZipCode);

           //Generar las Entidades

            var student = new Student( name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,        
                command.PaidDate,
                command.ExpireDate, 
                command.Total,
                command.TotalPaid,
                command.Payer, 
                new Document(command.Document, command.PayerDocumentType),
                address, 
                email
            );

            // Relaciones
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

           // Agrupar las validaciones
           AddNotifications(name,document, email, address, student, subscription, payment);

           // Verficar Notificaciones
           if(Invalid)
                return new CommandResult(false, "No fue posible realizar su subscripción");

           //Guardar las informacion
           _repository.CreateSubscription(student);

           // Notificar via Email

           _emailService.Send(student.Name.ToString(),student.Email.Address, "Bienvenido al ditlearn.pe", "Su subscripcion fue creada");

           // Retornar infomracion

           return new CommandResult(true,"Suscripción realizada de manera exitosa.");
        }
    }
}