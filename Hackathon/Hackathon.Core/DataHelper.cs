using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Core
{
    public enum TileList
    {
        Featured,
        Laugh,
        Smile,
        Aww,
        Hungry
    }

    class DataHelper
    {
        public List<UserInfo> Users;
        //public List<ImageData> Images;

        public List<Tile> Tiles;

        public void LoadTiles(TileList type)
        {

            Tiles.Add(new Tile("art.jpg", "steve", 0));
            Tiles.Add(new Tile("art2.jpg", "steve", 0));
            Tiles.Add(new Tile("guy_fieri.jpg", "maggie", 0));
            Tiles.Add(new Tile("forest.jpg", "maggie", 0));
            Tiles.Add(new Tile("house.jpg", "maggie", 0));
        }
    }
}
