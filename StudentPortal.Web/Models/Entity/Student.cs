namespace StudentPortal.Web.Models.Entity
{
    public class Student
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public bool IsSubscribed {  get; set; }


    }
}
