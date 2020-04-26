using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw3.DAL;
using Cw3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s16541;Integrated Security=True";


        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }



        /*  [HttpGet]
          public IActionResult GetStudent(string orderBy)
          {
              return Ok(_dbService.GetStudents());

          }



          [HttpGet("{id}")]
          public IActionResult GetStudent(int id)
          {
              if (id == 1)
              {
                  return Ok("Kowalski");

              }
              else if (id == 2)
              {
                  return Ok("Geteralski");
              }
              else if (id == 2)
              {
                  return Ok("fofofof");
              }

              return NotFound("Nie ma studenta");
          }

          [HttpPost]
          public IActionResult CreateStudent(Student student)
          {
              student.IndexNumber = $"s{new Random().Next(1, 20000)}";
              return Ok(student);
          }

          [HttpPut("{id}")]
          public IActionResult Actualiz(int id)
          {
              return Ok(" Ąktualizacja dokończona");

          }

          [HttpDelete("{id}")]
          public IActionResult Del(int id)
          {
              return Ok(" Usuwanie ukończone");

          }*/

        

       // private IStudentsDal _dbService;

        [HttpGet]
        public IActionResult GetStudent()
        {
            var list = new List<Student>(); 
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select FirstName, LastName, BirthDate, StadName, SemNumber from Student join Enrollment ON Student.IDENROLLMENT = ENROLLMENT.IDENROLLMENT join Studies ON ENROLLMENT.IDSTUDY = STUDIES.IDSTUDY";

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = (DateTime)dr["BirthDate"];
                    st.StadName = dr["Name"].ToString();
                    st.SemNumber = (int)dr["Semester"];
                    list.Add(st);
                }
              
            }

            return Ok(list);

        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexnumber)
        {

            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from students where indexnumber='"+ indexnumber +"'";

                con.Open();
                SqlDataReader dr = com.ExecuteReader();

                if (dr.Read())
                {
                    var st = new Student();

                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                   // st.SemNumber = dr["Semester"].ToString();
                    
                    return Ok(st);
                }


                return NotFound("Nie ma studenta");
            }
        }


    }
}
