<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDoList._Default" %>
  
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <link rel="stylesheet" href="Content/TODO.css">

    <div id="taskHeader">
         <asp:Button ID="addNewTask" runat="server" Text="Add Task" OnClick="AddNewTask_Click" />
    </div>
      
    <div id="gridDiv">
    <asp:GridView ID="gvTasks" runat="server" AutoGenerateColumns="false" DataKeyNames="ID"  OnRowCommand="FireRowCommand" CellPadding="10" AllowSorting="true" OnSorting="gvTasks_Sorting"> 
         <HeaderStyle CssClass="GridViewRowHeader"/>
        <Columns>

             <asp:TemplateField HeaderText="Edit / Delete"  >
                    <ItemTemplate>
                        <asp:Button ID="updateButton" runat="server" Text="Edit" CssClass="UpdateBtn" CommandName="updateTask" CommandArgument='<%#Eval("ID")+","+ Eval("taskText")+","+ Eval("Priority")%>'/>
                        <asp:Button ID="deleteButton" runat="server" Text="Delete" CssClass="DeleteBtn" CommandName="deleteTask" CommandArgument='<%#Eval("ID") %>'  OnClientClick="return confirm('Are you certain you want to delete this task?');"/>
                    </ItemTemplate>
                </asp:TemplateField>

             <asp:BoundField DataField="Priority" HeaderText="Priority" SortExpression="Priority"></asp:BoundField >

            <asp:TemplateField HeaderText="Tasks" SortExpression="taskText" >
                    <HeaderStyle Width="500px" HorizontalAlign="Left"/>
                    <ItemTemplate>
                         <ItemStyle Width="500px" HorizontalAlign="Left"/>
                            <asp:Literal  runat="server" Text='<%# Eval("taskText") %>'></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>

             <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="hiddencol"  HeaderStyle-CssClass="hiddencol"  ></asp:BoundField >
        </Columns>
    </asp:GridView>
        </div>
    <asp:Panel ID="UpsertTaskPanel" runat="server" class="popup"  Visible="false" >
        
        <asp:TextBox ID="tbEditTask" runat="server" CssClass="editTb" ></asp:TextBox>
        
        <asp:ListBox ID="lbPriority" runat="server" Rows="1">
            <asp:ListItem>Hot Rush</asp:ListItem>
            <asp:ListItem Selected="True">Normal</asp:ListItem>
            <asp:ListItem>Whenever</asp:ListItem>
        </asp:ListBox>
       
        <asp:Button ID="UpsertTask" runat="server" Text="Save" OnClick="UpsertTask_Click" />
        
        <asp:Label ID="upsertTaskID" runat="server" CssClass="hiddencol"></asp:Label>
    </asp:Panel>


</asp:Content>
