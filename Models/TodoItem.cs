﻿namespace To_Do_Api.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
        public string UserId { get; set; }
    }
}
