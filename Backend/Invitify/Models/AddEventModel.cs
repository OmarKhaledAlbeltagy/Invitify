using Invitify.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Invitify.Models
{
    public class AddEventModel
    {
        public int? Id { get; set; }

        public string EventName { get; set; }

        public int StateId { get; set; }

        public string Address { get; set; }

        public int? Participants { get; set; }

        public int? Speakers { get; set; }

        public string? IframeLocation { get; set; }

        public string? About { get; set; }

        public string Domain { get; set; }

        public bool AllowAnonymous { get; set; }
    }
}
