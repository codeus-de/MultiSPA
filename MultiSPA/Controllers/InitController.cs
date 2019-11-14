using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MultiSPA.Data;
using MultiSPA.Data.Entities;

namespace MultiSPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;

        public InitController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        // GET: api/Init/xyz
        [HttpGet("{email}/{pass}", Name = "Get")]
        public async Task<string> Get(string email, string pass)
        {
            var admin = await userManager.FindByNameAsync("Admin");
            if (admin != null)
                return "Init already completed.";

            try
            {
                await Initialize(email, pass);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "Init completed.";
        }

        private async Task Initialize(string email, string pass)
        {
            // Add the Admin role to the database
            IdentityResult roleResult;
            bool adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Select the user, and then add the admin role to the user
            var result = await userManager.CreateAsync(new ApplicationUser() { Email = email, UserName = "Admin" }, pass);
            var user = await userManager.FindByNameAsync("Admin");
            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                var userResult = await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
