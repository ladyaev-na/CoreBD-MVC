using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CoreBD.Pages.Users
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorsMessage = ""; // ���������� �� errorsMessage
        public string successMessage = ""; // ���������� �� successMessage

        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.surname = Request.Form["surname"];

            if (clientInfo.name.Length == 0 || clientInfo.surname.Length == 0)
            {
                errorsMessage = "������!"; 
                return;
            }
            try
            {
                String connectionString = "Server=localhost\\SQLEXPRESS;Database=CoreBD;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO users (name, surname) VALUES (@name, @surname);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@surname", clientInfo.surname);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorsMessage = ex.Message;
                return;
            }

            clientInfo.name = "";
            clientInfo.surname = "";
            successMessage = "������ ��������";

            Response.Redirect("/Users/Index");
        }
    }
}
