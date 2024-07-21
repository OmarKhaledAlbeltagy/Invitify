﻿using Invitify.Entities;

namespace Invitify.Models
{
    public class EditContactModel
    {
        public int Id { get; set; }

        public string ContactName { get; set; }

        public bool? Gender { get; set; }

        public int StateId { get; set; }

        public int? PhoneCodeId { get; set; }

        public string? Address { get; set; }

        public string? MobileNumber { get; set; }

        public string? Email { get; set; }

        public int ContactTypeId { get; set; }

        public string? Notes { get; set; }
    }
}
