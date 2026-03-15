using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareBusiness.Models
{
    public partial class Appointment
    {
        [Key]
        public int ID { get; set; }

        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime BookingDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime AppointmentDate { get; set; }

        public bool IsCancelled { get; set; }

        [ForeignKey(nameof(DoctorID))]
        [InverseProperty("Appointments")]
        public virtual Doctor Doctor { get; set; }

        [ForeignKey(nameof(PatientID))]
        [InverseProperty("Appointments")]
        public virtual User Patient { get; set; }
    }
}
