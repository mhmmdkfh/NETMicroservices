using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PaymentService.Dtos;
using PaymentService.Models;

namespace PaymentService.Profiles
{
    public class PaymenstProfile : Profile
    {
        public PaymenstProfile()
        {
            CreateMap<Enrollment,EnrollmentReadDto>();
            CreateMap<EnrollmentPublishedDto, Enrollment>()
            .ForMember(dest=>dest.EnrollmentID,
            opt=>opt.MapFrom(src=>src.EnrollmentID));
        }
    }
}