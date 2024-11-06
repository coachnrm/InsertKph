namespace InsertKph.Dtos 
{
    public class IpnDto 
    {
        public int progress_note_id {get; set;} // ซ้ำ
        public string an {get; set;} // ซ้ำ
        public DateOnly progress_note_date {get; set;} = DateOnly.FromDateTime(DateTime.Now);
        public string progress_note_time {get; set;} 
        public string progress_note_owner_type {get; set;}  = "doctor";
        public string progress_note_doctor {get; set;} = "2005";
        public DateTime progress_note_enter_datetime {get; set;} = DateTime.Now;
        public string create_user {get; set;} = "50239"; // ซ้ำ
        public DateTime create_datetime {get; set;} = DateTime.Now; // ซ้ำ
        public string update_user {get; set;} = "50239"; // ซ้ำ
        public DateTime update_datetime {get; set;} // ซ้ำ
        public string version {get; set;} = "1";
        public int pre_order_progress_note_id {get; set;}
        public DateOnly pre_order_progress_note_date {get; set;}

        public string pre_order_progress_note_time {get; set;}
        public List<IpniDto> Ipnis {get; set;} 
    }
}