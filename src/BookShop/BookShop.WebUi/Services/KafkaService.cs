using BookShop.Common;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace BookShop.WebUi.Services
{
    public class KafkaService : IKafkaService
    {
        private readonly ProducerConfig _config;
        private readonly KafkaSetting _kafkaSetting;
        public KafkaService(IOptions<KafkaSetting> kafkaSetting)
        {
            _kafkaSetting = kafkaSetting.Value;
            _config = new ProducerConfig()
            {                
                BootstrapServers = _kafkaSetting.BootstrapServices,
            };
        }

        public async Task<DeliveryResult<Null, string>> ProduceAsync(string topic, string message)
        {
            using var p = new ProducerBuilder<Null, string>(_config).Build();
            var deliveryResult = await p.ProduceAsync(topic, new Message<Null, string>
            {                
                Value = message,
            });
            return deliveryResult;
        }

        public async Task<DeliveryResult<Null, string>> ProduceAsync<T>(string topic, T message)
        {
            using var p = new ProducerBuilder<Null, string>(_config).Build();
            var messageString = JsonConvert.SerializeObject(message);
            var deliveryResult = await p.ProduceAsync(topic, new Message<Null, string>
            {
                Value = messageString,
            });
            return deliveryResult;
        }

    }
}
