using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App_horarios_BackEnd.Models
{
    public class ComissaoCurso
    {
       
        public int IdUtilizador { get; set; }
        public Secretariado Secretariado { get; set; }
        
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        
        public int EscolaId { get; set; }
        public Escola Escola { get; set; }
    }
}