using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMembershipsLab.Models
{
    /// <summary>
    /// Class ApplicationUser models a user.
    /// Inherits from the Identity class.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name.
        /// <value>The users firstname.</value>
        /// </summary>
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// <value>The users lastname.</value>
        /// </summary>
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the birthdate.
        /// <value>The users birthdate.</value>
        /// </summary>
        public DateTime Birthdate { get; set; }
    }
}
