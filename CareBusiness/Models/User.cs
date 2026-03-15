using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareBusiness.Models
{
    public partial class User
    {
        public User()
        {
            Appointments = new HashSet<Appointment>();
            Sessions = new HashSet<Session>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        [InverseProperty(nameof(Appointment.Patient))]
        public virtual ICollection<Appointment> Appointments { get; set; }

        [InverseProperty(nameof(Session.User))]
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
