namespace CoFloPeopleAPI.Entities
{
    public class PersonModelDB
    {
        public int Id { get; set; }  // EFCore automatically assigns and increments
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate => DateTime.Now; // public get only, private set
    }
}
