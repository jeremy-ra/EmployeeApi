using Employee.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Service.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<EmployeeInformation>> GetEmployees();
        public Task<EmployeeInformation> Add(EmployeeInformation employeeInformation);
        public Task Update(EmployeeInformation employeeInformation);
        public Task<EmployeeInformation> GetEmployeeById(int id);
        public Task<IEnumerable<EmployeeInformation>> GetEmployeeByFirstName(string firstName);
        public Task<IEnumerable<EmployeeInformation>> GetEmployeeByLastName(string lastName);
        public Task<IEnumerable<EmployeeInformation>> GetEmployeeByTemperature(int temperatureStart, int temperatureEnd);
        public Task<IEnumerable<EmployeeInformation>> GetEmployeeByRecordDate(DateTime recordDateStart, DateTime recordDateEnd);
    }
}
