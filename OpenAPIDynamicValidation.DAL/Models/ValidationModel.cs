namespace OpenAPIDynamicValidation.DAL.Models
{
    public class ValidationModel
    {
        public string RequestedID { get; set; }  // varchar 100, not null
        public DateTime RequestedOn { get; set; } // DateTime, not null
        public string SecurityCode { get; set; } // varchar 6, nullable
        public int? ExtraRowCount { get; set; }   // int, nullable
    }
}
