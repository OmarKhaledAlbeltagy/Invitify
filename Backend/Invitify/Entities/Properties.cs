namespace Invitify.Entities
{
    public class Properties
    {
        public int Id { get; set; }

        public string Property { get; set; }

        public string? Value { get; set; }

        public bool IsSocialMedia { get; set; } = false;
    }
}
