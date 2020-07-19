namespace Whereterbottle.Models
{
    public enum MenuItemType
    {
        Profile,
        Bottle,
        FavoriteFountains,
        About,
        VirtualBottle,
        FountainReview
    }

    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}