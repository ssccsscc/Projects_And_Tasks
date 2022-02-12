using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DataAccess.Entities
{
    /// <summary>
    /// Base class for the database record containing the primary key. Need to support reading of multiple record with template type in the DatabaseContext.cs
    /// </summary>
    public abstract class DataObject
    {
        [Key]
        public int Id { get; set; }
    }
}
