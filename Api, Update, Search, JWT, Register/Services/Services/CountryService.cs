using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using Services.DTOs.Country;
using Services.DTOs.Employee;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepo;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CountryService(ICountryRepository countryRepo, IMapper mapper,AppDbContext context)
        {
            _countryRepo = countryRepo;
            _mapper = mapper;
            _context = context;
        }

        public async Task CreateAsync(CountryCreateDto country) => await _countryRepo.CreateAsync(_mapper.Map<Country>(country));


        public async Task<CountryDto> GetByIdAsync(int? id) => _mapper.Map<CountryDto>(await _countryRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _countryRepo.DeleteAsync(await _countryRepo.GetByIdAsync(id));

   

        public async Task SoftDeleteAsync(int id)
        {
            Country country = await _countryRepo.GetByIdAsync(id);
            await _countryRepo.SoftDeleteAsync(country);
        }


        public async Task UpdateAsync(int? id, CountryUpdateDto country)
        {
            if (id == null) throw new ArgumentNullException();

            var data = await _countryRepo.GetByIdAsync(id);
            if (data == null) throw new NullReferenceException();

            _mapper.Map(country, data);
            await _countryRepo.UpdateAsync(data);
        }
    }
}
