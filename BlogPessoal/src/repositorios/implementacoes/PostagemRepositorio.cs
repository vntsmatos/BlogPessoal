using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.repositorios.implementacoes
{
    public class PostagemRepositorio : IPostagem
    {
        #region Atributos

        private readonly BlogPessoalContext _context;

        #endregion Atributos


        #region Construtores

        public PostagemRepositorio(BlogPessoalContext context)
        {
            _context = context;
        }

        #endregion Construtores


        #region Métodos

        public async Task<List<PostagemModelo>> PegarTodasPostagensAsync()
        {
            return await _context.Postagens
                .Include(p => p.Criador)
                .Include(p => p.Tema)
                .ToListAsync();
        }

        public async Task<PostagemModelo> PegarPostagemPeloIdAsync(int id)
        {
            return await _context.Postagens
                .Include(p => p.Criador)
                .Include(p => p.Tema)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PostagemModelo>> PegarPostagensPorPesquisaAsync(
            string titulo,
            string descricaoTema,
            string nomeCriador)
        {
            switch (titulo, descricaoTema, nomeCriador)
            {
                case (null, null, null):
                    return await PegarTodasPostagensAsync();

                case (null, null, _):
                    return await _context.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();

                case (null, _, null):
                    return await _context.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Tema.Descricao.Contains(descricaoTema))
                        .ToListAsync();

                case (_, null, null):
                    return await _context.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Titulo.Contains(titulo))
                        .ToListAsync();

                case (_, _, null):
                    return await _context.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                            p.Titulo.Contains(titulo) &
                            p.Tema.Descricao.Contains(descricaoTema))
                        .ToListAsync();

                case (null, _, _):
                    return await _context.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                            p.Tema.Descricao.Contains(descricaoTema) &
                            p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();

                case (_, null, _):
                    return await _context.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                            p.Titulo.Contains(titulo) &
                            p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();

                case (_, _, _):
                    return await _context.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                            p.Titulo.Contains(titulo) |
                            p.Tema.Descricao.Contains(descricaoTema) |
                            p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();
            }
        }

        public async Task NovaPostagemAsync(NovaPostagemDTO postagem)
        {
            await _context.Postagens.AddAsync(new PostagemModelo
            {
                Titulo = postagem.Titulo,
                Descricao = postagem.Descricao,
                Foto = postagem.Foto,
                Criador = _context.Usuarios.FirstOrDefault(u => u.Email == postagem.EmailCriador),
                Tema = _context.Temas.FirstOrDefault(t => t.Descricao == postagem.DescricaoTema)
            });
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarPostagemAsync(AtualizarPostagemDTO postagem)
        {
            var postagemExistente = await PegarPostagemPeloIdAsync(postagem.Id);
            postagemExistente.Titulo = postagem.Titulo;
            postagemExistente.Descricao = postagem.Descricao;
            postagemExistente.Foto = postagem.Foto;
            postagemExistente.Tema = _context.Temas.FirstOrDefault(t => t.Descricao == postagem.DescricaoTema);

            _context.Postagens.Update(postagemExistente);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarPostagemAsync(int id)
        {
            _context.Postagens.Remove(await PegarPostagemPeloIdAsync(id));
            await _context.SaveChangesAsync();
        }

        #endregion Métodos
    }
}