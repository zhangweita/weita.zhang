using EFCoreDemo;
using EFCoreDemo.Model;
using Newtonsoft.Json;


using EFDbContext Db = new();

#region 增加
//Db.Books.Add(new() { Title = "自由的鸟", AuthorName = "阿刁", Price = 29.99, PublishTime = DateTime.Now, });
//Db.Books.Add(new() { Title = "有点自由的鸟", AuthorName = "阿刁", Price = 58, PublishTime = DateTime.Now, });
//Db.Books.Add(new() { Title = "不是很自由的鸟", AuthorName = "阿刁", Price = 80, PublishTime = DateTime.Now, });
//Db.Books.Add(new() { Title = "放弃自由的鸟", AuthorName = "阿刁", Price = 99, PublishTime = DateTime.Now, });
//Db.Books.Add(new() { Title = "没有自由的鸟", AuthorName = "阿刁", Price = 246, PublishTime = DateTime.Now, });
//await Db.SaveChangesAsync();
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
Db.Books.Remove(Db.Books.Single(b => b.Title == "自由的鸟123"));
await Db.SaveChangesAsync();
#endregion