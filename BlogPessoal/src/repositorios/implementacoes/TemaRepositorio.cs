using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.repositorios.implementacoes
{
    public class TemaRepositorio : ITema
    {
        #region Atributos

        private readonly BlogPessoalContext _context;

        #endregion Atributos


        #region Construtores

        public TemaRepositorio(BlogPessoalContext context)
        {
            _context = context;
        }

        #endregion Construtores


        #region Métodos

        public async Task<List<TemaModelo>> PegarTodosTemasAsync()
        {
            return await _context.Temas.ToListAsync();
        }

        public async Task<TemaModelo> PegarTemaPeloIdAsync(int id)
        {
            return await _context.Temas.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TemaModelo>> PegarTemasPelaDescricaoAsync(string descricao)
        {
            return await _context.Temas
                            .Where(u => u.Descricao.Contains(descricao))
                            .ToListAsync();
        }

        public async Task NovoTemaAsync(NovoTemaDTO tema)
        {
            await _context.Temas.AddAsync(new TemaModelo
            {
                Descricao = tema.Descricao
            });

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarTemaAsync(AtualizarTemaDTO tema)
        {
            var temaExistente = await PegarTemaPeloIdAsync(tema.Id);
            temaExistente.Descricao = tema.Descricao;
            _context.Temas.Update(temaExistente);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarTemaAsync(int id)
        {
            _context.Temas.Remove(await PegarTemaPeloIdAsync(id));
            await _context.SaveChangesAsync();
        }

        #endregion Métodos
    }
}