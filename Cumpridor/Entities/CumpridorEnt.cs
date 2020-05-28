using System;
using System.ComponentModel.DataAnnotations;

namespace Cumpridor.Entities
{
    public class CumpridorEnt
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
