using System;
using System.Collections.Generic;
using System.Text;
using globals;

namespace Whereterbottle.Utilities
{
    class ModelHandler
    {
        public void wipeUserData()
        {
            Globals.user.id = "";
            Globals.user.firstName = "";
            Globals.user.lastName = "";
            Globals.user.bottleID = "";
            Globals.user.lastFill= "";
            Globals.user.favorites = null;
            Globals.user.friends = null;
            Globals.user.email = "";
            Globals.user.address = "";

        }
        public void wipeUserBottleData()
        {
            Globals.userBottle.id = "";
            Globals.userBottle.size = "";
            Globals.userBottle.totalRefills = 0;
            Globals.userBottle.lastRefillDay = "";
            Globals.userBottle.dayRefills = 0;
            Globals.userBottle.xCoord = "";
            Globals.userBottle.yCoord = "";
        }

        public void wipeFriendData()
        {
            Globals.friend.id = "";
            Globals.friend.firstName = "";
            Globals.friend.lastName = "";
            Globals.friend.bottleID = "";
            Globals.friend.lastFill = "";
            Globals.friend.favorites = null;
            Globals.friend.friends = null;
            Globals.friend.email = "";
            Globals.friend.address = "";

        }
    }
}
