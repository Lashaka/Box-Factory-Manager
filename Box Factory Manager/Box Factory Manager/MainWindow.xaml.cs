using Class_Library_UI;
using System.Windows;
using System.Windows.Input;
using Wpf_Class_Library_Logic.BoxOverall;

namespace Box_Factory_Manager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Box_Storage_Set.AddDefaultsToData();

            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.ThreeDBorderWindow;

            InitializeComponent();

            SayAtStart();
        }
        public async void SayAtStart()
        {
            TextWriteEffect textWriteEffect = new TextWriteEffect();
            await textWriteEffect.ScreenSay("Press ESC to access the menu!", TextWriteEffect_TextBlock);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainMenuEffect.OpenCloseMenu(Exit_Button, Buy_Button, Manage_Button);
                //open menu or close
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Manage_Button_Click(object sender, RoutedEventArgs e)
        {
            AddWindow Addwindow = new AddWindow();
            Addwindow.Show();
            this.Close();
        }

        private void Buy_Button_Click(object sender, RoutedEventArgs e)
        {
            BuyWindow buyWindow = new BuyWindow();
            buyWindow.Show();
            this.Close();
        }
    }
}
