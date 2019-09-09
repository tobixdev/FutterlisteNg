using System;

namespace FutterlisteNg.Shared
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string ShortName { get; set; }

        public UserViewModel(string name, string shortName)
        {
            Name = name;
            ShortName = shortName;
        }
    }
}