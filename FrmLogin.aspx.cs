using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tabuleiro
{
    public partial class FrmLogin : System.Web.UI.Page
    {
        // Método chamado quando a página é carregada
        protected void Page_Load(object sender, EventArgs e)
        {
            // Nada é feito neste momento, apenas a página é carregada
        }

        // Método chamado quando o botão "Login" é clicado
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Obter os dados do formulário de login
            string login = txtLogin.Text;
            string senha = txtSenha.Text;

            // Criptografar a senha fornecida para comparar com a senha no banco de dados
            string senhaCriptografada = FormsAuthentication.HashPasswordForStoringInConfigFile(senha, "SHA1");

            // Autenticar o usuário com base no login e senha criptografada
            bool autenticado = LoginDAO.AutenticarUsuario(login, senhaCriptografada);

            if (autenticado)
            {
                // Se a autenticação for bem-sucedida, registrar o acesso no log
                Usuario user = UsuarioDAO.SelecionarUsuario(login);
                if (user != null)
                {
                    // Registrar o acesso no log
                    LogAcessoDAO.Cadastrar(user);
                    // Redirecionar para a página principal, por exemplo
                    Response.Redirect("~/adm/Default.aspx");
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                    // Lidar com a situação em que o usuário não foi encontrado
                    // ou ocorreu algum problema ao obter os dados do usuário
                }
            }
            else
            {
                // Lidar com a falha de autenticação (exibir mensagem de erro, por exemplo)
            }
        }

    }
}