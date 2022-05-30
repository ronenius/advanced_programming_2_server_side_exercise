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
    public class MessagesController : Controller
    {
        private readonly IMessageService _service;

        public MessagesController(advanced_programming_2_server_side_exerciseContext context)
        {
            _service = new MessageService(context);
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _service == null)
            {
                return NotFound();
            }

            var message = await _service.Get((int)id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FromUsername,ToUsername,Content,Created")] Message message)
        {
            if (ModelState.IsValid)
            {
                await _service.Create(message.FromUsername, message.ToUsername, message.Content, message.Created);
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _service == null)
            {
                return NotFound();
            }

            var message = await _service.Get((int)id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FromUsername,ToUsername,Content,Created")] Message message)
        {
            if (id != message.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Edit(message);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MessageExists(message.Id))
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
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _service == null)
            {
                return NotFound();
            }

            var message = await _service.Get((int)id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_service == null)
            {
                return Problem("Entity set 'advanced_programming_2_server_side_exerciseContext.Message'  is null.");
            }
            var message = await _service.Get(id);
            if (message != null)
            {
                await _service.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MessageExists(int id)
        {
            return await _service.isIdExist(id);
        }
    }
}
