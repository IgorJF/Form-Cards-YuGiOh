<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmCadastro.aspx.cs" Inherits="Tabuleiro.FrmCadastro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Cadastro de Usuario</title>
    <link href="css/styles.css" rel="stylesheet" />
</head>
<body>

     <nav id="nav1" runat="server">
        <div class="nav">
            <ul>
                <li><a href="Default.aspx">Inicio</a></li>
                <li><a href="FrmLogin.aspx">Login</a></li>
                <li><a href="FrmCadastro.aspx">Cadastro</a></li>
            </ul>
        </div>
    </nav>

    <form id="form1" runat="server">
        <div class="form">
             <p>
                <asp:TextBox runat="server" ID="txtNome" placeholder="Nome"/>
            </p>
            <p>
                <asp:TextBox runat="server" ID="txtUsuario" placeholder="Usuario"/>
            </p>
            <p>
                <asp:TextBox ID="txtDataNasc" runat="server" placeholder="Data de Nascimento"/>
            </p>
            <p>
                 <asp:TextBox runat="server" ID="txtSenha" TextMode="Password" placeholder="Senha" />
            </p>
             <p>
                <asp:DropDownList runat="server" ID="ddlPerfis"></asp:DropDownList>
            </p>
            <p>
                <asp:Button Text="Confirmar" runat="server" ID="btnConfirmar"  OnClick="btnConfirmar_Click"/>
            </p>
        </div>
    </form>



</body>
</html>
