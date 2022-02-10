using AutoMapper;
using EventBus.Messages.Events;
using Ordering.API.Entities;

namespace Ordering.API.Mapping
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<Order, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
