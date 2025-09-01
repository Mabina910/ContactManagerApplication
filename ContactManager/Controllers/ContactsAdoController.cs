using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class ContactsAdoController : Controller
    {
        private readonly string _connectionString = string.Empty;


        public ContactsAdoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: /ContactsAdo
        public IActionResult Index()
        {
            var contacts = new List<Contact>();
            using (var conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Contacts";
                var cmd = new MySqlCommand(query, conn);
                conn.Open();
                using var reader = cmd.ExecuteReader();
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
            return View(contacts);
        }

        // GET: /ContactsAdo/Details/5
        public IActionResult Details(int id)
        {
            Contact contact = null;
            using (var conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Contacts WHERE Id = @Id";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    contact = new Contact
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FullName = reader["FullName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        Notes = reader["Notes"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                    };
                }
            }
            if (contact == null) return NotFound();
            return View(contact);
        }

        // GET: /ContactsAdo/Create
        public IActionResult Create() => View();

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

        // GET: /ContactsAdo/Edit/5
        public IActionResult Edit(int id)
        {
            Contact contact = null;
            using (var conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Contacts WHERE Id = @Id";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    contact = new Contact
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FullName = reader["FullName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        Notes = reader["Notes"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                    };
                }
            }
            if (contact == null) return NotFound();
            return View(contact);
        }

        // POST: /ContactsAdo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    string query = @"UPDATE Contacts SET 
                                    FullName=@FullName, Email=@Email, Phone=@Phone, Address=@Address, Notes=@Notes, UpdatedAt=@UpdatedAt 
                                    WHERE Id=@Id";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", contact.Id);
                    cmd.Parameters.AddWithValue("@FullName", contact.FullName);
                    cmd.Parameters.AddWithValue("@Email", contact.Email);
                    cmd.Parameters.AddWithValue("@Phone", contact.Phone);
                    cmd.Parameters.AddWithValue("@Address", contact.Address);
                    cmd.Parameters.AddWithValue("@Notes", contact.Notes);
                    cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: /ContactsAdo/Delete/5
        public IActionResult Delete(int id)
        {
            Contact contact = null;
            using (var conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Contacts WHERE Id=@Id";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    contact = new Contact
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FullName = reader["FullName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        Notes = reader["Notes"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
                    };
                }
            }
            if (contact == null) return NotFound();
            return View(contact);
        }

        // POST: /ContactsAdo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM Contacts WHERE Id=@Id";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
