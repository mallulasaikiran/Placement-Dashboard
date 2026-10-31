<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Companies.aspx.cs"
    Inherits="PlacementDashboard.Companies" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Company Placement Details</title>
</head>
<body>
    <form id="form1" runat="server">

        <h1>COMPANY PLACEMENT DETAILS</h1>

        <asp:GridView ID="gvCompanies"
            runat="server"
            AutoGenerateColumns="True"
            BorderWidth="1"
            CellPadding="5"
            GridLines="Both"
            EmptyDataText="No company records found.">
        </asp:GridView>

        <br />

        <h2>Company Summary</h2>

        <p>
            Total Companies:
            <asp:Label ID="lblCompanies" runat="server" />
        </p>

        <p>
            Highest Package:
            <asp:Label ID="lblHighestPackage" runat="server" />
            LPA
        </p>

        <p>
            Average Package:
            <asp:Label ID="lblAveragePackage" runat="server" />
            LPA
        </p>

        <br />

        <asp:HyperLink ID="lnkHome"
            runat="server"
            NavigateUrl="~/Default.aspx"
            Text="Back to Placement Dashboard" />

    </form>
</body>
</html>