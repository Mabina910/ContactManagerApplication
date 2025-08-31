using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient; // MySQL library
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class ContactsAdoController : Controller
    {
        private readonly string _connectionString;

        public ContactsAdoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: /ContactsAdo
        public IActionResult Index()
        {
            List<Contact> contacts = new List<Contact>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT Id, FullName, Email, Phone, Address, Notes, CreatedAt, UpdatedAt FROM Contacts";
                var cmd = new MySqlCommand(query, conn);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contacts.Add(new Contact
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FullName = reader["FullName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Address = reader["Address"].ToString(),
                            Notes = reader["Notes"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                        });
                    }
                }
            }

            return View(contacts);
        }

        // GET: /ContactsAdo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ContactsAdo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO Contacts 
                                    (FullName, Email, Phone, Address, Notes, CreatedAt, UpdatedAt) 
                                     VALUES 
                                    (@FullName, @Email, @Phone, @Address, @Notes, @CreatedAt, @UpdatedAt)";

                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FullName", contact.FullName);
                    cmd.Parameters.AddWithValue("@Email", contact.Email);
                    cmd.Parameters.AddWithValue("@Phone", contact.Phone);
                    cmd.Parameters.AddWithValue("@Address", contact.Address);
                    cmd.Parameters.AddWithValue("@Notes", contact.Notes);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(contact);
        }
    }
}
