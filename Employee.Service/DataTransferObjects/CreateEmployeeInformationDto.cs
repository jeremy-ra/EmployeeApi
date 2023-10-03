using System.ComponentModel.DataAnnotations;
using System;

namespace Employee.Service.DataTransferObjects
{
    public class CreateEmployeeInformationDto : EmployeeInformationDto
    {
        [Required]
        public override string FirstName { get; set; }
        [Required]
        public override string LastName { get; set; }
        [Required]
        [Range(1, 40, ErrorMessage = "Enter between 1 to 40")]
        public override int Temperature { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public override DateTime RecordDate { get; set; }
    }
}
