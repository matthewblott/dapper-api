using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace dapper_api.Controllers
{
  [Route("api/[controller]")]
  public class PersonController : Controller
  {
    PersonService service;

    public PersonController()
    {
      service = new PersonService();
    }

    // GET api/values
    [HttpGet]
    public IEnumerable<Person> Get()
    {
      return service.Get();
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public Person Get(int id)
    {
      return service.GetById(id);
    }

    [HttpPost]
    public int Post(Person person)
    {
      return service.Add(person);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put([FromBody]Person person)
    {
      service.Update(person);
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      service.Delete(id);
    }

  }

}