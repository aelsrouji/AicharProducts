using System;
using System.Threading.Tasks;

namespace AicharProducts.ServiceBus
{
    public interface IMessageBus
    {
        Task PublishMessage(BaseMessage message, string topicName);

    }
}
