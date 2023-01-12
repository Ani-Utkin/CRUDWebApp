using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyApp.Pages.Clients
{
    public class EditModel : PageModel
    {

        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String ID = Request.Query["ID"];
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=myapp;Integrated Security=True";

                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                String sql = "SELECT * FROM clients WHERE id=@ID";
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", ID);
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    clientInfo.ID = "" + reader.GetInt32(0);
                    clientInfo.name = reader.GetString(1);
                    clientInfo.email = reader.GetString(2);
                    clientInfo.phone = reader.GetString(3);
                    clientInfo.address = reader.GetString(4);
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            clientInfo.ID = Request.Form["ID"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
                clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All fields are required!!";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=myapp;Integrated Security=True";

                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                String sql = "UPDATE clients " +
                             "SET name=@name, email=@email, phone=@phone, address=@address " +
                             "WHERE id=@ID";
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@name", clientInfo.name);
                command.Parameters.AddWithValue("@email", clientInfo.email);
                command.Parameters.AddWithValue("@phone", clientInfo.phone);
                command.Parameters.AddWithValue("@address", clientInfo.address);
                command.Parameters.AddWithValue("@ID", clientInfo.ID);


                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Index");
        }
    }
}
