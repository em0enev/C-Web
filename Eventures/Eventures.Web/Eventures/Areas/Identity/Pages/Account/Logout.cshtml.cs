﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventures.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Eventures.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<EventuresUser> _signInManager;

        public LogoutModel(SignInManager<EventuresUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await _signInManager.SignOutAsync();

            return LocalRedirect("/Home/Index");
        }
    }
}