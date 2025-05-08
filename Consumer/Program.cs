using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using DataLayer.KrediPortal; 
using Common.KrediPortal.DTO;

var factory = new ConnectionFactory()
{
    Uri = new Uri("amqps://cacjbhxg:qiK63wE3p7hNHDeddnowXMaQphDcTQ8N@puffin.rmq2.cloudamqp.com/cacjbhxg")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();



var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume("kullanici-basvuru-kuyruk", true, consumer);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var json = Encoding.UTF8.GetString(body);

    Console.WriteLine("Başvuru Alındı Lütfen Bekleyiniz");

    Thread.Sleep(5000); 

    var basvuru = JsonConvert.DeserializeObject<DTOKullaniciBasvuru>(json);

    KullaniciBasvuruDataAccess dataAccess = new KullaniciBasvuruDataAccess();
   
    bool result = dataAccess.EkleKullaniciBasvuru(basvuru);

    Console.WriteLine(result ? "Veritabanına kaydedildi." : "Kayıt başarısız.");
    
};



Console.ReadLine();
