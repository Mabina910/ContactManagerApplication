using Microsoft.AspNetCore.Mvc;
using ContactManager.Data;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Contacts
        public IActionResult Index()
        {
            var contacts = _context.Contacts.ToList();
            return View(contacts);
        }

        // GET: /Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.CreatedAt = DateTime.Now;
                contact.UpdatedAt = DateTime.Now;

                _context.Contacts.Add(contact);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: /Contacts/Edit/5
        public IActionResult Edit(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        // POST: /Contacts/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.UpdatedAt = DateTime.Now;
                _context.Contacts.Update(contact);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: /Contacts/Delete/5
        public IActionResult Delete(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        // POST: /Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /Contacts/Details/5
        public IActionResult Details(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null) return NotFound();
            return View(contact);
        }
    }
}
