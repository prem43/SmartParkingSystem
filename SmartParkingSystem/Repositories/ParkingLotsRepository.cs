using Microsoft.AspNetCore.Connections;
using SmartParkingSystem.Models;
using SmartParkingSystem.Repositories.IRepositories;
using System.Data;
using Dapper;
using System.Linq;
using System;
using SmartParkingSystem.Infrastructure;
namespace SmartParkingSystem.Repositories
{
    public class ParkingLotsRepository: IParkingLotsRepository
    {

        private readonly IDatabaseHelper _databaseHelper;
        public ParkingLotsRepository(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }
        public List<ParkingLotsDTO> Getall()
        {
            using IDbConnection db = _databaseHelper.GetConnection;
            return db.Query<ParkingLotsDTO>(@"Select * from ParkingLots").ToList();
        }
    }
}
