using Microsoft.AspNetCore.Mvc;
using SmartParkingSystem.Models;
using SmartParkingSystem.Repositories;
using System.Threading.Tasks;

namespace SmartParkingSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly Repository<ApplicationUser> _userRepository;

        public UserController(Repository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.AddAsync(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
