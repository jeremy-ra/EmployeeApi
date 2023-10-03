using System;

namespace Employee.Service.DataTransferObjects
{
    public abstract class EmployeeInformationDto
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }       
        public virtual int Temperature { get; set; }        
        public virtual DateTime RecordDate { get; set; }
    }
}
