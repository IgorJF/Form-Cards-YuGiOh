using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tabuleiro
{
    public partial class FrmCadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AtualizarDDLPerfis();
            }
        }

        private void AtualizarDDLPerfis()
        {
            // Lista de perfis obtida da classe PerfilDAO
            List<Perfil> perfis = PerfilDAO.ListarPerfis();

            // Define a fonte de dados (DataSource) do dropdown list como a lista de perfis
            ddlPerfis.DataSource = perfis;

            // Define o campo a ser exibido para cada item do dropdown list como o nome do perfil
            ddlPerfis.DataTextField = "Nome";

            // Define o campo de valor para cada item do dropdown list como o IdPerfil do perfil
            ddlPerfis.DataValueField = "IdPerfil";

            // Faz o data binding para preencher o dropdown list com os perfis
            ddlPerfis.DataBind();

            // Insere um item no topo do dropdown list para indicar seleção
            ddlPerfis.Items.Insert(0, "Selecione..");
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ddlPerfis.SelectedIndex > 0)
            {
                // Cria um novo objeto Usuario e preenche os campos com os valores do formulário
                var user = new Usuario();

                user.Nome = txtNome.Text;
                user.Usuario1 = txtUsuario.Text;

                // Obtém a senha em texto normal do formulário e a criptografa usando SHA1
                var senhaNormal = txtSenha.Text;
                string senhaCriptografada = FormsAuthentication.HashPasswordForStoringInConfigFile(senhaNormal, "SHA1");

                user.Senha = senhaCriptografada;

                user.dataNascimento = Convert.ToDateTime(txtDataNasc.Text);
                user.PerfilId = Convert.ToInt32(ddlPerfis.SelectedValue);

                // Chama o método Cadastrar da classe UsuarioDAO para cadastrar o usuário no sistema
                string mensagem = UsuarioDAO.Cadastrar(user);

                // Limpa os campos do formulário
                LimparCampos();
            }
        }

        private void LimparCampos()
        {
            txtDataNasc.Text = "";
            txtUsuario.Text = "";
            txtNome.Text = "";
            txtSenha.Text = "";
            ddlPerfis.SelectedIndex = 0;
        }
    }
}