using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.modelos
{
    [Table("tb_usuario")]
    public class UsuarioModelo
    {
        [Key] //chave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //coluna id

        [Required, StringLength(50)]//para limitar
        public string Name { get; set; }//agora VOU COLOCAR os atributos

        [Required, StringLength(30)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Senha { get; set; }

        public string Foto { get; set; }

        [JsonIgnore]
        //usar JsonIgnore quando tem bidimensionalidade, pertence a postagem mas não faz mapeamento, ou seja ñ puxa as postagens
        public List<PostagemModelo> MinhasPostagens { get; set; }
        public string Nome { get; set; }
    }
}
