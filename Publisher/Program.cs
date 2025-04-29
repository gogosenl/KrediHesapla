using RabbitMQ.Client;
using System.Text;
using System;

class Program
{
    static void Main(string[] args)
    {
        for (int i = 0; i < 10; i++)
        {
            MesajGonder();
        }
        Console.ReadLine();

    }

    static void MesajGonder()
    {
        var factory = new ConnectionFactory();
        factory.Uri = new Uri("amqps://cacjbhxg:qiK63wE3p7hNHDeddnowXMaQphDcTQ8N@puffin.rmq2.cloudamqp.com/cacjbhxg");

        using var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare("mesaj-kuyruk", true, false, false);

        var mesaj = "Deneme Mesaj";
        var body = Encoding.UTF8.GetBytes(mesaj);

        channel.BasicPublish(string.Empty, "mesaj-kuyruk", null, body);

        Console.WriteLine("Mesaj gönderildi: " + mesaj);
    }

}
