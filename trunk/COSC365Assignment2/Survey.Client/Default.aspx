﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Survey.Client._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblMessage" runat="server" />
        <asp:TextBox ID="txtUsercode" runat="server" />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_OnClick" />
        <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvCourses_OnRowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Course">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkCourse" runat="server" Text='<%# Eval("CourseCode") %>' NavigateUrl='<%#"~/CourseSurvey.aspx?CourseCode=" + Eval("CourseCode") %>' />    
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Role" DataField="Role" />
                <asp:BoundField HeaderText="Status" DataField="Status" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
