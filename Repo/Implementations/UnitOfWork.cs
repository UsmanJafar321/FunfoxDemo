using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Entity;
using Repo.Int;
using Manager;

namespace Repo.Imp
{
    public class UnitOfWork : IDisposable
    {
        private readonly FunfoxEntities _context = new FunfoxEntities();
        public GenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }


        //Procedures

        //private IGenericRepository<ProductListByBestSeller_Result> _productListByBestSellerRepository;
        //public IGenericRepository<ProductListByBestSeller_Result> ProductListByBestSellerRepository
        //{
        //    get { return _productListByBestSellerRepository ?? (_productListByBestSellerRepository = new GenericRepository<ProductListByBestSeller_Result>(_context)); }
        //}


        //Tables Transactions

        //private IGenericRepository<ProductListBySale_Result> _productListBySaleRepository;
        //public IGenericRepository<ProductListBySale_Result> ProductListBySaleRepository
        //{
        //    get { return _productListBySaleRepository ?? (_productListBySaleRepository = new GenericRepository<ProductListBySale_Result>(_context)); }
        //}

        
        //methods
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleExecption(ex);
            }
        }
        public virtual DataTable GetDataTable(string query)
        {
            var da = new SqlDataAdapter();
            var dt = new DataTable();
            var cmd = new SqlCommand
            {
                Connection =
                    new SqlConnection(_context.Database.Connection.ConnectionString.ToString(CultureInfo.InvariantCulture)),
                CommandTimeout = 120,
                CommandType = CommandType.Text,
                CommandText = query
            };
            da.SelectCommand = cmd;
            da.Fill(dt);
            return dt;
        }
        public virtual DataTable ExecSp(string spName, Dictionary<string, string> parameters)
        {
            var da = new SqlDataAdapter();
            var dt = new DataTable();
            var cmd = new SqlCommand
            {
                Connection =
                    new SqlConnection(_context.Database.Connection.ConnectionString.ToString(CultureInfo.InvariantCulture)),
                CommandTimeout = 120,
                CommandType = CommandType.StoredProcedure,
                CommandText = spName
            };
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
            da.SelectCommand = cmd;
            da.Fill(dt);
            return dt;
        }
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
