using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.modelos
{
    [Table("tb_temas")]
    public class TemaModelo
    {
        [Key] //chave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(20)]//para limitar
        public string Descricao { get; set; }

        [JsonIgnore]
        //usar JsonIgnore quando tem bidimensionalidade, pertence a postagem mas não faz mapeamento, ou seja ñ puxa as postagens
        public List<PostagemModelo> PostagensRelacionadas { get; set; }
    }
}
