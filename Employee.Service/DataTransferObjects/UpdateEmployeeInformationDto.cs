using System;
using System.ComponentModel.DataAnnotations;

namespace Employee.Service.DataTransferObjects
{
    public class UpdateEmployeeInformationDto : EmployeeInformationDto, IEmployeeInformation
    {
        [Required]
        public int EmployeeNumber { get; set; }
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
