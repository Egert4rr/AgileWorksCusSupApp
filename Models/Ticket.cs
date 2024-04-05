namespace CusSupApp.Models {
    
    public class Ticket {

        public int Id {get; set;}
        public required string Description {get; set;}
        public DateTime EntryTime {get; set;}
        public required DateTime Deadline {get; set;}
        public bool IsResolved {get; set;}
    }
}