using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using advanced_programming_2_server_side_exercise.Data;
using advanced_programming_2_server_side_exercise.Models;
using advanced_programming_2_server_side_exercise.Services;

namespace advanced_programming_2_server_side_exercise.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService _service;

        public ContactsController(advanced_programming_2_server_side_exerciseContext context)
        {
            _service = new ContactService(context);
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _service == null)
            {
                return NotFound();
            }

            var contact = await _service.Get(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContactServer,ContactNickname")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _service.Create("ronen", contact.Id, contact.ContactServer, contact.ContactNickname);
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _service == null)
            {
                return NotFound();
            }

            var contact = await _service.Get(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ContactServer,ContactNickname")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Edit(contact);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ContactExists(contact.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _service == null)
            {
                return NotFound();
            }

            var contact = await _service.Get(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_service == null)
            {
                return Problem("Entity set 'advanced_programming_2_server_side_exerciseContext.Contact'  is null.");
            }
            var contact = await _service.Get(id);
            if (contact != null)
            {
                await _service.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ContactExists(string id)
        {
            return await _service.isIdExist(id);
        }
    }
}
