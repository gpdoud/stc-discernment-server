using System;
namespace stc_discernment_server.Models {

    public class Parishioner {

        public int Id { get; set; } = 0;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string? Email { get; set; } = null;
        public string? Cellphone { get; set; } = null;
        public string? Homephone { get; set; } = null;
        public string? Ministry { get; set; } = null;
        public int Year { get; set; } = DateTime.Now.Year;
        public bool Reviewed { get; set; } = false;
        public string Status { get; set; } = string.Empty; 
        public string? SubmittedBy { get; set; } = null;

        public bool IsCaller { get; set; } = true;
        public int? CallerId { get; set; } = null;
        public virtual Parishioner? Caller { get; set; } = null;

        public bool Active { get; set; } = true;
        public DateTime? Created { get; set; } = null;
        public DateTime? Updated { get; set; } = null;
    }
}

