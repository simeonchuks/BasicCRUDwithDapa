using DapperDemo.Models;
using DapperDemo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = _companyRepository.GetAll();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var companies = _companyRepository.Find(id);
            return Ok(companies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] Company company)
        {
            var createCom = _companyRepository.Add(company);
            return Ok(createCom);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany([FromBody] Company company)
        {
            var updateCom = _companyRepository.Update(company);
            return Ok(updateCom);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            _companyRepository.Remove(id);
            return Ok();
        }
    }
}
