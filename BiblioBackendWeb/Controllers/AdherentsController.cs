using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiblioBackendWeb.Models;
using BiblioBackendWeb.Repository.Implementations;
using Microsoft.AspNetCore.Authorization;

namespace BiblioBackendWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AdherentsController : ControllerBase
    {
        private readonly BibliothequeDbContext _context;

        public AdherentsController(BibliothequeDbContext context)
        {
            _context = context;
        }

        // GET: api/Adherents
        [HttpGet]
        public  IEnumerable<Adherent> GetAdherents()
        {
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                return uow.Adherent.GetAll();
            }
        }

        // GET: api/Adherents/5
        [HttpGet("{id}")]
        public  Adherent GetAdherent(int id)
        {
             using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                return uow.Adherent.Get(id);
            }
             
        }

        // PUT: api/Adherents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutAdherent(int id, string nomAdherent, string prenomAdherent, string Email)
        {
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                Adherent adherent = uow.Adherent.Get(id);
                adherent.Email = Email;
                adherent.PrenomAdherent = prenomAdherent;
                adherent.NomAdherent = nomAdherent;
              

                uow.Complete();

            }
        }

        // POST: api/Adherents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public  void PostAdherent(string nomAdherent,string prenomAdherent,string Email)
        {
            Adherent adherent = new Adherent()
            {
                NomAdherent = nomAdherent,
                PrenomAdherent = prenomAdherent,
                Email = Email,
                DateInscription = DateTime.Now
        };
           

            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                 
                 uow.Adherent.Add(adherent);
                 uow.Complete();
            } 


        }
        [HttpPost("Login")] 
        public IActionResult LoginAdherent(string Email, string Password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (UnitOfWork uow = new UnitOfWork(new BibliothequeDbContext()))
                    {
                        Console.WriteLine($"email: {Email}");
                        Console.WriteLine($"Password: {Password}");

                        if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                        {
                            Adherent adherent = uow.Adherent.CurrentAdherent(Email, Password);

                            if (adherent != null)
                            {
                                // Authentication successful, you can proceed with further actions
                                return Ok(new { Message = "Login successful", data = adherent, success = true });
                            }
                            else
                            {
                                // Invalid credentials
                                return BadRequest(new { Message = "Invalid credentials", success = false });
                            }
                        }
                        else
                        {
                            // Email and Password are required
                            return BadRequest(new { Message = "Email and Password are required", success = false });
                        }
                    }
                }
                else
                {
                    // Invalid model state
                    return BadRequest(new { Message = "Invalid model state", success = false, Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Consider using a logging library like Serilog or log4net
                Console.WriteLine($"Exception: {ex.Message}"); 
                 
                // Don't expose detailed exception information in a production environment
                return StatusCode(500, new { Message = "An error occurred while processing the request", success = false });
            }
        }

        // DELETE: api/Adherents/5
        [HttpDelete("{id}")]
        public void DeleteAdherent(int id)
        {
            using (UnitOfWork uow = new(new BibliothequeDbContext()))
            {
                Adherent adherent =  uow.Adherent.Get(id);
                uow.Adherent.Remove(adherent);
                uow.Complete();

            }
        }

       
    }
}
