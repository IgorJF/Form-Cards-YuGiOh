<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tabuleiro.adm.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Administração</title>
    <link href="css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <nav id="nav1" runat="server">
            <div class="nav">
                <ul>
                    <li><a href="Default.aspx">Inicio</a></li>
                    <li><a href="Tabuleiro.aspx">Tabuleiro</a></li>
                    <li><a href="Usuarios.aspx">Usuarios</a></li>
                </ul>
            </div>
            <div class="logout">
                <asp:LinkButton ID="btnLogout" runat="server" OnClick="btnLogout_Click">Logout</asp:LinkButton>
            </div>
        </nav>

        <div>
            <p>Igor Jorge Ferraz</p>
            <p>Kaua Fernandes Farrapo</p>
            <p>Rodrigo de Jesus Gambarte Onofre</p>
            <p>Eloi Evaristo dos Santos</p>
            <p>3 TII</p>
        </div>
    </form>
</body>
</html>
