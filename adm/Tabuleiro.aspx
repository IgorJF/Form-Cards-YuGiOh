<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tabuleiro.aspx.cs" Inherits="Tabuleiro.adm.Tabuleiro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tabuleiro</title>
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
        <!-- Botão para abrir o modal -->
        <asp:Button ID="btnCadastrarCartas" runat="server" Text="Cadastrar Cartas" OnClientClick="abrirModal(); return false;" />

        <!-- Modal de cadastro de cartas -->
        <div id="modalCadastroCartas" class="modal">
            <div class="modal-content">
                <span class="close" onclick="fecharModal()">&times;</span>
                <h2>Formulário de Cadastro de Cartas</h2>
                <!-- Formulário de cadastro de cartas -->
                <asp:Label ID="lblNomeCarta" runat="server" Text="Nome da Carta:"></asp:Label>
                <asp:TextBox ID="txtNomeCarta" runat="server"></asp:TextBox><br />
                <br />

                <asp:Label ID="lblTipoCarta" runat="server" Text="Tipo da Carta:"></asp:Label>
                <asp:DropDownList ID="ddlTipoCarta" runat="server">
                    <asp:ListItem Text="Selecione.." Value=""></asp:ListItem>
                    <asp:ListItem Text="Monstro" Value="Monstro"></asp:ListItem>
                    <asp:ListItem Text="Armadilha" Value="Armadilha"></asp:ListItem>
                    <asp:ListItem Text="Magia" Value="Magia"></asp:ListItem>
                </asp:DropDownList><br />
                <br />

                <p>
                    Último ID cadastrado:
                    <asp:Label ID="lblUltimoID" runat="server"></asp:Label>
                </p>
                <p>O nome da imagem deve ser o nome do ID da carta que será cadastrada no momento.</p>
                <br />

                <asp:Label ID="lblImagemCarta" runat="server" Text="Imagem da Carta:"></asp:Label>
                <asp:FileUpload ID="fuImagemCarta" runat="server" /><br />
                <br />

                <asp:Button ID="btnSalvarCarta" runat="server" Text="Salvar Carta" OnClick="btnCadastrarCarta_Click" />
            </div>
        </div>

        <asp:Button ID="btnExibirCartas" runat="server" Text="Exibir Cartas" OnClick="btnExibirCartas_Click" />

        <div id="modalExibirCartas" class="modal">
            <div class="modal-content">
                <span class="close" onclick="fecharModalExibirCartas()">&times;</span>
                <h2>Cartas Cadastradas</h2>
                <div id="cardContainer" runat="server"></div>
            </div>
        </div>

        <div id="buttons">
            <asp:Button ID="btnZonaMonstro" runat="server" Text="Zona de Monstros" OnClientClick="abrirModalMonstro(); return false;" />
            <asp:Button ID="btnZonaAM" runat="server" Text="Zona de Armadilhas e Magias" OnClientClick="abrirModalAM(); return false;" />
            <asp:Button ID="btnCemiterio" runat="server" Text="Cemiterio" OnClientClick="abrirModalCemiterio(); return false;" />
            <asp:Button ID="btnDeck" runat="server" Text="Zona Deck Principal" OnClientClick="abrirModalDeck(); return false;" />
            <asp:Button ID="btnCampo" runat="server" Text="Zona de Campo" OnClientClick="abrirModalCampo(); return false;" />
            <asp:Button ID="btnAdicional" runat="server" Text="Zona Deck Adicional" OnClientClick="abrirModalAdicional(); return false;" />
            <asp:Button ID="btnPendulo" runat="server" Text="Zona de Pendulo" OnClientClick="abrirModalPendulo(); return false;" />
        </div>

        <!-- Modal da Zona de Monstros -->
        <div id="modalMonstro" class="modal">
            <div class="modal-content">
                <span class="close" onclick="fecharModalMonstro()">&times;</span>
                <h2>Zona de Monstros</h2>
                <h3>Todas as Cartas</h3>
                <div id="todasCartasMonstros" runat="server">
                </div>

                 <p>Não é posível colocar cartas que já estão em uma zona.</p>
                <asp:Button ID="btnConfirmarMonstros" runat="server" Text="Confirmar" OnClick="btnConfirmarMonstros_Click" />

                <h3>Cartas na Zona de Monstros</h3>
                <div id="cartasZonaMonstros" class="cartas-container" runat="server">
                </div>
            </div>
        </div>

        <!-- Modal da Zona de Armadilhas e Magias -->
        <div id="modalAM" class="modal">
            <div class="modal-content">
                <span class="close" onclick="fecharModalAM()">&times;</span>
                <h2>Zona de Armadilhas e Magias</h2>
                <h3>Todas as Cartas</h3>
                <div id="todasCartasAM" runat="server">
                    <!-- Aqui será preenchida a lista de todas as cartas -->
                </div>

                <p>Não é posível colocar cartas que já estão em uma zona.</p>
                <asp:Button ID="btnConfirmarAM" runat="server" Text="Confirmar" OnClick="btnConfirmarAM_Click" />

                <h3>Cartas na Zona de Armadilhas e Magias</h3>
                <div id="cartasZonaAM" class="cartas-container" runat="server">
                    <!-- Aqui será preenchida a lista de cartas selecionadas -->
                </div>
            </div>
        </div>


        <!-- Modal do Cemitério -->
        <div id="modalCemiterio" class="modal">
            <div class="modal-content">
                <span class="close" onclick="fecharModalCemiterio()">&times;</span>
                <h2>Cemitério</h2>
                <h3>Todas as Cartas</h3>
                <div id="todasCartasCemiterio" runat="server">
                    <!-- Lista de todas as cartas -->
                </div>

                <p>Não é posível colocar cartas que já estão em uma zona.</p>
                <asp:Button ID="btnConfirmarCemiterio" runat="server" Text="Confirmar" OnClick="btnConfirmarCemiterio_Click" />

                <h3>Cartas no Cemitério</h3>
                <div id="cartasZonaCemiterio" class="cartas-container" runat="server">
                    <!-- Lista de cartas selecionadas -->
                </div>
            </div>
        </div>


        <!-- Modal da Zona de Deck Principal -->
        <div id="modalDeck" class="modal">
            <div class="modal-content">
                <span class="close" onclick="fecharModalDeck()">&times;</span>
                <h2>Zona Deck Principal</h2>
                <h3>Todas as Cartas</h3>
                <div id="todasCartasDeckPrincipal" runat="server">
                    <!-- Lista de todas as cartas -->
                </div>

                <p>Não é posível colocar cartas que já estão em uma zona.</p>
                <asp:Button ID="btnConfirmarDeckPrincipal" runat="server" Text="Confirmar" OnClick="btnConfirmarDeckPrincipal_Click" />

                <h3>Cartas na Zona Deck Principal</h3>
                <div id="cartasZonaDeckPrincipal" class="cartas-container" runat="server">
                    <!-- Lista de cartas selecionadas -->
                </div>
            </div>
        </div>

        <!-- Modal da Zona de Campo -->
        <div id="modalCampo" class="modal">
            <div class="modal-content">
                <span class="close" onclick="fecharModalCampo()">&times;</span>
                <h2>Zona de Campo</h2>
                <h3>Todas as Cartas</h3>
                <div id="todasCartasCampo" runat="server">
                    <!-- Lista de todas as cartas -->
                </div>

                <p>Não sé posível colocar cartas que já estão em uma zona.</p>
                <asp:Button ID="btnConfirmarCampo" runat="server" Text="Confirmar" OnClick="btnConfirmarCampo_Click" />

                <h3>Cartas na Zona de Campo</h3>
                <div id="cartasZonaCampo" class="cartas-container" runat="server">
                    <!-- Lista de cartas selecionadas -->
                </div>
            </div>
        </div>

        <!-- Modal da Zona de Deck Adicional -->
        <div id="modalAdicional" class="modal">
            <div class="modal-content">
                <span class="close" onclick="fecharModalAdicional()">&times;</span>
                <h2>Zona de Deck Adicional</h2>
                <h3>Todas as Cartas</h3>
                <div id="todasCartasDeckAdicional" runat="server">
                    <!-- Lista de todas as cartas -->
                </div>

                <p>Não é posível colocar cartas que já estão em uma zona.</p>
                <asp:Button ID="btnConfirmarDeckAdicional" runat="server" Text="Confirmar" OnClick="btnConfirmarDeckAdicional_Click" />

                <h3>Cartas na Zona de Deck Adicional</h3>
                <div id="cartasZonaDeckAdicional" class="cartas-container" runat="server">
                    <!-- Lista de cartas selecionadas -->
                </div>
            </div>
        </div>

        <!-- Modal da Zona de Pêndulo -->
        <div id="modalPendulo" class="modal">
            <div class="modal-content">
                <span class="close" onclick="fecharModalPendulo()">&times;</span>
                <h2>Zona de Pêndulo</h2>
                <h3>Todas as Cartas</h3>
                <div id="todasCartasPendulo" runat="server">
                    <!-- Lista de todas as cartas -->
                </div>

                <p>Não é posível colocar cartas que já estão em uma zona.</p>
                <asp:Button ID="btnConfirmarPendulo" runat="server" Text="Confirmar" OnClick="btnConfirmarPendulo_Click" />

                <h3>Cartas na Zona de Pêndulo</h3>
                <div id="cartasZonaPendulo" class="cartas-container" runat="server">
                    <!-- Lista de cartas selecionadas -->
                </div>
            </div>
        </div>

        <script src="js/script.js"></script>
    </form>
</body>
</html>
