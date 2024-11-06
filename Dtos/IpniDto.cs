namespace InsertKph.Dtos 
{
    public class IpniDto 
    {
        public int progress_note_item_id {get; set;}
        //public int progress_note_id {get; set;} // FK
        //public string an {get; set;} // FK
        public  string progress_note_item_type {get; set;}
        public string progress_note_item_detail {get; set;}
        public string progress_note_item_detail2 {get; set;}

        //public string create_user {get; set;} // ซ้ำ
        //public string create_datetime {get; set;} // ซ้ำ
        //public string update_user {get; set;} // ซ้ำ
        //public string update_datetime {get; set;} //ซ้ำ

    }
}