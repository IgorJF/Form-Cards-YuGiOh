<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Tabuleiro.adm.Usuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalhes do Usuário</title>
    <link href="css/style.css" rel="stylesheet" />
</head>
<body>

    <nav id="nav1" runat="server">
        <div class="nav">
            <ul>
                <li><a href="Default.aspx">Inicio</a></li>
                <li><a href="Tabuleiro.aspx">Tabuleiro</a></li>
                <li><a href="Usuarios.aspx">Usuarios</a></li>
            </ul>
        </div>
    </nav>

    <form id="form1" runat="server">
        <div>
            <h1>Detalhes do Usuário</h1>
            <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Nome" HeaderText="Nome" />
                    <asp:BoundField DataField="UltimoAcesso" HeaderText="Último Acesso" />
                    <asp:BoundField DataField="QuantidadeAcessos" HeaderText="Quantidade de Acessos" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
