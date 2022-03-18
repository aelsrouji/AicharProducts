using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.OrderAPI.Messages;
using Services.OrderAPI.Models;
using Services.OrderAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer: IAzureServiceBusConsumer
    {
        private readonly OrderRepository _orderRepository;
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionCheckOut;
        private readonly string checkoutMessageTopic;
        private ServiceBusProcessor checkOutProcessor;


        private readonly IConfiguration _configuration;

        public AzureServiceBusConsumer(OrderRepository orderRepository, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;

            serviceBusConnectionString = configuration.GetValue<string>("ServiceBusConnectionString");
            checkoutMessageTopic = configuration.GetValue<string>("CheckoutMessageTopic");
            subscriptionCheckOut = configuration.GetValue<string>("SubscriptionCheckOut");

            var client = new ServiceBusClient(serviceBusConnectionString);
            checkOutProcessor = client.CreateProcessor(checkoutMessageTopic,subscriptionCheckOut);

        }

        public async Task Start()
        {
            checkOutProcessor.ProcessMessageAsync += OnCheckOutMessageReceived;
            checkOutProcessor.ProcessErrorAsync += ErrorHandler;

            await checkOutProcessor.StartProcessingAsync();
        }
        
        public async Task Stop()
        {
     
            await checkOutProcessor.StopProcessingAsync();
            await checkOutProcessor.DisposeAsync();

        }

        Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnCheckOutMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            CheckoutHeaderDto checkoutHeaderDto = JsonConvert.DeserializeObject<CheckoutHeaderDto>(body);

            OrderHeader orderHeader = new()
            {
                UserId = checkoutHeaderDto.UserId,
                FirstName = checkoutHeaderDto.FirstName,
                LastName = checkoutHeaderDto.LastName,
                OrderDetails = new List<OrderDetails>(),
                CardNumber = checkoutHeaderDto.CardNumber,
                CouponCode = checkoutHeaderDto.CouponCode,
                CVV = checkoutHeaderDto.CVV,
                DiscountTotal = checkoutHeaderDto.DiscountTotal,
                Email = checkoutHeaderDto.Email,
                ExpirtyMonthYear = checkoutHeaderDto.ExpirtyMonthYear,
                OrderTime = DateTime.Now,
                OrderTotal = checkoutHeaderDto.OrderTotal,
                PaymentStatus= false,
                Phone = checkoutHeaderDto.Phone,
                PickupDatetime = checkoutHeaderDto.PickupDatetime
            };


            foreach (var detailsList in checkoutHeaderDto.CartDetails)
            {
                OrderDetails orderDetails = new()
                {
                    ProductId = detailsList.ProductId,
                    ProductName = detailsList.Product.Name,
                    ProductPrice = detailsList.Product.Price,
                    Count = detailsList.Count
                };
                orderHeader.CartTotalItems += detailsList.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }

            await _orderRepository.AddOrder(orderHeader);

        }
    }
}
