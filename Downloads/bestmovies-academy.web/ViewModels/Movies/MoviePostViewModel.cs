using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bestmovies_academy.web.ViewModels.Movies
{
    public class MoviePostViewModel
    {
        [Required(ErrorMessage = "Name must entered")]
        [DisplayName("Movie")]
        public string Name { get; set; }
         [Required(ErrorMessage = "Category must entered")]
         [DisplayName("Genre")]
         public string Category { get; set; }
          [Required(ErrorMessage = "Release must entered")]
          [DisplayName("Release")]
         public string Release { get; set; }
          [Required(ErrorMessage = "Description must entered")]
          [DisplayName("Description")]

         public string ImageUrl { get; set; }
          public string Description { get; set; }
    }
}