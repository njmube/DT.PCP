using System.Linq;
using AutoMapper;
using DT.PCP.CommonDomain;
using DT.PCP.Domain;
using DT.PCP.ServicesProxies.BddViolationService;
using DT.PCP.Web.ViewModels.Cabinet;
using DT.PCP.Web.ViewModels.Pay;

namespace DT.PCP.Utils.Impl
{
    public class AutomapBootstrap
    {
        public static void InitializeMap()
        {
            Mapper.CreateMap<User, UserInfoViewModel>()
                  .ForMember(dest => dest.IsEmailSubscribed,
                             opt =>
                             opt.MapFrom(
                                 src =>src.Notifications != null && src.Notifications.OfType<EmailNotification>().Count(n => n.IsConfirmed) > 0))
                  .ForMember(dest => dest.IsSmsSubscribed,
                             opt =>
                             opt.MapFrom(
                                 src => src.Notifications != null && src.Notifications.OfType<SmsNotification>().Count(n => n.IsConfirmed) > 0));
                //.ForMember(dest=>dest.IsSubscribed, opt =>opt.MapFrom(src=>src.EmailNotification || src.SmsNotification));

            Mapper.CreateMap<UserInfoViewModel, User>();
            Mapper.CreateMap<NotificationViewModel, NotificationSetting>();
            Mapper.CreateMap<User, NotificationViewModel>();
            
            Mapper.CreateMap<ViolationViewModel, OrderDetail>();
            Mapper.CreateMap<OrderDetail, ViolationViewModel>();
            Mapper.CreateMap<TrafficViolationData, PayedViolation>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.ViolatorFullName))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.FineCost));
            
            Mapper.CreateMap<TransportOwnerData, User>()
                .ForMember(dest => dest.CarColor, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.CarMark, opt => opt.MapFrom(src => src.Mark))
                .ForMember(dest => dest.CarModel, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.CarNumber, opt => opt.MapFrom(src => src.TransportNumber))
                .ForMember(dest => dest.CarPassportNumber, opt => opt.MapFrom(src => src.NumberSRTS))
                .ForMember(dest => dest.CarRegistrationAddress, opt => opt.MapFrom(src => src.RegisterAddress))
                .ForMember(dest => dest.CarYear, opt => opt.MapFrom(src => src.TransportYear))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.OwnerFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.OwnerLastName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.OwnerSecondName))
                .ForMember(dest => dest.IsArtificialPerson, opt => opt.MapFrom(src => src.IsLegalEntity))
                .ForMember(dest => dest.ArtificialPerson, opt => opt.MapFrom(src=>src.IsLegalEntity ? src.OwnerFullName:null));

            Mapper.CreateMap<TrafficViolationData, User>()
                 .ForMember(dest => dest.CarColor, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.CarMark, opt => opt.MapFrom(src => src.Mark))
                .ForMember(dest => dest.CarModel, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.CarNumber, opt => opt.MapFrom(src => src.TransportNumber))
                .ForMember(dest => dest.CarPassportNumber, opt => opt.MapFrom(src => src.NumberSRTS))
                .ForMember(dest => dest.CarRegistrationAddress, opt => opt.MapFrom(src => src.PostAddress))
               
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.ViolatorFirstname))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.ViolatorLastname))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.ViolatorSecondname))
                .ForMember(dest => dest.IsArtificialPerson, opt => opt.MapFrom(src => src.IsLegalEntity))
                .ForMember(dest => dest.ArtificialPerson, opt => opt.MapFrom(src => src.IsLegalEntity ? src.ViolatorFullName : null));

            Mapper.CreateMap<TrafficViolationData, CarViolationViewModel>()
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.FineCost))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.ViolatorFullName))
                .ForMember(dest => dest.Mark, opt => opt.MapFrom(src => src.Mark))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ViolationStatus))
                .ForMember(dest => dest.ViolationType, opt => opt.MapFrom(src => src.ViolationType))
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.OrderNumber))
                //.ForMember(dest => dest.Button, opt => opt.MapFrom(src => src.OrderNumber))
                .ForMember(dest => dest.FixationTime, opt => opt.MapFrom(src => src.FixationDateTime))
                .ForMember(dest => dest.PostAddress, opt => opt.MapFrom(src => src.PostAddress));

           

            Mapper.CreateMap<TrafficViolationData, CarViolationDetailsViewModel>()
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.FineCost))
                .ForMember(dest => dest.FixationDateTime, opt => opt.MapFrom(src => src.FixationDateTime))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.ViolatorFullName))
                .ForMember(dest => dest.Mark, opt => opt.MapFrom(src => src.Mark))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.TransportNumber))
                .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.NumberSRTS))
                .ForMember(dest => dest.PostAddress, opt => opt.MapFrom(src => src.PostAddress))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ViolationStatus))
                .ForMember(dest => dest.StatusEnum, opt => opt.MapFrom(src => src.ViolationStatusEnum))
                .ForMember(dest => dest.ViolationType, opt => opt.MapFrom(src => src.ViolationType))
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.OrderNumber));

        }

    }
}
