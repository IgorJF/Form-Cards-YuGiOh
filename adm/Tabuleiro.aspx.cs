using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Tabuleiro.adm
{
    public partial class Tabuleiro : System.Web.UI.Page
    {

        private List<CheckBox> checkboxes = new List<CheckBox>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Página de tabuleiro
            PreencherTodasCartasNaZona();
            PreencherCartasZonaMonstros();
            PreencherTodasCartasNaZonaAM();
            PreencherCartasZonaAM();
            PreencherTodasCartasNaZonaCemiterio();
            PreencherCartasZonaCemiterio();
            PreencherCartasZonaDeckPrincipal();
            PreencherTodasCartasNaZonaDeckPrincipal();
            PreencherTodasCartasNaZonaCampo();
            PreencherCartasZonaCampo();
            PreencherTodasCartasNaZonaDeckAdicional();
            PreencherCartasZonaDeckAdicional();
            PreencherCartasZonaPendulo();
            PreencherTodasCartasNaZonaPendulo();
            int ultimoID = CartaDAO.ObterUltimoIDCadastrado();
            lblUltimoID.Text = ultimoID.ToString();
        }

        protected void btnCadastrarCarta_Click(object sender, EventArgs e)
        {
            var carta = new Carta();
            carta.Nome = txtNomeCarta.Text;
            carta.Tipo = ddlTipoCarta.SelectedValue;

            if (fuImagemCarta.HasFile)
            {
                // Supondo que o campo ID é o identificador único da carta no banco de dados
                int idCarta = CartaDAO.ObterProximoID(); // Método para obter o próximo ID disponível

                string nomeArquivo = idCarta + ".jpg"; // Salvar como JPG

                string caminho = Server.MapPath("~/adm/img/" + nomeArquivo); // Caminho onde a imagem será salva

                using (System.Drawing.Image imagem = System.Drawing.Image.FromStream(fuImagemCarta.PostedFile.InputStream))
                {
                    // Salva a imagem no formato JPG
                    imagem.Save(caminho, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                carta.idCarta = idCarta; // Define o ID da carta
                carta.NomeArquivoImagem = nomeArquivo; // Salva o nome do arquivo da imagem na carta
                carta.TipoImagem = "jpg"; // Salva a extensão como JPG

            }

            string mensagem = CartaDAO.Cadastrar(carta);

            // Limpa os campos do formulário
            LimparCampos();

            // Verifique a mensagem de retorno
            if (!string.IsNullOrEmpty(mensagem))
            {
                // Exiba a mensagem de erro ou faça algo com ela
                // ...
            }
        }
        protected void btnExibirCartas_Click(object sender, EventArgs e)
        {
            List<Carta> cartas = CartaDAO.ObterTodasAsCartas();

            if (cartas != null && cartas.Any())
            {
                string htmlCards = "<div class='card-container'>";

                foreach (var carta in cartas)
                {
                    if (carta.idCarta > 0) // Verifica se o ID é válido
                    {
                        string imagePath = ResolveUrl($"~/adm/img/{carta.idCarta}.jpg");

                        htmlCards += "<div class='card'>";
                        htmlCards += $"<img src='{imagePath}' alt='{carta.Nome}' />";
                        htmlCards += $"<h3>{carta.Nome}</h3>";
                        htmlCards += $"<p>Tipo: {carta.Tipo}</p>";
                        htmlCards += "</div>";
                    }
                    else
                    {
                        htmlCards += "<div class='card'>";
                        htmlCards += $"<p>Imagem não encontrada para {carta.Nome}</p>";
                        htmlCards += $"<h3>{carta.Nome}</h3>";
                        htmlCards += $"<p>Tipo: {carta.Tipo}</p>";
                        htmlCards += "</div>";
                    }
                }

                htmlCards += "</div>";

                cardContainer.Controls.Clear();
                cardContainer.Controls.Add(new LiteralControl(htmlCards));
            }
            else
            {
                cardContainer.InnerHtml = "Nenhuma carta encontrada!";
            }

            ClientScript.RegisterStartupScript(this.GetType(), "ExibirCartas", "<script>exibirModalDeCartas();</script>");
        }


        private void PreencherTodasCartasNaZona()
        {
            List<Carta> todasCartas = CartaDAO.ObterTodasAsCartas();

            foreach (var carta in todasCartas)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.ID = "chk" + carta.idCarta;
                checkbox.Text = $"{carta.Nome} - {ObterZonaDaCarta(carta.idCarta)}";
                checkboxes.Add(checkbox);
                todasCartasMonstros.Controls.Add(checkbox);
                todasCartasMonstros.Controls.Add(new LiteralControl("<br />"));
            }
        }


        private void PreencherCartasZonaMonstros()
        {
            List<Carta> cartasZonaMonstros = CartaDAO.ObterCartasDaZona(1);

            string htmlCartasZonaMonstros = "";

            foreach (var carta in cartasZonaMonstros)
            {
                string imagePath = ResolveUrl($"~/adm/img/{carta.idCarta}.jpg");

                htmlCartasZonaMonstros += "<div class='card'>";
                htmlCartasZonaMonstros += $"<img src='{imagePath}' alt='{carta.Nome}' />";
                htmlCartasZonaMonstros += $"<h3>{carta.Nome}</h3>";
                htmlCartasZonaMonstros += $"<p>Tipo: {carta.Tipo}</p>";
                htmlCartasZonaMonstros += "</div>";
            }

            HtmlGenericControl cartasZonaMonstrosControl = (HtmlGenericControl)FindControl("cartasZonaMonstros");

            if (cartasZonaMonstrosControl != null)
            {
                cartasZonaMonstrosControl.InnerHtml = htmlCartasZonaMonstros;
            }
        }


        protected void btnConfirmarMonstros_Click(object sender, EventArgs e)
        {
            List<int> cartasSelecionadas = new List<int>();

            foreach (var checkbox in checkboxes)
            {
                if (checkbox.Checked)
                {
                    int cartaId = int.Parse(checkbox.ID.Replace("chk", ""));
                    cartasSelecionadas.Add(cartaId);
                }
            }

            CartaDAO.AssociarCartasAZona(1, cartasSelecionadas);

            Response.Redirect(Request.RawUrl);
        }

        private void PreencherTodasCartasNaZonaAM()
        {
            List<Carta> todasCartas = CartaDAO.ObterTodasAsCartas();

            foreach (var carta in todasCartas)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.ID = "chkAM" + carta.idCarta; // Identificação única para cada checkbox
                checkbox.Text = $"{carta.Nome} - {ObterZonaDaCarta(carta.idCarta)}";
                checkboxes.Add(checkbox);
                todasCartasAM.Controls.Add(checkbox);
                todasCartasAM.Controls.Add(new LiteralControl("<br />"));
            }
        }

        private void PreencherCartasZonaAM()
        {
            List<Carta> cartasZonaAM = CartaDAO.ObterCartasDaZonaAM(2); // Suponha que o ID da Zona de Armadilhas e Magias é 2

            string htmlCartasZonaAM = "";

            foreach (var carta in cartasZonaAM)
            {
                string imagePath = ResolveUrl($"~/adm/img/{carta.idCarta}.jpg");

                htmlCartasZonaAM += "<div class='card'>";
                htmlCartasZonaAM += $"<img src='{imagePath}' alt='{carta.Nome}' />";
                htmlCartasZonaAM += $"<h3>{carta.Nome}</h3>";
                htmlCartasZonaAM += $"<p>Tipo: {carta.Tipo}</p>";
                htmlCartasZonaAM += "</div>";
            }

            HtmlGenericControl cartasZonaAMControl = (HtmlGenericControl)FindControl("cartasZonaAM");

            if (cartasZonaAMControl != null)
            {
                cartasZonaAMControl.InnerHtml = htmlCartasZonaAM;
            }
        }

        protected void btnConfirmarAM_Click(object sender, EventArgs e)
        {
            List<int> cartasSelecionadas = new List<int>();

            foreach (var checkbox in checkboxes)
            {
                if (checkbox.Checked)
                {
                    int cartaId = int.Parse(checkbox.ID.Replace("chkAM", ""));
                    cartasSelecionadas.Add(cartaId);
                }
            }

            CartaDAO.AssociarCartasAZonaAM(2, cartasSelecionadas); // Suponha que o ID da Zona de Armadilhas e Magias é 2

            Response.Redirect(Request.RawUrl);
        }


        private void PreencherTodasCartasNaZonaCemiterio()
        {
            List<Carta> todasCartas = CartaDAO.ObterTodasAsCartas();

            foreach (var carta in todasCartas)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.ID = "chkCemiterio" + carta.idCarta;
                checkbox.Text = $"{carta.Nome} - {ObterZonaDaCarta(carta.idCarta)}";
                checkboxes.Add(checkbox);
                todasCartasCemiterio.Controls.Add(checkbox);
                todasCartasCemiterio.Controls.Add(new LiteralControl("<br />"));
            }
        }

        private void PreencherCartasZonaCemiterio()
        {
            List<Carta> cartasZonaCemiterio = CartaDAO.ObterCartasDaZonaCemiterio(3); // Suponha que o ID da Zona do Cemitério seja 7

            string htmlCartasZonaCemiterio = "";

            foreach (var carta in cartasZonaCemiterio)
            {
                string imagePath = ResolveUrl($"~/adm/img/{carta.idCarta}.jpg");

                htmlCartasZonaCemiterio += "<div class='card'>";
                htmlCartasZonaCemiterio += $"<img src='{imagePath}' alt='{carta.Nome}' />";
                htmlCartasZonaCemiterio += $"<h3>{carta.Nome}</h3>";
                htmlCartasZonaCemiterio += $"<p>Tipo: {carta.Tipo}</p>";
                htmlCartasZonaCemiterio += "</div>";
            }

            HtmlGenericControl cartasZonaCemiterioControl = (HtmlGenericControl)FindControl("cartasZonaCemiterio");

            if (cartasZonaCemiterioControl != null)
            {
                cartasZonaCemiterioControl.InnerHtml = htmlCartasZonaCemiterio;
            }
        }

        protected void btnConfirmarCemiterio_Click(object sender, EventArgs e)
        {
            List<int> cartasSelecionadas = new List<int>();

            foreach (var checkbox in checkboxes)
            {
                if (checkbox.Checked)
                {
                    int cartaId = int.Parse(checkbox.ID.Replace("chkCemiterio", ""));
                    cartasSelecionadas.Add(cartaId);
                }
            }

            CartaDAO.AssociarCartasAZonaCemiterio(3, cartasSelecionadas); // Suponha que o ID da Zona do Cemitério seja 7

            Response.Redirect(Request.RawUrl);
        }


        private void PreencherTodasCartasNaZonaDeckPrincipal()
        {
            List<Carta> todasCartas = CartaDAO.ObterTodasAsCartas();

            foreach (var carta in todasCartas)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.ID = "chkDeck" + carta.idCarta;
                checkbox.Text = $"{carta.Nome} - {ObterZonaDaCarta(carta.idCarta)}";
                checkboxes.Add(checkbox);
                todasCartasDeckPrincipal.Controls.Add(checkbox);
                todasCartasDeckPrincipal.Controls.Add(new LiteralControl("<br />"));
            }
        }

        private void PreencherCartasZonaDeckPrincipal()
        {
            List<Carta> cartasZonaDeckPrincipal = CartaDAO.ObterCartasDaZonaDeckPrincipal(4); // Suponha que o ID da Zona de Deck Principal seja 6

            string htmlCartasZonaDeckPrincipal = "";

            foreach (var carta in cartasZonaDeckPrincipal)
            {
                string imagePath = ResolveUrl($"~/adm/img/{carta.idCarta}.jpg");

                htmlCartasZonaDeckPrincipal += "<div class='card'>";
                htmlCartasZonaDeckPrincipal += $"<img src='{imagePath}' alt='{carta.Nome}' />";
                htmlCartasZonaDeckPrincipal += $"<h3>{carta.Nome}</h3>";
                htmlCartasZonaDeckPrincipal += $"<p>Tipo: {carta.Tipo}</p>";
                htmlCartasZonaDeckPrincipal += "</div>";
            }

            HtmlGenericControl cartasZonaDeckPrincipalControl = (HtmlGenericControl)FindControl("cartasZonaDeckPrincipal");

            if (cartasZonaDeckPrincipalControl != null)
            {
                cartasZonaDeckPrincipalControl.InnerHtml = htmlCartasZonaDeckPrincipal;
            }
        }

        protected void btnConfirmarDeckPrincipal_Click(object sender, EventArgs e)
        {
            List<int> cartasSelecionadas = new List<int>();

            foreach (var checkbox in checkboxes)
            {
                if (checkbox.Checked)
                {
                    int cartaId = int.Parse(checkbox.ID.Replace("chkDeck", ""));
                    cartasSelecionadas.Add(cartaId);
                }
            }

            CartaDAO.AssociarCartasAZonaDeckPrincipal(4, cartasSelecionadas); // Suponha que o ID da Zona de Deck Principal seja 6

            Response.Redirect(Request.RawUrl);
        }

        private void PreencherTodasCartasNaZonaCampo()
        {
            List<Carta> todasCartas = CartaDAO.ObterTodasAsCartas();

            foreach (var carta in todasCartas)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.ID = "chkCampo" + carta.idCarta;
                checkbox.Text = $"{carta.Nome} - {ObterZonaDaCarta(carta.idCarta)}";
                checkboxes.Add(checkbox);
                todasCartasCampo.Controls.Add(checkbox);
                todasCartasCampo.Controls.Add(new LiteralControl("<br />"));
            }
        }

        private void PreencherCartasZonaCampo()
        {
            List<Carta> cartasZonaCampo = CartaDAO.ObterCartasDaZonaCampo(5); // Suponha que o ID da Zona de Campo seja 8

            string htmlCartasZonaCampo = "";

            foreach (var carta in cartasZonaCampo)
            {
                string imagePath = ResolveUrl($"~/adm/img/{carta.idCarta}.jpg");

                htmlCartasZonaCampo += "<div class='card'>";
                htmlCartasZonaCampo += $"<img src='{imagePath}' alt='{carta.Nome}' />";
                htmlCartasZonaCampo += $"<h3>{carta.Nome}</h3>";
                htmlCartasZonaCampo += $"<p>Tipo: {carta.Tipo}</p>";
                htmlCartasZonaCampo += "</div>";
            }

            HtmlGenericControl cartasZonaCampoControl = (HtmlGenericControl)FindControl("cartasZonaCampo");

            if (cartasZonaCampoControl != null)
            {
                cartasZonaCampoControl.InnerHtml = htmlCartasZonaCampo;
            }
        }

        protected void btnConfirmarCampo_Click(object sender, EventArgs e)
        {
            List<int> cartasSelecionadas = new List<int>();

            foreach (var checkbox in checkboxes)
            {
                if (checkbox.Checked)
                {
                    int cartaId = int.Parse(checkbox.ID.Replace("chkCampo", ""));
                    cartasSelecionadas.Add(cartaId);
                }
            }

            CartaDAO.AssociarCartasAZonaCampo(5, cartasSelecionadas); // Suponha que o ID da Zona de Campo seja 8

            Response.Redirect(Request.RawUrl);
        }

        private void PreencherTodasCartasNaZonaDeckAdicional()
        {
            List<Carta> todasCartas = CartaDAO.ObterTodasAsCartas();

            foreach (var carta in todasCartas)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.ID = "chkDeckAdicional" + carta.idCarta;
                checkbox.Text = $"{carta.Nome} - {ObterZonaDaCarta(carta.idCarta)}";
                checkboxes.Add(checkbox);
                todasCartasDeckAdicional.Controls.Add(checkbox);
                todasCartasDeckAdicional.Controls.Add(new LiteralControl("<br />"));
            }
        }

        private void PreencherCartasZonaDeckAdicional()
        {
            List<Carta> cartasZonaDeckAdicional = CartaDAO.ObterCartasDaZonaDeckAdicional(6); // Suponha que o ID da Zona de Deck Adicional seja 9

            string htmlCartasZonaDeckAdicional = "";

            foreach (var carta in cartasZonaDeckAdicional)
            {
                string imagePath = ResolveUrl($"~/adm/img/{carta.idCarta}.jpg");

                htmlCartasZonaDeckAdicional += "<div class='card'>";
                htmlCartasZonaDeckAdicional += $"<img src='{imagePath}' alt='{carta.Nome}' />";
                htmlCartasZonaDeckAdicional += $"<h3>{carta.Nome}</h3>";
                htmlCartasZonaDeckAdicional += $"<p>Tipo: {carta.Tipo}</p>";
                htmlCartasZonaDeckAdicional += "</div>";
            }

            HtmlGenericControl cartasZonaDeckAdicionalControl = (HtmlGenericControl)FindControl("cartasZonaDeckAdicional");

            if (cartasZonaDeckAdicionalControl != null)
            {
                cartasZonaDeckAdicionalControl.InnerHtml = htmlCartasZonaDeckAdicional;
            }
        }

        protected void btnConfirmarDeckAdicional_Click(object sender, EventArgs e)
        {
            List<int> cartasSelecionadas = new List<int>();

            foreach (var checkbox in checkboxes)
            {
                if (checkbox.Checked)
                {
                    int cartaId = int.Parse(checkbox.ID.Replace("chkDeckAdicional", ""));
                    cartasSelecionadas.Add(cartaId);
                }
            }

            CartaDAO.AssociarCartasAZonaDeckAdicional(6, cartasSelecionadas); // Suponha que o ID da Zona de Deck Adicional seja 9

            Response.Redirect(Request.RawUrl);
        }

        private void PreencherTodasCartasNaZonaPendulo()
        {
            List<Carta> todasCartas = CartaDAO.ObterTodasAsCartas();

            foreach (var carta in todasCartas)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.ID = "chkPendulo" + carta.idCarta;
                checkbox.Text = $"{carta.Nome} - {ObterZonaDaCarta(carta.idCarta)}";
                checkboxes.Add(checkbox);
                todasCartasPendulo.Controls.Add(checkbox);
                todasCartasPendulo.Controls.Add(new LiteralControl("<br />"));
            }
        }

        private void PreencherCartasZonaPendulo()
        {
            List<Carta> cartasZonaPendulo = CartaDAO.ObterCartasDaZonaPendulo(10); // Suponha que o ID da Zona de Pêndulo seja 10

            string htmlCartasZonaPendulo = "";

            foreach (var carta in cartasZonaPendulo)
            {
                string imagePath = ResolveUrl($"~/adm/img/{carta.idCarta}.jpg");

                htmlCartasZonaPendulo += "<div class='card'>";
                htmlCartasZonaPendulo += $"<img src='{imagePath}' alt='{carta.Nome}' />";
                htmlCartasZonaPendulo += $"<h3>{carta.Nome}</h3>";
                htmlCartasZonaPendulo += $"<p>Tipo: {carta.Tipo}</p>";
                htmlCartasZonaPendulo += "</div>";
            }

            HtmlGenericControl cartasZonaPendulolControl = (HtmlGenericControl)FindControl("cartasZonaPendulo");

            if (cartasZonaPendulolControl != null)
            {
                cartasZonaPendulolControl.InnerHtml = htmlCartasZonaPendulo;
            }
        }

        protected void btnConfirmarPendulo_Click(object sender, EventArgs e)
        {
            List<int> cartasSelecionadas = new List<int>();

            foreach (var checkbox in checkboxes)
            {
                if (checkbox.Checked)
                {
                    int cartaId = int.Parse(checkbox.ID.Replace("chkPendulo", ""));
                    cartasSelecionadas.Add(cartaId);
                }
            }

            CartaDAO.AssociarCartasAZonaPendulo(10, cartasSelecionadas); // Suponha que o ID da Zona de Pêndulo seja 10

            Response.Redirect(Request.RawUrl);
        }

        private string ObterZonaDaCarta(int idCarta)
        {
            // Aqui você deve implementar a lógica para buscar a zona da carta no banco de dados
            string zona = CartaDAO.ObterZonaDaCartaPorID(idCarta);

            if (string.IsNullOrEmpty(zona))
            {
                return "Sem Zona"; // Se a zona não for encontrada, retorna "Sem Zona"
            }
            else
            {
                return zona; // Retorna a zona da carta
            }
        }


        private void LimparCampos()
        {
            txtNomeCarta.Text = "";
            ddlTipoCarta.SelectedIndex = 0;
        }
    }
}