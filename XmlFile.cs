using System;
using System.Data;
using System.Xml;

namespace ToDoList
{
    public class XmlFile
    {
        //-----------------------------------------------------------------
        //Creates a new task and saves it to the xml file
        public static void AddTask(string path, Task t)
        {
            XmlDocument xmlTask = new XmlDocument();
            xmlTask.Load(path);

            XmlElement ParentElement = xmlTask.CreateElement("Task");

            XmlElement ID = xmlTask.CreateElement("ID");
            ID.InnerText = t.ID;

            XmlElement Priority = xmlTask.CreateElement("Priority");
            Priority.InnerText = t.Priority;

            XmlElement taskText = xmlTask.CreateElement("taskText");
            taskText.InnerText = t.taskText;

            ParentElement.AppendChild(ID);
            ParentElement.AppendChild(Priority);
            ParentElement.AppendChild(taskText);

            xmlTask.DocumentElement.AppendChild(ParentElement);

            xmlTask.Save(path);
        }
        //--------------------------------------------------------------------
        //Delete a task, given the ID, and save to xml file
        public static void DeleteTask(string path, string taskId)
        {
            DataSet ds = new DataSet();

            ds.ReadXml(path);
            DataTable dt = ds.Tables[0];
            if(dt.Rows.Count > 1) //if not the last task
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["ID"].ToString() == taskId)
                    {
                        dr.Delete();
                    }
                }
                ds.Tables[0].AcceptChanges();

                ds.WriteXml(path);
            }
           else //if trying to delete the last task
            {
                Task lastTask = new Task(taskId, "Sorry, but you must have at least one task.", "Whenever");
                UpdateTask(path, lastTask);
            }
        }
        //--------------------------------------------------------------------
        //Delete a task, given the ID, and save to xml file
        public static void UpdateTask(string path, Task t)
        {
            DataSet ds = new DataSet();

            ds.ReadXml(path);

            DataTable dt = ds.Tables[0];

            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["ID"].ToString() == t.ID)
                {
                    dr["taskText"]=t.taskText;
                    dr["Priority"] = t.Priority;
                }
            }
            ds.Tables[0].AcceptChanges();

            ds.WriteXml(path);

        }
     }
}