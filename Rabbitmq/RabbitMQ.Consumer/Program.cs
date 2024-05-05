using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new()
{
    HostName = "139.159.254.248",
    UserName = "zwt",
    Password = "zwt",
    DispatchConsumersAsync = true
};

string exchangeName = "exchange1";
string routingKey = "myEvent";

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

string queueName = "queue1";
channel.ExchangeDeclare(exchange: exchangeName, type: "direct");

channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);

AsyncEventingBasicConsumer consumer = new(channel);
consumer.Received += async(sender, args) =>
{
    try
    {
        var bytes = args.Body.ToArray();
        string msg = Encoding.UTF8.GetString(bytes);
        Console.WriteLine($"{DateTime.Now}收到了消息：{msg}");
        channel.BasicAck(args.DeliveryTag, multiple: false);
        await Task.Delay(1000);

    }
    catch (Exception ex)
    {
        channel.BasicReject(args.DeliveryTag, true);
        Console.WriteLine($"处理收到的消息一场：{ex}");
    }
};
channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

Console.ReadLine();