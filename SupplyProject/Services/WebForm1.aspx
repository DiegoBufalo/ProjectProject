<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SupplyProject.Services.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <div>
            <table border="0">
                <tr>
                    <td>
                        <label for="txtOrigem">Origem</label>
                        <asp:TextBox runat="server" ID="txtOrigem" ClientIDMode="Static" />
                    </td>
                    </tr>
                <tr>
                    <td>
                        <label for="txtDestino">Destino</label>
                        <asp:TextBox runat="server" ID="txtDestino" ClientIDMode="Static" />                    
                    </td>
                </tr>
            </table>
            <asp:Button Text="Calcular" ID="btnCalcular" runat="server" 
                onclick="btnCalcular_Click" />
        </div>
    </div>
        <asp:Literal ID="litResultado" runat="server" />
    </form>
</body>
</html>
