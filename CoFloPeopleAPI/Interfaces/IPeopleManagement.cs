namespace CoFloPeopleAPI.Interfaces
{
    public interface IPeopleManagement
    {
        Task<IEnumerable<PersonModel>> GetListOfPersonAsync();
        Task<PersonModel> GetPersonById(int Id);
        Task DeletePersonAsync(int Id);
        Task<PersonModel> CreatePersonAsync(PersonModel person);
        Task<PersonModel> UpdatePersonAsync(PersonModel updatedPerson);
    }
}
