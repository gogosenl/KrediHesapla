using Common.KrediPortal.DTO;
using Common.KrediPortal.Interfaces;
using DataLayer.KrediPortal;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Service.KrediPortal.Aspect;
using System.Text;

public class KullaniciBasvuruService : IBasvuru
{

    [ValidateKullaniciBasvuruAspect]
    public DTOKullaniciBasvuru EkleKullaniciBasvuru(DTOKullaniciBasvuru kullaniciBasvuru)
    {
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

    public List<DTOKullaniciBasvuruList> ListKullaniciBasvuru()
    {
        KullaniciBasvuruDataAccess dbBasvuruAccess = new KullaniciBasvuruDataAccess();
        return dbBasvuruAccess.ListKullaniciBasvuru();
    }
}
