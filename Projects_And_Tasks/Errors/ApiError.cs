using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Projects_And_Tasks
{
    //Class of the error that is send to a user
    /// <summary>
    /// Api error details
    /// </summary>
    public class ApiError
    {    /// <summary>
         /// Problem description
         /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Status (404 or 500)
        /// </summary>
        public int status { get; set; }
    }
}
