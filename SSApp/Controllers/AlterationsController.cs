using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SSApp.Data;
using SSApp.Models;
using SSApp.Services;

namespace SSApp
{
    public class AlterationsController : Controller
    {
        private readonly IAlterationService _service;
        private readonly ITopicClient _client;

        public AlterationsController(IAlterationService service, ITopicClient client)
        {
            _service = service;
            _client = client;
        }

        // GET: Alterations
        public async Task<IActionResult> Index()
        {
            var result = await _service.ListAsync();
            return View(result);
        }

        // GET: Alterations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alteration = await _service.DetailAsync(id);
            if (alteration == null)
            {
                return NotFound();
            }

            return View(alteration);
        }

        // GET: Alterations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alterations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,LeftLength,RightLength")] Alteration alteration)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(alteration);
                return RedirectToAction(nameof(Index));
            }
            return View(alteration);
        }

        // GET: Alterations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alteration = await _service.DetailAsync(id);
            if (alteration == null)
            {
                return NotFound();
            }
            return View(alteration);
        }

        // POST: Alterations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,Type,LeftLength,RightLength")] Alteration alteration)
        {
            if (id != alteration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(alteration);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (alteration.Status == StatusEnum.Paid || alteration.Status == StatusEnum.Done)
                {
                    // Send message to service bus
                    var messageBody = JsonConvert.SerializeObject(alteration);
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    await _client.SendAsync(message);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(alteration);
        }

        // GET: Alterations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alteration = await _service.DetailAsync(id);
            if (alteration == null)
            {
                return NotFound();
            }

            return View(alteration);
        }

        // POST: Alterations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
