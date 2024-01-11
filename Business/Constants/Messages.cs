using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "ürün eklendi ";
        public static string ProductNameInvalid = "ürün ismi geçersiz ";
        public static string MaintenanceTime="Sistem bakımda";
        public static string ProductsListed="ürünler Listelendi ";
        public static string ProductOfCategoryError="en fazla aynı katagoride 10 ürün olabilir";
        public static string ProductNameAlredyExist="bu isim çoktan verildi";
        public static string CategoryLimitExeded="Kategori Limiti Aşıldı yeni ürün eklenemiyor";
        public static string? AuthorizationDenied="yetkiniz yok ";
        public static string UserRegistered = "Kayıtlı kullanıcı";
        public  static string  UserNotFound="Kullanıcı Yok ";
        public static string PasswordError="Şifre Hatası ";
        public static string SuccessfulLogin="giriş Başarılı ";
        internal static string UserAlreadyExists="Kullanıcı oluşturuldu ";
        internal static string AccessTokenCreated="erişim eklemdi ";
    }
}
