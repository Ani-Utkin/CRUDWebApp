using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyApp.Pages.Clients
{
    public class IndexModel : PageModel
    {

        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {

            try 
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=myapp;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString)) 
                {
                    conn.Open();
                    String sql = "SELECT * FROM clients";

                    using (SqlCommand command = new SqlCommand(sql, conn)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.ID = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.createdAt = reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Message" + ex.ToString());
            }
        }
    }

    public class ClientInfo {

        public String ID { get; set; }
        public String name { get; set; }
        public String address { get; set; }
        public String email { get; set; }
        public String phone { get; set; }

        public String createdAt { get; set; }

    }
}
