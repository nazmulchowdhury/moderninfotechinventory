using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Model.User
{
    [Table("AspNetUsers")]
    [Serializable]
    public class UserEntity
    {
        [Key]
        public string Id { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string UserName { get; set; }
    }
}
