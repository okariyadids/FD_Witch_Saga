using System.ComponentModel.DataAnnotations;

namespace WitchSaga.Models
{
    public class VillagerModel
    {
        #region properties

        [Required]
        public string Name { get; set; }

        [Required]
        public int AgeOfDeath { get; set; }

        [Required]
        public int YearOfDeath { get; set; }

        #endregion
    }
}
