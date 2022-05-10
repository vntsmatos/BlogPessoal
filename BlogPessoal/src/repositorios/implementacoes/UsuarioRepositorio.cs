using System.Collections.Generic;
using System.Linq;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;

namespace BlogPessoal.src.repositorios.implementacoes
{
    public class UsuarioRepositorio : IUsuario
    {
        #region Atributos

        private readonly BlogPessoalContext _context;

        #endregion Atributos

        #region Construtores

        public UsuarioRepositorio(BlogPessoalContext context)
        {
            _context = context;
        }

        #endregion Construtores

        #region Métodos

        public UsuarioModelo PegarUsuarioPeloId(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public List<UsuarioModelo> PegarUsuariosPeloNome(string nome)
        {
            return _context.Usuarios
                        .Where(u => u.Nome.Contains(nome))
                        .ToList();
        }

        public UsuarioModelo PegarUsuarioPeloEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public void NovoUsuario(NovoUsuarioDTO usuario)
        {
            _context.Usuarios.Add(new UsuarioModelo
            {
                Email = usuario.Email,
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Foto = usuario.Foto,
                Tipo = usuario.Tipo
            });

            _context.SaveChanges();
        }

        public void AtualizarUsuario(AtualizarUsuarioDTO usuario)
        {
            var usuarioExistente = PegarUsuarioPeloId(usuario.Id);
            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Senha = usuario.Senha;
            usuarioExistente.Foto = usuario.Foto;
            _context.Usuarios.Update(usuarioExistente);
            _context.SaveChanges();
        }

        public void DeletarUsuario(int id)
        {
            _context.Usuarios.Remove(PegarUsuarioPeloId(id));
            _context.SaveChanges();
        }

        #endregion Métodos
    }
}