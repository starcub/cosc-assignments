<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseSurvey.aspx.cs" Inherits="Survey.Client.CourseSurvey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span>Course Survey 2009</span>
        <asp:Label ID="lblTitle" runat="server" Text="" />
        <asp:Literal ID="ltrIntro" runat="server" Text="Please rate this course according to each of the categories below. Please also include any comments that might be helpful for improving this course. Thank You!" />
        <asp:Repeater ID="rptQuestions" runat="server" >
            <ItemTemplate>
                <asp:Label ID="lblQuestionText" runat="server" Font-Bold="true" Text='<%# Eval("Text") %>' /><br />
                <asp:RadioButtonList ID="rblScore" runat="server" RepeatDirection="Horizontal" >
                    <asp:ListItem Text="Strongly Agree" Value="5" />
                    <asp:ListItem Text="Agree" Value="4" />
                    <asp:ListItem Text="Neutral" Value="3" />
                    <asp:ListItem Text="Disagree" Value="2" />
                    <asp:ListItem Text="Strongly Disagree" Value="1" />
                </asp:RadioButtonList>
                Comments:<br />
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" /><br /><br />
            </ItemTemplate>
        </asp:Repeater>
        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_OnClick" />
        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_OnClick" />
    </div>
    </form>
</body>
</html>
