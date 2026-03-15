using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareBusiness.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string DoctorName { get; set; }

        [Required]
        [StringLength(100)]
        public string Specialty { get; set; }

        [Required]
        [StringLength(20)]
        public string LicenseNumber { get; set; }

        public int MaxPatients { get; set; }

        public bool Active { get; set; }

        [InverseProperty(nameof(Appointment.Doctor))]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
