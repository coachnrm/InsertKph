using System.ComponentModel.DataAnnotations;

namespace InsertKph.Models
{
    public class IpdProgressNote
    {
        public int progress_note_id {get; set;}
        public string an {get; set;}
        public DateOnly progress_note_date {get; set;} 
        public string progress_note_time {get; set;} 
        public string progress_note_owner_type {get; set;} 
        public string progress_note_doctor {get; set;}
        public DateTime progress_note_enter_datetime {get; set;}
        public string create_user {get; set;} 
        public DateTime create_datetime {get; set;} 
        public string update_user {get; set;} 
        public DateTime update_datetime {get; set;} 
        public string version {get; set;}
        public int? pre_order_progress_note_id {get; set;}
        public DateOnly pre_order_progress_note_date {get; set;}

        public string pre_order_progress_note_time {get; set;}
        public List<IpdProgressNoteItem> IpdProgressNoteItems {get; set;} = new List<IpdProgressNoteItem>();
    }

    // public class IpdProgressNote
    // {
    //     public int progress_note_id {get; set;}
    //     public string an {get; set;}
    //     public DateOnly progress_note_date {get; set;} = DateOnly.FromDateTime(DateTime.Now);
    //     public TimeOnly progress_note_time {get; set;} = new TimeOnly(23, 59, 59);
    //     public string progress_note_owner_type {get; set;}  = "doctor";
    //     public string progress_note_doctor {get; set;} = "2005";
    //     public DateTime progress_note_enter_datetime {get; set;} = DateTime.Now;
    //     public string create_user {get; set;} = "50239";
    //     public DateTime create_datetime {get; set;} = DateTime.Now;
    //     public string update_user {get; set;} = "50239";
    //     public DateTime update_datetime {get; set;} 
    //     public string version {get; set;} = "1";
    //     public int? pre_order_progress_note_id {get; set;}
    //     public DateOnly pre_order_progress_note_date {get; set;}

    //     public TimeOnly pre_order_progress_note_time {get; set;}
    //     public List<IpdProgressNoteItem> IpdProgressNoteItems {get; set;} = new List<IpdProgressNoteItem>();
    // }
}