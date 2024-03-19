using System.ComponentModel.DataAnnotations;

namespace APIPRUEBAS.Models
{
    public partial class Employee
    {
        public int? Id { get; set; }
        public int identification { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
        public int position { get; set; }
    }
}       
