using System;
using System.ComponentModel.DataAnnotations;

namespace FutterlisteNg.Shared
{
    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(5)]
        public string ShortName { get; set; }

        public UserViewModel()
        {
        }

        public UserViewModel(string name, string shortName)
        {
            Name = name;
            ShortName = shortName;
        }
    }
}