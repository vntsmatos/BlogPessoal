using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.repositorios.implementacoes
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IUsuario</para>
    /// <para>Criado por: Vinicius Santos Matos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
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

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuario pelo Id</para>
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <return>UsuarioModelo</return>
        public async Task<UsuarioModelo> PegarUsuarioPeloIdAsync(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar usuarios pelo nome</para>
        /// </summary>
        /// <param name="nome">Nome do usuario</param>
        /// <return>Lista UsuarioModelo</return>
        public async Task<List<UsuarioModelo>> PegarUsuariosPeloNomeAsync(string nome)
        {
            return await _context.Usuarios
                        .Where(u => u.Nome.Contains(nome))
                        .ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuario pelo email</para>
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <return>UsuarioModelo</return>
        public async Task<UsuarioModelo> PegarUsuarioPeloEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo usuario</para>
        /// </summary>
        /// <param name="usuario">NovoUsuarioDTO</param>
        public async Task NovoUsuarioAsync(NovoUsuarioDTO usuario)
        {
            await _context.Usuarios.AddAsync(new UsuarioModelo
            {
                Email = usuario.Email,
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Foto = usuario.Foto,
                Tipo = usuario.Tipo
            });

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar um usuario</para>
        /// </summary>
        /// <param name="usuario">AtualizarUsuarioDTO</param>
        public async Task AtualizarUsuarioAsync(AtualizarUsuarioDTO usuario)
        {
            var usuarioExistente = await PegarUsuarioPeloIdAsync(usuario.Id);
            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Senha = usuario.Senha;
            usuarioExistente.Foto = usuario.Foto;
            _context.Usuarios.Update(usuarioExistente);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um usuario</para>
        /// </summary>
        /// <param name="id">Id do usuario</param>
        public async Task DeletarUsuarioAsync(int id)
        {
            _context.Usuarios.Remove(await PegarUsuarioPeloIdAsync(id));
            await _context.SaveChangesAsync();
        }

        #endregion Métodos
    }
}