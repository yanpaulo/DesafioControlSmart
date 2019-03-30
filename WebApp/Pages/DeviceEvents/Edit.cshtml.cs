﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DesafioControlSmart.Data;
using DesafioControlSmart.WebApp.Data;

namespace DesafioControlSmart.WebApp.Pages.DeviceEvents
{
    public class EditModel : PageModel
    {
        private readonly DesafioControlSmart.WebApp.Data.ApplicationDbContext _context;

        public EditModel(DesafioControlSmart.WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DeviceEvent DeviceEvent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeviceEvent = await _context.DeviceEvents.FirstOrDefaultAsync(m => m.Id == id);

            if (DeviceEvent == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DeviceEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceEventExists(DeviceEvent.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DeviceEventExists(int id)
        {
            return _context.DeviceEvents.Any(e => e.Id == id);
        }
    }
}
