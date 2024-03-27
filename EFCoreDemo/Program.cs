using EFCoreDemo;
using EFCoreDemo.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;


using EFDbContext Db = new();

#region 增加
//Db.Books.Add(new() { Title = "自由的鸟", AuthorName = "阿刁", Price = 29.99, PublishTime = DateTime.Now, });
//Db.Books.Add(new() { Title = "有点自由的鸟", AuthorName = "阿刁", Price = 58, PublishTime = DateTime.Now, });
//Db.Books.Add(new() { Title = "不是很自由的鸟", AuthorName = "阿刁", Price = 80, PublishTime = DateTime.Now, });
//Db.Books.Add(new() { Title = "放弃自由的鸟", AuthorName = "阿刁", Price = 99, PublishTime = DateTime.Now, });
//Db.Books.Add(new() { Title = "没有自由的鸟", AuthorName = "阿刁", Price = 246, PublishTime = DateTime.Now, });
//await Db.SaveChangesAsync();

//Author author = new() { Name = "洪洪洪洪洪" };
//Console.WriteLine("添加前：" + JsonConvert.SerializeObject(author));
//Db.Authors.Add(author);
//Console.WriteLine("添加后：" + JsonConvert.SerializeObject(author));
//await Db.SaveChangesAsync();
//Console.WriteLine("保存后：" + JsonConvert.SerializeObject(author));


//Article article = new()
//{
//    Title = "喜大普奔！传洪喝酒啦！",
//    Content = @"近日，一则令人振奋的消息在社交网络上迅速传播开来——知名人士传洪破天荒地参加了一场酒会，并与众友人开怀畅饮。这一消息不仅引发了广大网友的热烈讨论，更在多个社交平台引发了一股“喜大普奔”的热潮。

//据了解，传洪一直以来以严谨自律著称，鲜少公开露面于酒会等场合。然而，在近日的一次私人聚会上，传洪却意外地出现在了酒桌旁，与在场的朋友们举杯共饮，展现了其难得的轻松一面。

//酒会上，传洪身着休闲装，面带微笑，与友人们谈笑风生。他不仅主动举杯敬酒，还与大家分享了自己的生活感悟和工作经验。现场气氛热烈而融洽，不时爆发出阵阵欢笑声。

//对于传洪此次喝酒的举动，网友们纷纷表示惊喜和祝福。有网友评论道：“传洪一直以来都是我们的榜样，他的自律和勤奋让人敬佩。看到他也能放下包袱，享受生活的美好，真是让人感到欣慰。”还有网友调侃道：“传洪终于‘接地气’了，这下我们可以和他一起喝酒聊天了！”

//传洪的这次举动不仅展现了他个人的多面性，也传递出了一种积极的生活态度。在忙碌的工作之余，适当地放松自己，享受生活的乐趣，是每个人都应该追求的目标。

//同时，这也提醒我们，无论是公众人物还是普通人，都应该保持一颗平常心，不要过分追求完美和自律。适当地放松和享受生活，才能更好地面对工作和生活的挑战。

//传洪喝酒的消息虽然只是一则简单的社交新闻，但却引发了广泛的关注和讨论。它让我们看到了传洪的另一面，也让我们思考如何在忙碌的生活中找到平衡和乐趣。希望在未来，传洪能够继续以积极、乐观的态度面对生活，为我们带来更多的惊喜和感动。"
//};
//article.Comments.Add(new() { Message="牛逼牛逼！！！"});
//article.Comments.Add(new() { Message="传洪起飞！"});
//article.Comments.Add(new() { Message="这波只能给98.4分，毕竟传洪真的有1.6！"});

//Db.Articles.Add(article);
//await Db.SaveChangesAsync();




//User user = new() { Name = "夏传洪" };
//Leave leave = new()
//{
//    Requester = user,
//    From = new DateTime(2024, 4, 3),
//    To = new DateTime(2024, 4, 10),
//    Remarks = "祭天",
//    Status = 0
//};

//Db.Users.Add(user);
//Db.Leaves.Add(leave);

//await Db.SaveChangesAsync();



Order order = new()
{
    Address = "某某市某某区",
    Name = "USB充电器"
};

Delivery delivery = new()
{
    CompanyName = "蜗牛快递",
    Number = "SN333322888",
    Order = order
};
Db.Deliveries.Add(delivery);
await Db.SaveChangesAsync();

Order order1 = await Db.Orders.Include(o => o.Delivery).FirstAsync(o => o.Name.Contains("充电器"));
Console.WriteLine(JsonConvert.SerializeObject(order1, Formatting.Indented));

#endregion

#region 查询
//Console.WriteLine("***所有书***");
//foreach (var book in Db.Books)
//{
//    Console.WriteLine(JsonConvert.SerializeObject(book));
//}


//Console.WriteLine("\n\n价格大于80的书：");
//foreach (var book in Db.Books.Where(b => b.Price > 80))
//{
//    Console.WriteLine(JsonConvert.SerializeObject(book));
//}

//Console.WriteLine("\n\n价格倒序：");
//foreach (var book in Db.Books.OrderByDescending(b => b.Price))
//{
//    Console.WriteLine(JsonConvert.SerializeObject(book));
//}

//Console.WriteLine("\n\n");
//Console.WriteLine(new string('-', 120));

//Article article = Db.Articles.Include(a => a.Comments).Single(a => a.Id == 1);
//Console.WriteLine(JsonConvert.SerializeObject(article, Formatting.Indented));
#endregion

#region 改
//Book book = Db.Books.Single(b => b.Title == "不是很自由的鸟");
//Console.WriteLine(JsonConvert.SerializeObject(book));
//book.Price = 82;
//await Db.SaveChangesAsync();
//Console.WriteLine(JsonConvert.SerializeObject(book));
#endregion


#region 删除
//Book b = new() { Title = "自由的鸟123", AuthorName = "阿刁", Price = 29.99, PublishTime = DateTime.Now, };
//Db.Books.Add(b);
//await Db.SaveChangesAsync();
//Console.WriteLine(JsonConvert.SerializeObject(Db.Books.Single(b => b.Title == "自由的鸟123")));
//Db.Books.Remove(Db.Books.Single(b => b.Title == "自由的鸟123"));
//await Db.SaveChangesAsync();
#endregion


