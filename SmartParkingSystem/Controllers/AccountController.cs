﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartParkingSystem.Models;
using SmartParkingSystem.Services.IServices;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IAuthenticationService _authenticationService;
    public AccountController(IAuthenticationService authenticationService,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _authenticationService =authenticationService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // Login GET
    public IActionResult Login() => View();

    // Login POST
    //[HttpPost]
    //public async Task<IActionResult> Login(LoginViewModel model)
    //{
    //    if (!ModelState.IsValid)
    //        return View(model);

    //    // Find user by email
    //    var user = await _userManager.FindByEmailAsync(model.Email);
    //    if (user != null)
    //    {
    //        // Validate password
    //        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
    //        if (result.Succeeded)
    //        {
    //            return RedirectToAction("Dashboard", "Home");
    //        }
    //    }

    //    // Add error if login fails
    //    ModelState.AddModelError("", "Invalid login attempt");
    //    return View(model);
    //}
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        if (result.Succeeded)
            return RedirectToAction("Dashboard", "Home");

        ModelState.AddModelError("", "Invalid login attempt");
        return View(model);
    }

    // Register GET
    [HttpGet]
    public IActionResult Register() => View();

    // Register POST
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        model.Role = "Admin";
        //if (!ModelState.IsValid)
        //    return View(model);

        // Create a new user
        //var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FirstName + model.LastName, Role = model.Role };
        //var result = await _userManager.CreateAsync(user, model.Password);


        if (ModelState.IsValid)
        {
            // Add user to role
           var data =_authenticationService.register(model);
            return RedirectToAction("Login");
        }

        // Return errors if registration fails
        //foreach (var error in result.Errors)
        //{
        //    ModelState.AddModelError("", error.Description);
        //}
        return View(model);
    }

    // Logout GET
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}
