using AutoMapper;
using CoFloPeopleAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CoFloPeopleAPI.Services
{
    public class PeopleManagement : IPeopleManagement
    {
        private readonly ILogger<PeopleManagement> _logger;
        private readonly CoFloPeopleAPIContext _context;
        private readonly IMapper _mapper;


        public PeopleManagement(ILogger<PeopleManagement> logger, CoFloPeopleAPIContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        // Create Person
        public async Task<PersonModel> CreatePersonAsync(PersonModel person)
        {
            var personDb = ConvertToDatabaseModel(person);
            await _context.AddAsync(personDb);
            await _context.SaveChangesAsync();
            return ConvertFromDatabaseModel(personDb);
        }

        // Delete Person
        public async Task DeletePersonAsync(int id)
        {
            var personModeldB = await _context.PersonModel.FindAsync(id);
            if (personModeldB != null)
            {
                _context.PersonModel.Remove(personModeldB);
                await _context.SaveChangesAsync();
            } else
            {
                throw new Exception($"Person with ID : {id} not found");
            }

        }

        // Get List of Persons (People)
        public async Task<IEnumerable<PersonModel>> GetListOfPersonAsync()
        {
            var personList = _context.PersonList;  // Use IQueryable instead of IEnumerable as it is more effecient for dbs
            return personList.Select(personDB => ConvertFromDatabaseModel(personDB)).ToList();
        }

        // Get Person By Id
        public async Task<PersonModel> GetPersonById(int id)
        {
            var personModelDB = await _context.PersonModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personModelDB == null)
            {
                throw new Exception($"Person with ID : {id} not found");
            }
            return ConvertFromDatabaseModel(personModelDB);
        }

        // Update Person Detail
        public async Task<PersonModel> UpdatePersonAsync(PersonModel updatedPerson)
        {
            var updatedPersonDb = ConvertToDatabaseModel(updatedPerson);
            try
            {
                var currentDbPerson = await _context.PersonModel.FindAsync(updatedPersonDb.Id);
                if (currentDbPerson == null)
                {
                    throw new Exception($"Person with ID : {updatedPersonDb.Id} not found");
                }

                currentDbPerson.FirstName = updatedPersonDb.FirstName;
                currentDbPerson.LastName = updatedPersonDb.LastName;
                currentDbPerson.BirthDate = updatedPersonDb.BirthDate;
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbe)
            {
                if (!PersonModelExists(updatedPerson.Id))
                {
                    throw new Exception($"Person with ID : {updatedPersonDb.Id} not found");
                }
                else
                {
                    _logger.LogError($"Exception in UpdatePersonAsync : {dbe.Message}");
                    throw;
                }
            }
            
            return ConvertFromDatabaseModel(updatedPersonDb);

        }

        private bool PersonModelExists(int id)
        {
            return _context.PersonModel.Any(e => e.Id == id);
        }

        private PersonModelDB ConvertToDatabaseModel(PersonModel person)
        {
            return _mapper.Map<PersonModelDB>(person);
        }

        private PersonModel ConvertFromDatabaseModel(PersonModelDB personDb)
        {
            return _mapper.Map<PersonModel>(personDb);
        }
    }
}
