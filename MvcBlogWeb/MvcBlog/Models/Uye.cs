namespace MvcBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Uye")]
    public partial class Uye
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Uye()
        {
            Makales = new HashSet<Makale>();
            Yorums = new HashSet<Yorum>();
        }

        public int Uyeid { get; set; }

        [Required(ErrorMessage = "Kullanýcý adý boþ býrakýlamaz.")]
        [StringLength(50)]
        public string KullaniciAdi { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Þifre alaný boþ býrakýlamaz.")]
        [StringLength(20)]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Ad Soyad alaný boþ býrakýlamaz.")]
        [StringLength(50)]
        public string AdSoyad { get; set; }

        
        public int? Yetkiid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Makale> Makales { get; set; }

        public virtual Yetki Yetki { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorums { get; set; }
    }
}
