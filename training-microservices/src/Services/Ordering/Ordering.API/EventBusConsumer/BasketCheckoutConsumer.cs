using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Ordering.API.Entities;
using Ordering.API.Repositories;
using System;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IOrderRepositoryRedis _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        public BasketCheckoutConsumer(IOrderRepositoryRedis repository, IMapper mapper, ILogger<BasketCheckoutConsumer> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            //var order = _mapper.Map<Order>(context.Message);
            Order order = new Order {
                TotalPrice = context.Message.TotalPrice,
                UserName = context.Message.UserName,
                Id = Guid.NewGuid()
        };
            var result =   _repository.AddAsync(order);

            _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id : {newOrderId}", result.Id);
            return Task.CompletedTask;
        }

    }
}
