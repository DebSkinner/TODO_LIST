using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ToDoList
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_gvTasks();
            }

        }
        //------------------------------------------------
        protected void Bind_gvTasks()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("~/Tasks.xml"));
            gvTasks.DataSource = ds;
            gvTasks.DataBind();
            Session["gvTasks"] = ds.Tables[0];

        }
        //------------------------------------------------------
        //display modal to add task (Triggered by Add New Task button)
        protected void AddNewTask_Click(object sender, EventArgs e)
        {
            UpsertTaskPanel.Visible = true;

        }
        //---------------------------------------------------------
        //Save the new task or update an existing task in the xml file (Triggered by Save button in modal)
        protected void UpsertTask_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/Tasks.xml");
            string id = upsertTaskID.Text;

            if (id == "")
            {
                Task t = new Task(Scrub(tbEditTask.Text), lbPriority.Text);
                XmlFile.AddTask(path, t);
            }
            else
            {
                Task t = new Task(id, Scrub(tbEditTask.Text), lbPriority.Text);
                XmlFile.UpdateTask(path, t);
            }

            Bind_gvTasks();

            UpsertTaskPanel.Visible = false;
        }
        //-------------------------------------------------------
        //clean up the text to prevent XSS and SQL injection
        protected string Scrub(string taskText)
        {
            string scrubbedText = HttpUtility.HtmlEncode(taskText).Replace("/", "&#x2F;");
            return scrubbedText;
        }
        //---------------------------------------------------------
        //delete or update a task (Triggered by Edit and Delete buttons)
        protected void FireRowCommand(object sender, GridViewCommandEventArgs e)

        {
            string command = e.CommandName;

            //string autoId = e.CommandArgument.ToString();

            switch (command)

            {
                case "deleteTask":

                    string path = Server.MapPath("~/Tasks.xml");
                    string taskId = e.CommandArgument.ToString();

                    XmlFile.DeleteTask(path, taskId);
                    Bind_gvTasks();

                    break;

                case "updateTask":
                    //open modal and populate with existing values
                    UpsertTaskPanel.Visible = true;
                    //fill in existing values 
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    tbEditTask.Text = commandArgs[1].ToString();
                    lbPriority.SelectedValue = commandArgs[2].ToString(); commandArgs[0].ToString();
                    upsertTaskID.Text = commandArgs[0].ToString();

                    break;

            }
        }
        //---------------------------------------------------------------------------------------
        //sort on task or priority
        protected void gvTasks_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (dir == SortDirection.Ascending)
            {
                dir = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                dir = SortDirection.Ascending;
                sortingDirection = "Asc";
            }
            DataTable dt = new DataTable();
            dt = (DataTable)Session["gvTasks"];
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["sortExpressionU"] = e.SortExpression.ToString();
            Session["sortDirectionU"] = sortingDirection.ToString();
            gvTasks.DataSource = sortedView;
            gvTasks.DataBind();
        }
        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }
        }



    }
}