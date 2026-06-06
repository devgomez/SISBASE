using System;
using System.Collections.Generic;
using System.Text;

namespace SISBase.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }

        public string TaxId { get; set; } = string.Empty; // RUC

        public string LegalName { get; set; } = string.Empty; // Razón Social

        public string? TradeName { get; set; } // Nombre Comercial

        public string? Address { get; set; }

        public string? UbigeoCode { get; set; }

        public string? District { get; set; }

        public string? Province { get; set; }

        public string? Department { get; set; } // Región/Departamento

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? DigitalCertificate { get; set; }

        public string? SunatUsername { get; set; } // Usuario SOL

        public string? SunatPassword { get; set; } // Clave SOL

        public DateTime CreatedAt { get; set; }
    }
}
