using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tabuleiro.adm
{
    public partial class Usuarios : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarUsuarios();
            }
        }

        private void CarregarUsuarios()
        {
            List<UsuarioDetalhes> usuariosDetalhes = ObterDetalhesUsuarios();
            gvUsuarios.DataSource = usuariosDetalhes;
            gvUsuarios.DataBind();
        }

        private List<UsuarioDetalhes> ObterDetalhesUsuarios()
        {
            List<UsuarioDetalhes> usuariosDetalhes = new List<UsuarioDetalhes>();

            // Obter todos os usuários do banco de dados
            List<Usuario> usuarios = UsuarioDAO.SelecionarUsuario(); // Implemente este método

            foreach (Usuario usuario in usuarios)
            {
                UsuarioDetalhes detalhes = new UsuarioDetalhes
                {
                    Nome = usuario.Nome,
                    UltimoAcesso = ObterUltimoAcesso(usuario.idUsuario), // Implemente este método
                    QuantidadeAcessos = ObterQuantidadeAcessos(usuario.idUsuario) // Implemente este método
                };

                usuariosDetalhes.Add(detalhes);
            }

            return usuariosDetalhes;
        }

        private DateTime? ObterUltimoAcesso(int usuarioId)
        {
            List<LogAcesso> logs = LogAcessoDAO.SelecionarLogAcessosPorUsuario(usuarioId);
            if (logs != null && logs.Any())
            {
                return logs.First().UltimoAcesso;
            }
            return null;
        }

        private int ObterQuantidadeAcessos(int usuarioId)
        {
            List<LogAcesso> logs = LogAcessoDAO.SelecionarLogAcessosPorUsuario(usuarioId);
            return logs?.Count ?? 0;
        }
    }

    public class UsuarioDetalhes
    {
        public string Nome { get; set; }
        public DateTime? UltimoAcesso { get; set; }
        public int QuantidadeAcessos { get; set; }
    }
}