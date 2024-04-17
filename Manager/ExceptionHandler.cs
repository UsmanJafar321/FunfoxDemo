using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;

namespace Manager
{
    public static class ExceptionHandler
    {
        private static readonly Dictionary<string, string> ConstraintMessages = new Dictionary<string, string>();

        static ExceptionHandler()
        {
            ConstraintMessages.Add("uq_AccountCode", "Please enter unique account code.");
            ConstraintMessages.Add("uq_AccountTitle", "Please enter unique account title.");
            ConstraintMessages.Add("uq_LocationCode", "Please enter unique location code.");
            ConstraintMessages.Add("uq_GuardCode", "Please enter unique guard code.");
        }
        public static void HandleExecption(Exception ex)
        {
            var sqlException = ex.GetBaseException() as SqlException;
            if (sqlException != null)
            {
                throw GetDbException(sqlException);
            }
            var dbEntityException = ex as DbEntityValidationException;
            if (dbEntityException != null)
            {
                throw GetDbEntityValidationException(dbEntityException);
            }
            throw new Exception("Server error.");
        }
        private static Exception GetDbException(SqlException ex)
        {
            string errorMessage;
            switch (ex.Number)
            {
                case 2627:
                {
                    if (!TryGetCustomMessageForSqlException(ex, out errorMessage))
                    {
                        errorMessage = "Record already exists.";
                    }
                    return new Exception(errorMessage, ex);   
                }
                case 2601:
                    return new Exception("Item with same key already exist.");
                case 4060:
                    return new Exception("Db doesn't exist.");
                case 18452:
                case 18456:
                    return new Exception("Db Login failed.");
                case 547:
                    return new Exception("Operation failed due to reference of record in other entires.");
                case 8152:
                    return new Exception("Validation failed for one or more properties.");
                default:
                    return new Exception("Server error.");
            }
        }
        private static Exception GetDbEntityValidationException(DbEntityValidationException ex)
        {
            var errors = new List<string>();
            foreach (var eve in ex.EntityValidationErrors)
            {
                //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //    eve.Entry.Entity.GetType().Name, eve.Entry.State);                
                foreach (var ve in eve.ValidationErrors)
                {
                    //errors.Add(string.Format("Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    errors.Add(string.Format(ve.ErrorMessage));
                }
            }
            return new Exception(string.Join(",", errors));
        }

        private static bool TryGetCustomMessageForSqlException(Exception ex, out string message)
        {
            foreach (var constraintMessage in ConstraintMessages)
            {
                if (ex.Message.Contains(constraintMessage.Key))
                {
                    message= constraintMessage.Value;
                    return true;
                }
            }
            message = string.Empty;
            return false;
        }
    }
}
