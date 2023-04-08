using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
  public static class ExceptionExtensions
  {
    public static string GetMessage(this Exception exception) {
      var message = "";
      if (exception is System.Data.Entity.Validation.DbEntityValidationException)
      {
        var e = exception as System.Data.Entity.Validation.DbEntityValidationException;
        message = string.Join(",", e.EntityValidationErrors.Select(x => x.ValidationErrors.FirstOrDefault()?.PropertyName + ":" + x.ValidationErrors.FirstOrDefault()?.ErrorMessage));
      }
      else if (exception is System.Data.Entity.Infrastructure.DbUpdateException)
      {
        var e = exception as System.Data.Entity.Infrastructure.DbUpdateException;
        if (e.InnerException != null
            && e.InnerException.InnerException != null)
        {
          if (e.InnerException.InnerException is System.Data.SqlClient.SqlException)
          {
            var sqlexception = e.InnerException.InnerException as System.Data.SqlClient.SqlException;
            switch (sqlexception.Number)
            {
              case 2627:  // Unique constraint error
                message = sqlexception.Message;
                break;
              case 547:   // Constraint check violation
                message = sqlexception.Message;
                break;
              case 2601:  // Duplicated key row error
                var regex = @"\ACannot insert duplicate key row in object \'(?<TableName>.+?)\' with unique index \'(?<IndexName>.+?)\'\. The duplicate key value is \((?<KeyValues>.+?)\)";
                var match = new System.Text.RegularExpressions.Regex(regex, System.Text.RegularExpressions.RegexOptions.Compiled).Match(sqlexception.Message);
                var tablename = match?.Groups["TableName"].Value;
                var indexname = match?.Groups["IndexName"].Value;
                var keyvalue = match?.Groups["KeyValues"].Value;
                message = $"[{keyvalue}] 已经存在,不允许重复.索引:{indexname},表:{tablename}";
                break;
              default:
                message = e.InnerException.InnerException.Message;
                break;
            }
          }
          else
          {
            message = e.GetBaseException().Message;
          }
        }
        else
        {
          message = e.GetBaseException().Message;
        }
      }
      else
      {
        message = exception.GetBaseException().Message;
      }
      return message;

      }

  }
}