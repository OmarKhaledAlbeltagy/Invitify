namespace Invitify.Models
{
    public class ContinueEventCreationModel
    {
        public int Id { get; set; }

        public string EventName { get; set; }

        public int LastStep { get; set; }

        public string CreationDateTime { get; set; }
    }
}
