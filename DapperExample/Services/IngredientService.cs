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
    public class IngredientService : IIngredientService
    {
        private IDbConnection _db;

        public IngredientService()
        {
            this._db = new SqlConnection(ConfigurationManager.ConnectionStrings[AppConstants.ConnectionStringName].ConnectionString);
            this._db.Open();
            CreateTables();
        }

        private void CreateTables()
        {
             this._db.Execute(@"
                if (OBJECT_ID('Ingredient') is null)
                begin
                    CREATE TABLE [dbo].[Ingredient] (
                        [IngredientID]              INT IDENTITY(1,1) NOT NULL,
                        [CupcakeID]                 INT           NOT NULL,
                        [Data]                      NVARCHAR (40) NOT NULL,
                        CONSTRAINT [PK_Ingredient]  PRIMARY KEY ([IngredientID])
                    );
                end
                ");
        }

        public IEnumerable<Ingredient> GetIngredientsByCupcakeID(int cupcakeID)
        {
            List<Ingredient> ingredients = _db.Query<Ingredient>("SELECT * FROM Ingredient WHERE CupcakeID = @CupcakeID", new { cupcakeID }).ToList();

            return ingredients as IEnumerable<Ingredient>;
        }

        public bool InsertIngredient(Ingredient ingredient)
        {
            var sqlQuery = "INSERT INTO Ingredient (CupcakeID, Data) VALUES(@CupcakeID, @Data); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var ingredientId = this._db.Query<int>(sqlQuery, ingredient).Single();
            ingredient.IngredientID = ingredientId;
            return true;
        }

        public bool DeleteIngredient(int ingredientID)
        {
            string sqlQuery = "DELETE FROM Ingredient WHERE IngredientID = @IngredientID";
            this._db.Execute(sqlQuery, new { ingredientID });
            return true;
        }

        public bool UpdateIngredient(Ingredient ingredient)
        {
            var sqlQuery = "UPDATE Ingredient SET Name = @Name, Price = @Price, Created = @Created, LastModified = @LastModified WHERE IngredientID = @IngredientID";
            this._db.Execute(sqlQuery, ingredient);
            return true;
        }
    }

    internal interface IIngredientService
    {
        IEnumerable<Ingredient> GetIngredientsByCupcakeID(int cupcakeID);
        bool InsertIngredient(Ingredient ingredient);
        bool DeleteIngredient(int ingredientID);
        bool UpdateIngredient(Ingredient ingredient);
    }
}