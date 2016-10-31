using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FilterStudy
{
    public class ResponseHelper
    {
        public static void Write(string text, HttpResponse response)
        {
            var v = $"<div><h2>[{DateTime.Now.ToStd()}] {text}</h2><div>";
            var buff = Encoding.UTF8.GetBytes(v);
            response.Body.Write(buff,0,buff.Length);
        }
    }
}
