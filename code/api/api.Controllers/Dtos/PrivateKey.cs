using System.ComponentModel.DataAnnotations;

namespace api.Controllers.Dtos
{
    public class PrivateKey
    {
        [StringLength(32)]
        public string SU { get; set; } = string.Empty;

        [StringLength(32)]
        public string HR { get; set; } = string.Empty;
    }
}
