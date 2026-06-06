using System;
using System.Collections.Generic;
using System.Text;

namespace SISBase.Domain.Entities
{
    public class Option
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public required string FormName { get; set; }
        public string? Group { get; set; }
        public string? ShortName { get; set; }
        public string? Icon { get; set; }
        public bool Status { get; set; }
    }
}
