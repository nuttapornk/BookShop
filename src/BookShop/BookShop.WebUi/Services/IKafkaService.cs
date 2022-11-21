using Confluent.Kafka;

namespace BookShop.WebUi.Services
{
    public interface IKafkaService
    {
        Task<DeliveryResult<Null, string>> ProduceAsync(string topic, string message);
        Task<DeliveryResult<Null, string>> ProduceAsync<T>(string topic, T message);
    }
}
