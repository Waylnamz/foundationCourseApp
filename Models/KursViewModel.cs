using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class KursViewModel
    {
        //id => Primary Key
        [Key]
        public int KursId { get; set; }
        [Required(ErrorMessage ="Başlık boş bırakılamaz!")]

        public string? Baslik{ get; set; }

        public int OgretmenId { get; set; }

        public ICollection<KursKayit> KursKayitlari { get; set; } =new List<KursKayit>();
    }


}