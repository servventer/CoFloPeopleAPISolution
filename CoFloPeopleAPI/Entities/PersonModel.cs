namespace CoFloPeopleAPI.Entities
{
    public class PersonModel
    {
        public int Id { get; set; }  // EFCore automatically assigns and increments
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate => DateTime.Now; // public get only, private set
        public int Age => setAge(BirthDate); // public get only, private set

        public int setAge(DateTime birthDate)
        {
            var today = DateTime.Now;
            int iAge = today.Year - birthDate.Year;
            if (today < birthDate.AddYears(iAge))
            {
                iAge--;
            }
            return iAge;
        }
    }
}
