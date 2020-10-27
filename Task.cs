using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList
{
    public class Task
    {
        public string ID { get; set; }
        public string taskText { get; set; }
        public string Priority { get; set; }

        public Task()
        {

        }
        public Task (string taskText, string Priority)
        {
            this.ID = DateTime.Now.Ticks.ToString("x");
            this.taskText = taskText;
            this.Priority = Priority;
        }
        public Task(string ID, string taskText, string Priority)
        {
            this.ID = ID;
            this.taskText = taskText;
            this.Priority = Priority;
        }
    }
}