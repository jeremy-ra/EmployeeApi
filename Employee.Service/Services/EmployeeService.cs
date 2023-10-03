using Employee.Entity.Entities;
using Employee.Infrastructure.Repositories;
using Employee.Service.DataTransferObjects;
using Employee.Service.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Service.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IRepository<EmployeeInformation> _employeeInformationRepository;
        public EmployeeService(IUnitOfWork unitOfWork, IRepository<EmployeeInformation> employeeInformationRepository) 
            : base(unitOfWork)
        {
            _employeeInformationRepository = employeeInformationRepository;
        }

        /// <summary>
        /// Add new employee information.
        /// </summary>
        /// <param name="employeeInformation"></param>
        /// <returns></returns>
        public async Task<EmployeeInformation> Add(EmployeeInformation employeeInformation)
        {           
            _employeeInformationRepository.Add(employeeInformation);
            
            await _unitOfWork.SaveChangesAsync();

            return employeeInformation;
        }

        /// <summary>
        /// Gets employee information by firstName.
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeeInformation>> GetEmployeeByFirstName(string firstName)
        {
            var employees = await _employeeInformationRepository.GetAll();

            return employees.Where(e => e.FirstName.ToLower() == firstName.ToLower());
        }

        /// <summary>
        /// Gets employee information by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EmployeeInformation> GetEmployeeById(int id)
        {
            return await _employeeInformationRepository.GetById(id);
        }

        /// <summary>
        /// Gets employee information by lastName.
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeeInformation>> GetEmployeeByLastName(string lastName)
        {
            var employees = await _employeeInformationRepository.GetAll();

            return employees.Where(e => e.LastName.ToLower() == lastName.ToLower());
        }

        /// <summary>
        /// Gets employee information by record date range.
        /// </summary>
        /// <param name="recordDateStart"></param>
        /// <param name="recordDateEnd"></param>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeeInformation>> GetEmployeeByRecordDate(DateTime recordDateStart, DateTime recordDateEnd)
        {
            var employees = await _employeeInformationRepository.GetAll();

            return employees.Where(e => e.RecordDate.Date >= recordDateStart.Date && e.RecordDate.Date <= recordDateEnd.Date);
        }

        /// <summary>
        /// Gets employee information by temperature range.
        /// </summary>
        /// <param name="temperatureStart"></param>
        /// <param name="temperatureEnd"></param>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeeInformation>> GetEmployeeByTemperature(int temperatureStart, int temperatureEnd)
        {
            var employees = await _employeeInformationRepository.GetAll();

            return employees.Where(e => e.Temperature >= temperatureStart && e.Temperature <= temperatureEnd);
        }

        /// <summary>
        /// Gets list of employees
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeeInformation>> GetEmployees()
        {
            return await _employeeInformationRepository.GetAll();
        }
                
        /// <summary>
        /// Updates employee information.
        /// </summary>
        /// <param name="employeeInformation"></param>
        /// <returns></returns>
        public Task Update(EmployeeInformation employeeInformation)
        {
            var employeeInformationData = _employeeInformationRepository.GetById(employeeInformation.EmployeeNumber);

            EmployeeInformation employeeInformationUpdate = employeeInformationData.Result;
            employeeInformationUpdate.FirstName = employeeInformation.FirstName;
            employeeInformationUpdate.LastName = employeeInformation.LastName;
            employeeInformationUpdate.Temperature = employeeInformation.Temperature;
            employeeInformationUpdate.RecordDate = employeeInformation.RecordDate;
            employeeInformation.UpdatedDate = DateTime.Now;

            _employeeInformationRepository.Update(employeeInformationUpdate);
            
            return _unitOfWork.SaveChangesAsync();
        }        
    }
}
