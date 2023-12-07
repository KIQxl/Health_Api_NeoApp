namespace Entities.Models
{
    public class Patient
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public PersonStatus PatientStatus { get; set; }
        public virtual List<Appointment> Appointments { get; set; }
    }
}
