using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DapperExample.Models;
using DapperExample.Utility;
using Dapper;
using DapperExample.Resources;

namespace DapperExample.Services
{
    public class CupcakeService : ICupcakeService
    {
        private IDbConnection _db;

        public CupcakeService()
        {
            this._db = new SqlConnection(ConfigurationManager.ConnectionStrings[AppConstants.ConnectionStringName].ConnectionString);
            this._db.Open();
            CreateTables();
        }

        private void CreateTables()
        {
             this._db.Execute(@"
                if (OBJECT_ID('Cupcake') is null)
                begin
                    CREATE TABLE [dbo].[Cupcake] (
                        [CupcakeID]    INT IDENTITY(1,1) NOT NULL,
                        [Name]         NVARCHAR (40) NOT NULL,
                        [Price]        INT           NOT NULL,
                        [Created]      DATETIME      NOT NULL,
                        [LastModified] DATETIME      NOT NULL, 
                        CONSTRAINT [PK_Cupcake] PRIMARY KEY ([CupcakeID])
                    );
                end
                ");
        }

        public IEnumerable<Cupcake> GetCupcakes()
        {
            return this._db.Query<Cupcake>("SELECT * FROM Cupcake").ToList();
        }

        public Cupcake GetCupcakeByID(int id)
        {
            return this._db.Query<Cupcake>("SELECT * FROM Cupcake WHERE CupcakeID = @CupcakeID", new { id }).SingleOrDefault();
        }

        public bool InsertCupcake(Cupcake cupcake)
        {
            var sqlQuery = "INSERT INTO Cupcake (Name, Price, Created, LastModified) VALUES(@Name, @Price, @Created, @LastModified); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var cupcakeId = this._db.Query<int>(sqlQuery, cupcake).Single();
            cupcake.CupcakeID = cupcakeId;
            return true;
        }

        public bool DeleteCupcake(int CupcakeID)
        {
            string sqlQuery = "DELETE FROM Cupcake WHERE CupcakeId = @CupcakeID";
            this._db.Execute(sqlQuery, new { CupcakeID });
            return true;
        }

        public bool UpdateCupcake(Cupcake Cupcake)
        {
            var sqlQuery = "UPDATE Cupcake SET Name = @Name, Price = @Price, Created = @Created, LastModified = @LastModified WHERE CupcakeID = @CupcakeID";
            this._db.Execute(sqlQuery, Cupcake);
            return true;
        }
    }

    internal interface ICupcakeService
    {
        IEnumerable<Cupcake> GetCupcakes();
        Cupcake GetCupcakeByID(int cupcakeId);
        bool InsertCupcake(Cupcake cupcake);
        bool DeleteCupcake(int cupcakeID);
        bool UpdateCupcake(Cupcake cupcake);
    }
}