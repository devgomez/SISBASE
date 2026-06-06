using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SISBase.Domain.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Lastname { get; set; }

        public string? Email { get; set; }

        /// <summary>
        /// 1 = Activo, 0 = Inactivo
        /// </summary>
        public bool Status { get; set; } = true;

        public int? RoleId { get; set; }

        // Propiedad de navegación (opcional)
        public Role? Role { get; set; }
    }
}
