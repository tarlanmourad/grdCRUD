<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="grdCRUD.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="CSS/style.css" rel="stylesheet" />
    <title>CRUD</title>
</head>
<body>
    <form id="frmCRUD" runat="server">
        <div>
            <asp:GridView ID="grdView" runat="server" OnRowCancelingEdit="grdView_RowCancelingEdit" 
                OnRowDeleting="grdView_RowDeleting" OnRowEditing="grdView_RowEditing" 
                OnRowUpdating="grdView_RowUpdating" AutoGenerateColumns="False" CssClass="mydatagrid" 
                PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("ProductID")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                    <asp:BoundField DataField="QuantityPerUnit" HeaderText="Quantity" />
                    <asp:BoundField DataField="UnitPrice" HeaderText="Price" />
                    <asp:BoundField DataField="UnitsInStock" HeaderText="Stock" />
                    <asp:CommandField ShowEditButton="true" />
                    <asp:CommandField ShowDeleteButton="true" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
