using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Data;
using PaymentService.Dtos;
using PaymentService.Models;

namespace PaymentService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        private EventType DetermineEvent(string notifcationMessage){
            Console.WriteLine("--> Determining Event");
            //convert pesan dari json ke obj
            var eventType = JsonSerializer.Deserialize<GenericEvent>(notifcationMessage);
            switch(eventType.Event){
                case "Enrollment_Published":
                    Console.WriteLine("Enrollment Published Event Detected");
                    return EventType.EnrollmentPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch(eventType){
                case EventType.EnrollmentPublished:
                    addEnrollment(message);
                    break;
                default:
                    break;
            }
        }

        private void addEnrollment(string enrollmentPublishMessage){
            using(var scope = _scopeFactory.CreateScope()){
                var repo = scope.ServiceProvider.GetRequiredService<IPaymentRepo>();
                var enrollmentPublishedDto = 
                JsonSerializer.Deserialize<EnrollmentPublishedDto>(enrollmentPublishMessage);
                try{
                    var plat = _mapper.Map<Enrollment>(enrollmentPublishedDto);
                    if(!repo.ExternalEnrollmentExist(plat.EnrollmentID)){
                        repo.CreateEnrollment(plat);
                        repo.SaveChanges();
                        Console.WriteLine("--> Platform added !");
                    }
                    else{
                        Console.WriteLine("--> Platform alredy exist");
                    }
                }catch(Exception ex){
                    Console.WriteLine($"--> Could not add Platform to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType{
        EnrollmentPublished,
        Undetermined
    }
}