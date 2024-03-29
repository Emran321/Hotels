﻿namespace Hotels.Models
{
    public class Review
    {
        public Guid ID { get; set; }
        public Guid RoomID { get; set; }
        public virtual Room Room { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set; }
        public string Description { get; set; }
    }
}
