using System.Windows.Media.Imaging;
using System;

namespace Hackathon.Core
{
    public class Tile
    {
        public BitmapImage Image { get; set; }
        public string Author { get; set; }
        public bool isLiked { get; set; }
        public int numberOfLikes { get; set; }

        public Tile()
        {
   
        }

        public Tile(string imageUrl, string author, int numLikes)
        {
            Image = new BitmapImage();
            Image.BeginInit();
            Image.UriSource = new Uri(imageUrl, UriKind.RelativeOrAbsolute);
            Image.CacheOption = BitmapCacheOption.OnLoad;
            Image.EndInit();

            Author = author;
            isLiked = false;
            numberOfLikes = numLikes;
        }
    }
}
