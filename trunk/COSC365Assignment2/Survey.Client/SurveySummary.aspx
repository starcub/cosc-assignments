<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveySummary.aspx.cs" Inherits="Survey.Client.SurveySummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Course Survey 2009
        <asp:Label ID="lblCourseTitle" runat="server" />
        <asp:Label ID="lblResponse" runat="server" />
        <asp:GridView ID="gvSurveyAverage" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="Question" DataField="Text" />
                <asp:BoundField HeaderText="Average Score(out of 5)" DataField="AverageScore" />
            </Columns>
        </asp:GridView>
        
        <asp:GridView ID="gvResponses" runat="server" AutoGenerateColumns="false" Visible="false">
            <Columns>
                <asp:BoundField HeaderText="Response" DataField="Response" />
                <asp:BoundField HeaderText="Submitted" DataField="DateSubmitted" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
