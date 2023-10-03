
namespace Employee.Service.DataTransferObjects
{
    public class GetEmployeeInformationDto : EmployeeInformationDto, IEmployeeInformation
    {
        public int EmployeeNumber { get; set; }        
    }
}
