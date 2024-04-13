﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ApiDemo.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult<Person> GetPerson(int id) => id switch
    {
        <= 0 => BadRequest("id必须是正数"),
        1 => new Person(1, "洪洪洪", 18),
        2 => new Person(2, "刁刁刁", 18),
        3 => new Person(2, "旺旺旺", 18),
        _ => NotFound(new ErrorInfo(2, "人员不存在"))
    };

    [HttpGet("/school/{schoolName}/class/{classNo}")]
    public ActionResult<Student[]> GetAll(string schoolName, [FromRoute(Name = "classNo")] int classNum)
    {
        return NotFound();


    }

    [HttpPost]
    public ActionResult<Student[]> Post([FromBody] string json, [FromBody] JObject postJson)
    {
        return NotFound();
    }
}

public record Person(int Id, string Name, int Age);
public record Student(int Id, string Name, int Age, string schoolName);
public record ErrorInfo(int Code, string? Message);