using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace dapper_api
{
  public class PersonService
  {
    const string connStr = "server=windows-10;user id=cito;password=letmein";

    IDbConnection db;

    public PersonService()
    {
      db = new SqlConnection(connStr);
    }

    public int GetCount()
    {
      const string sql = "select count(*) from People with (nolock) ";

      return db.Query<int>(sql).Single();

    }

    public IEnumerable<Person> Get()
    {
      const string sql = @"
      
        select Id, FirstName, LastName, Active
        from People with (nolock) ";

      return db.Query<Person>(sql) as IList<Person>;

    }

    public Person GetById(int id)
    {
      const string sql = @"
      
        select Id, FirstName, LastName, Active
        from People with (nolock)
        where Id = @id ";

      return db.Query<Person>(sql, new { id }).SingleOrDefault();

    }

    public int Add(Person person)
    {
      var db = new SqlConnection(connStr);

      const string sql = @"
        
        insert into People (FirstName, LastName, Birthday, Active)
        values (@firstName, @lastName, @birthday, @active)

        select scope_identity() ";

      try
      {
        person.Id = db.Query<int>(sql, person).Single();
      }
      catch(Exception ex)
      {
        var message = ex.Message;

        Console.WriteLine(message);

      }

      return person.Id;

    }

    public void Update(Person person)
    {
      var sql = @"
        
        update
          People
        set
          FirstName = @firstName,
          LastName = @lastName,
          Birthday = @birthday,
          Active = @active
        where
          Id = @id ";

      db.Execute(sql, person);

    }

    public void Delete(int id)
    {
      var sql = "delete from People where Id = @id ";

      db.Execute(sql);

    }

  }

}