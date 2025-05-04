using Microsoft.Data.SqlClient;
using TestJenkinsWithUnit.Logics.Interface;
using TestJenkinsWithUnit.Models;

namespace TestJenkinsWithUnit.Logics.Repo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;
        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public List<Employee> GetAll()
        {
            List<Employee> list = new();
            using SqlConnection con = new(_connectionString);
            SqlCommand cmd = new("SELECT * FROM Employees", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new Employee
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Email = dr["Email"].ToString(),
                    Department = dr["Department"].ToString(),
                    Salary = Convert.ToDecimal(dr["Salary"])
                });
            }
            con.Close();
            con.Dispose();
            cmd.Dispose();
            return list;
        }

        public Employee GetById(int id)
        {
            Employee emp = new();
            using SqlConnection con = new(_connectionString);
            SqlCommand cmd = new("SELECT * FROM Employees WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                emp.Id = Convert.ToInt32(dr["Id"]);
                emp.Name = dr["Name"].ToString();
                emp.Email = dr["Email"].ToString();
                emp.Department = dr["Department"].ToString();
                emp.Salary = Convert.ToDecimal(dr["Salary"]);
            }
            con.Close();
            con.Dispose();
            cmd.Dispose();
            return emp;
        }

        public void Add(Employee emp)
        {
            using SqlConnection con = new(_connectionString);
            SqlCommand cmd = new("INSERT INTO Employees (Name, Email, Department, Salary) VALUES (@Name, @Email, @Dept, @Salary)", con);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@Dept", emp.Department);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            cmd.Dispose();
        }

        public void Update(Employee emp)
        {
            using SqlConnection con = new(_connectionString);
            SqlCommand cmd = new("UPDATE Employees SET Name=@Name, Email=@Email, Department=@Dept, Salary=@Salary WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", emp.Id);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@Dept", emp.Department);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            cmd.Dispose();
        }

        public void Delete(int id)
        {
            using SqlConnection con = new(_connectionString);
            SqlCommand cmd = new("DELETE FROM Employees WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            cmd.Dispose();
        }
    }

}
