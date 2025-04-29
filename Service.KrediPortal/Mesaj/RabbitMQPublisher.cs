using Common.KrediPortal.DTO;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Service.KrediPortal.Mesaj
{
    public class RabbitMQPublisher
    {
        private readonly string _uri = "amqps://cacjbhxg:qiK63wE3p7hNHDeddnowXMaQphDcTQ8N@puffin.rmq2.cloudamqp.com/cacjbhxg";

        public void PublishKullaniciBasvuru(DTOKullaniciBasvuru basvuru)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(_uri) };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "kullanici-basvuru-kuyruk", durable: true, exclusive: false, autoDelete: false);

            string jsonMessage = JsonSerializer.Serialize(basvuru);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            channel.BasicPublish(exchange: "",
                                 routingKey: "kullanici-basvuru-kuyruk",
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine("RabbitMQ mesaj gönderildi: " + jsonMessage);
        }
    }
}
