
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new()
{
    HostName = "139.159.254.248",
    UserName = "zwt",
    Password = "zwt",
    DispatchConsumersAsync = true,
};

string exchangeName = "exchange1";
string routingKey = "myEvent";

using var connection = factory.CreateConnection();

while (true)
{
    string msg = DateTime.Now.TimeOfDay.ToString();
    using (var channel = connection.CreateModel())
    {
        var properties = channel.CreateBasicProperties();
        properties.DeliveryMode = 2;    // Non-persistent (1) or persistent (2).
        channel.ExchangeDeclare(exchange: exchangeName, type: "direct");    // 声明交换机
        byte[] body = Encoding.UTF8.GetBytes(msg);
        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, mandatory: true,
                        basicProperties: properties, body: body);

    }

}