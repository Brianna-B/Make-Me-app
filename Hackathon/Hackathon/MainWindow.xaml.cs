using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hackathon.Core;
using System.IO;

namespace Hackathon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataHelper Data = new DataHelper();
        SignInHelper signInHelper;
        bool isSignedIn = false;
        CreateAccountHelper accountHelper;

        public MainWindow()
        {
            InitializeComponent();
            Import();
            Data.LoadTiles(TileType.Featured);
            BuildTiles();
        }

        public void BuildTiles()
        {
            var row = 0;
            var column = 0;
            var tileNum = 0;

            var likeButtonImage = new BitmapImage(new Uri("heart_icon_white.png", UriKind.RelativeOrAbsolute));

            foreach (var tile in Data.Tiles)
            {
                var stack = new StackPanel();
                var image = new Image();
                var likeButton = new Image();

                likeButton.Source = likeButtonImage;
                likeButton.Height = 50;
                likeButton.MouseLeftButtonDown += onLikeButtonClick;
                image.Source = tile.Image;

                stack.Margin = new Thickness(50);
                stack.VerticalAlignment = VerticalAlignment.Center;
                stack.HorizontalAlignment = HorizontalAlignment.Center;
                stack.Background = new SolidColorBrush(Colors.DodgerBlue);

                stack.Children.Add(image);
                stack.Children.Add(likeButton);
                stack.Name = $"tile{tileNum++}";

                mainGrid.Children.Add(stack);
                Grid.SetRow(stack, row);
                Grid.SetColumn(stack, column);

                if (column == 0)
                {
                    column = 1;
                }
                else
                {
                    if (row > 4)
                    {
                        break;
                    }

                    column = 0;
                    row++;
                }
            }
        }

        private void onLoginButtonClick(object sender, MouseButtonEventArgs e)
        {
            if(isSignedIn)
            {
                System.Windows.MessageBox.Show("You have successfully logged out.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                isSignedIn = false;
                loginButton.Content = "Sign In";
                return;
            }

            CollapseAll();
            loginPanel.Visibility = Visibility.Visible;
        }
        ///*********************************************************************************/
        private void onConfirmLoginButtonClick(object sender, EventArgs e)
        {
            // Authenticate
            signInHelper = new SignInHelper(usernameBox.Text, passwordBox.Password, Data.Users);
            if (!signInHelper.isValidEntry())
            {
                usernameBox.Clear();
                passwordBox.Clear();
                usernameBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                passwordBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                return;
            }

            if (signInHelper.isValidateUsername())
            {
                if (signInHelper.isValidPassword())
                {
                    CollapseAll();
                    usernameBox.Clear();
                    passwordBox.Clear();

                    isSignedIn = true;
                    loginButton.Content = "Sign Out";

                    mainScroll.Visibility = Visibility.Visible;
                }

                else
                {
                    passwordBox.Clear();
                    passwordBox.Background = new SolidColorBrush(Color.FromRgb(240,152,152));
                    return;
                }
            }

            else
            {
                usernameBox.Clear();
                passwordBox.Clear();
                usernameBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                return;
            }
        }

        private void onLabelMouseOver(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            label.Foreground = new SolidColorBrush(Colors.DodgerBlue);
        }

        private void onLabelMouseLeaveGrey(object sender, MouseEventArgs e)
        {
            var label = (Label)sender;
            label.Foreground = new SolidColorBrush(Color.FromArgb(100, 178, 178, 178));
        }
        private void onLabelMouseLeaveBlack(object sender, MouseEventArgs e)
        {
            var label = (Label)sender;
            label.Foreground = new SolidColorBrush(Colors.Black);
        }
        private void CollapseAll()
        {
            loginPanel.Visibility = Visibility.Collapsed;
            mainScroll.Visibility = Visibility.Collapsed;
            createPanel.Visibility = Visibility.Collapsed;
        }

        private void onLikeButtonClick(object sender, MouseButtonEventArgs e)
        {
            var button = (Image)sender;
            var tile = (StackPanel)button.Parent;
            var tileNum = -1;

            tileNum = int.Parse(tile.Name.Substring(4));

            if (Data.Tiles[tileNum].isLiked)
            {
                button.Source = new BitmapImage(new Uri("heart_icon_white.png", UriKind.Relative));
                Data.Tiles[tileNum].isLiked = false;
            }
            else
            {
                button.Source = new BitmapImage(new Uri("heart_icon.png", UriKind.Relative));
                Data.Tiles[tileNum].isLiked = true;
            }
        }

        private void onCreateAccountClick(object sender, MouseButtonEventArgs e)
        {
            CollapseAll();
            createPanel.Visibility = Visibility.Visible;
        }

        private void onCreateNewAccount(object sender, MouseButtonEventArgs e)
        {
            accountHelper = new CreateAccountHelper(createUserBox.Text, createPassBox.Password, confirmPassBox.Password, emailBox.Text, Data.Users);
            if(!accountHelper.isAccountValid())
            {
                createUserBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                createPassBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                confirmPassBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                emailBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                return;
            }

            else if (!accountHelper.isValidateUsername())
            {
                createUserBox.Clear();
                createPassBox.Clear();
                confirmPassBox.Clear();
                emailBox.Clear();
                createUserBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                return;
            }
            
            else if (!accountHelper.isValidPassword())
            {
                createUserBox.Clear();
                createPassBox.Clear();
                confirmPassBox.Clear();
                emailBox.Clear();
                createPassBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                confirmPassBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                return;
            }
          
            else if (!accountHelper.isValidateEmail())
            {
                createUserBox.Clear();
                createPassBox.Clear();
                confirmPassBox.Clear();
                emailBox.Clear();
                emailBox.Background = new SolidColorBrush(Color.FromRgb(240, 152, 152));
                return;
            }
            
            else
            {
                Data.Users = accountHelper.AddNewUser();
                System.Windows.MessageBox.Show("You have successfully created an account", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                CollapseAll();
                isSignedIn = true;
                loginButton.Content = "Sign Out";
                mainScroll.Visibility = Visibility.Visible;
            }
        }

        private void onPasswordEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                onConfirmLoginButtonClick(sender, EventArgs.Empty);
            }
            else
            {
                passwordBox.Background = new SolidColorBrush(Colors.White);
            }
        }

        private void onLaughClick(object sender, MouseButtonEventArgs e)
        {
            CollapseAll();
        }

        private void Export()
        {
            StreamWriter writer = new StreamWriter("E:\\Steve\\Documents\\visual studio 2015\\Projects\\Hackathon\\Users.txt");
            foreach (var user in Data.Users)
            {
                writer.WriteLine(user.Export());
            }
            writer.Close();   
        }
        private void Import()
        {
            if (File.Exists("E:\\Steve\\Documents\\visual studio 2015\\Projects\\Hackathon\\Users.txt") == true)
            {
                StreamReader reader = new StreamReader("E:\\Steve\\Documents\\visual studio 2015\\Projects\\Hackathon\\Users.txt");
                var line = reader.ReadLine();
                while (line != null)
                {
                    AddUser(line);
                    line = reader.ReadLine();
                }
                reader.Close();
            }
        }
        private void AddUser(string line)
        {
            if (line == null)
            {
                return;
            }

            else
            {
                string[] user = new string[3];
                user = line.Split(' ');
                Data.Users.Add(new UserInfo(user[0], user[1], user[2]));
            }
        }

        private void onClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Export();
        }

        private void onTextInput(object sender, EventArgs e)
        {
            var box = (Control)sender;
            box.Background = new SolidColorBrush(Colors.White);
        }
    }
}
