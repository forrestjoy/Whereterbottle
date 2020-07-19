using System.Collections.Generic;
using Whereterbottle.Models;

namespace globals
{
    /*
     *  This file contains all the global variables used throughout the application
     *  All this lists are filled upon successful login
     */

    public class Globals
    {
        public static User user { get; set; }
        public static User friend { get; set; }//contains the information of the user
        public static Bottle userBottle { get; set; }
        public static GlobalVariables globalVariables { get; set; }
        public static Fountain currentFountain { get; set; }
        public static User currentUser { get; set; }
        public static List<User> friendList { get; set; }
        //public static ObservableCollection<ItemModel> recentlyViewedList { get; set; } //contains the complete object for every item in the user's history array
        // public static List<Fountain> favFountList { get; set; } //ids of the favorite items
        public static List<Fountain> allFountList { get; set; } //ids of the favorite items
                                                                //public static ObservableCollection<ItemModel> favoritesItemsList { get; set; } //contains the complete object for every item in the user's favoritesList
                                                                // Utility definitions
    }
}