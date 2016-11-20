using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Core
{
    public class DataHelper
    {
        public List<UserInfo> Users;
        public List<string> Images;

        public List<Tile> Tiles;

        public DataHelper()
        {
            Users = new List<UserInfo>();
            Tiles = new List<Tile>();
            Images = new List<string>();

            Images.Add("forest.jpg");
            Images.Add("house.jpg");
            Images.Add("art.jpg");
            Images.Add("art2.jpg");
            Images.Add("guy_fieri.jpg");
        }

        public void LoadTiles(TileType type)
        {
            foreach(var path in Images)
            {
                Tiles.Add(new Tile(path, "", 0));
            }
        }
    }
}
