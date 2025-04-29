using Common.KrediPortal.DTO;
using Common.KrediPortal.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

public class KullaniciBasvuruService : IBasvuru
{
    public DTOKullaniciBasvuru EkleKullaniciBasvuru(DTOKullaniciBasvuru kullaniciBasvuru)
    {
        if (string.IsNullOrWhiteSpace(kullaniciBasvuru.Ad))
            throw new ArgumentException("Ad boş olamaz.");

        if (kullaniciBasvuru.KrediTutar <= 0)
            throw new ArgumentException("Kredi tutarı geçersiz.");

        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqps://cacjbhxg:qiK63wE3p7hNHDeddnowXMaQphDcTQ8N@puffin.rmq2.cloudamqp.com/cacjbhxg")
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "kullanici-basvuru-kuyruk", durable: true, exclusive: false, autoDelete: false);

        var json = JsonConvert.SerializeObject(kullaniciBasvuru);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: "", routingKey: "kullanici-basvuru-kuyruk", body: body);

        return kullaniciBasvuru; 
    }
}
