namespace Entities.Dtos.DoctorDto
{
    public struct DoctorAppointmentView
    {
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CRM { get; set; }
    }
}
