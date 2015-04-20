using Storage.DAL;
using Storage.Models;
using Storage.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Storage.Services
{
    public class SharedService
    {
        public static T CopyEntity<T>(T copyTo, T copyFrom)
        {
            foreach (var field in typeof(T).GetProperties())
            {
                var isKey = field.GetCustomAttributes(false).Any(attribute => attribute.GetType() == typeof(KeyAttribute));
                var value = field.GetValue(copyFrom);
                if (!isKey && value != null)
                {
                    if (!(value is BaseModel))
                    {
                        field.SetValue(copyTo, value, null);
                    }
                }
            }
            return copyTo;
        }
    }
}