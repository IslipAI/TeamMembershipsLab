using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMembershipsLab.Models
{
    /// <summary>
    /// Class Team models a team.
    /// </summary>
    public class Team
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// <value>The teams identifier.</value>
        /// </summary>
        [Key]
        public int Id { get; set; }


        /// <summary>
        /// Gets or sets the team name.
        /// <value>The teams team name.</value>
        /// </summary>
        [Required]
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }


        /// <summary>
        /// Gets or sets the email.
        /// <value>The teams email.</value>
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        /// <summary>
        /// Gets or sets the established date.
        /// <value>The teams established date.</value>
        /// </summary>
        [Display(Name = "Established Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EstablishedDate { get; set; }
        



    }
}