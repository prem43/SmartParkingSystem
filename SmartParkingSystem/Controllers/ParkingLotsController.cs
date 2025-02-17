using Dapper;
using Microsoft.AspNetCore.Mvc;
using SmartParkingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using SmartParkingSystem.Services.IServices;

namespace SmartParkingSystem.Controllers
{
    public class ParkingLotsController : Controller
    {
        //private readonly Models.ApplicationDbContext _context;
        //private readonly string _connectionString ;
        private readonly IParkingLotsService _parkingLotsService;

        public ParkingLotsController(IParkingLotsService parkingLotsService)
        {
            _parkingLotsService = parkingLotsService;
            
        }

        //public async Task<IActionResult> Index()
        //{
        //    var alldata = "Select * from ParkingLots";
        //    var mapdata = List<ParkingLotsDTO>(alldata);
        //    return View(alldata);
        //}
        public IActionResult SlotView()
        
        {
            var alldata = _parkingLotsService.Getall();
            return View(alldata);
        }
        //[HttpGet]
        //public IActionResult Create()
        //{
            
        //    return View(); // If error, reload form
        //}
        //[HttpPost]
        //public IActionResult Create(ParkingLotsDTO model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int result = InsertParkingLot(model); // Insert into DB
        //        if (result > 0)
        //        {
        //            return RedirectToAction("SlotView"); // Redirect after success
        //        }
        //    }
        //    return View(model); // If error, reload form
        //}

        //public int InsertParkingLot(ParkingLotsDTO parkingLot)
        //{
        //    string sql = @"INSERT INTO ParkingLots (Name, Location, TotalSlots, AvailableSlots, Price, CreatedAt, UpdatedAt) 
        //           VALUES (@Name, @Location, @TotalSlots, @AvailableSlots, @Price, @CreatedAt, @UpdatedAt)";

        //    return _dbHelper.Execute(sql, parkingLot); // Directly calling Execute() from _dbHelper
        //}


    }
}

