using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newsletter.Api.Auth
{
    public static class Permissions
    {
        public const string Admin = "newsletter:admin";
        public const string Viewer = "newsletter:viewer";
        public const string Controller = "newsletter:controller";
        public const string Author = "newsletter:author";
        public const string Worker = "newsletter:worker";
    }
}
