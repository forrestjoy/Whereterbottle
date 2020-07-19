using System.Collections.Generic;

namespace Whereterbottle.Models
{
    public class FriendGroup : List<Friends>
    {
        public string Title { get; set; }
        public string ShortTitle { get; set; }

        public FriendGroup(string title, string shortTitle)
        {
            Title = title;
            ShortTitle = shortTitle;
        }
    }
}