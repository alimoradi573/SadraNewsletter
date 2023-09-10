using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newsletter.Api.Auth
{
    public static class Policies
    {
        /// <summary>
        /// Adminstrative tasks
        /// </summary>
        public const string Admin = "admin";
        /// <summary>
        /// Authoring of workflow definitions and steps
        /// </summary>
        public const string Viewer = "viewer";
        /// <summary>
        /// Starting, stopping, suspending and resuming workflows
        /// </summary>
        public const string Controller = "controller";
        /// <summary>
        /// Querying the status of a workflow
        /// </summary>
        public const string Author = "author";
        /// <summary>
        /// Activity workers
        /// </summary>
        public const string Worker = "worker";
    }
}
