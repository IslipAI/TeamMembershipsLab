using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMembershipsLab.Models
{
    /// <summary>
    /// AppSecret class.
    /// </summary>
    public class AppSecrets
    {
        /// <summary>
        /// Gets or sets Adminpass.
        /// <value>Admin's password.</value>
        /// </summary>
        public string AdminPass { get; set; }

        /// <summary>
        /// Gets or sets MemberPass.
        /// <value>Member's password.</value>
        /// </summary>
        public string MemberPass { get; set; }
    }
}
