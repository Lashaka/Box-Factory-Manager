using Class_Library___DBmanager;
using Class_Library_Logic.BoxOverall;
using Class_Library_UI;
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
using Wpf_Class_Library_Logic.BoxOverall;

namespace Box_Factory_Manager
{
    public partial class AddWindow : Window
    {

        public AddWindow()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.ThreeDBorderWindow;

            InitializeComponent();
            MainMenuEffect.MenuIsOpen = false;

            MainGrid.Height = SystemParameters.PrimaryScreenHeight;
            MainGrid.Width = SystemParameters.PrimaryScreenWidth;
            BoxesDataGrid.Width = SystemParameters.PrimaryScreenWidth;

            //   BoxesDataGrid.ItemsSource = Box_Storage_Functions.SortDataBySize().Values.ToList();
            BoxesDataGrid.ItemsSource = Box_Storage_Set.Box_Storagess_SortedDictionary.Values.ToList();


        }

        public Button[] ReturnsButtons()
        {
            Button[] buttons = { Add_Button, Remove_Button, Remove_Button_Copy,Search_Button,Reset_Button };
            return buttons;

        }

        public async void GlobalTextChange(TextBox textBox, int number)  //Func for textchange
        {
            if (int.TryParse(textBox.Text, out int n) == false || int.Parse(textBox.Text) > number)
            {
                try
                {
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1, 1);

                }
                catch
                {
                    textBox.Text = "";
                }

                if (TextWriteEffect_TextBlock.Text == "")
                {
                    TextWriteEffect textWriteEffect = new TextWriteEffect();
                    await textWriteEffect.ScreenSay("Please Enter Numbers up to " + number, TextWriteEffect_TextBlock, ReturnsButtons(), false);
                }

            }
        }

        private void TextBox_x1_TextChanged(object sender, TextChangedEventArgs e)
        {
            GlobalTextChange(TextBox_x1, 100);
        }

        private void TextBox_x2_TextChanged(object sender, TextChangedEventArgs e)
        {
            GlobalTextChange(TextBox_x2, 100);
        }

        private void TextBox_y_TextChanged(object sender, TextChangedEventArgs e)
        {
            GlobalTextChange(TextBox_y, 100);
        }

        private void TextBox_NumOfBoxes_TextChanged(object sender, TextChangedEventArgs e)
        {
            GlobalTextChange(TextBox_NumOfBoxes, 100);
        }
        private void TextBox_Expire_TextChanged(object sender, TextChangedEventArgs e)
        {
            GlobalTextChange(TextBox_Expire, 1000);
        }

        private async void Add_Button_Click(object sender, RoutedEventArgs e) //box adding
        {
            int sample;
            if (int.TryParse(TextBox_x1.Text, out sample) == true && int.TryParse(TextBox_x2.Text, out sample) == true && int.TryParse(TextBox_y.Text, out sample) == true && int.TryParse(TextBox_NumOfBoxes.Text, out sample) == true)
            {
                Box_Storage box = new Box_Storage(int.Parse(TextBox_x1.Text), int.Parse(TextBox_x2.Text), int.Parse(TextBox_y.Text), int.Parse(TextBox_NumOfBoxes.Text));
                Box_Storage_Functions.AddBoxToStorage(box, Box_Storage_Set.Box_Storagess_SortedDictionary);

                BoxesDataGrid.ItemsSource = Box_Storage_Set.Box_Storagess_SortedDictionary.Values.ToList();

                if (Box_Storage_Functions.ProcExtraBoxMessage == true)
                {
                    TextWriteEffect textWriteEffect = new TextWriteEffect();
                    await textWriteEffect.ScreenSay("The factory cannot store more than 100 types of these boxes, " + Box_Storage_Functions.TempExtraBoxes + " Boxes will be returned to the supplier.", TextWriteEffect_TextBlock, ReturnsButtons(), false);

                    Box_Storage_Functions.ProcExtraBoxMessage = false;
                }

            }
            else
            {
                TextWriteEffect textWriteEffect = new TextWriteEffect();
                await textWriteEffect.ScreenSay("Please Enter Legitimate Numbers.", TextWriteEffect_TextBlock, ReturnsButtons(), false);
            }


        }

        private async void Remove_Button_Click(object sender, RoutedEventArgs e)//specific boxes removal
        {
            try
            {
                Box_Storage box = new Box_Storage(int.Parse(TextBox_x1.Text), int.Parse(TextBox_x2.Text), int.Parse(TextBox_y.Text), int.Parse(TextBox_NumOfBoxes.Text));
                Box_Storage_Functions.RemoveBoxFromStorage(box, Box_Storage_Set.Box_Storagess_SortedDictionary);

                BoxesDataGrid.ItemsSource = Box_Storage_Set.Box_Storagess_SortedDictionary.Values.ToList();

                if (Box_Storage_Functions.ProcMaxDeleteMessage == true)
                {
                    TextWriteEffect textWriteEffect = new TextWriteEffect();
                    await textWriteEffect.ScreenSay("The factory cannot remove more boxes than the amount of boxes that exist, all boxes have been removed.", TextWriteEffect_TextBlock, ReturnsButtons(), false);

                    Box_Storage_Functions.ProcMaxDeleteMessage = false;
                }
                else if (Box_Storage_Functions.ProcBoxDoesntExist == true)
                {
                    TextWriteEffect textWriteEffect = new TextWriteEffect();
                    await textWriteEffect.ScreenSay("The box you wish to remove does not exist. ", TextWriteEffect_TextBlock, ReturnsButtons(), false);

                    Box_Storage_Functions.ProcBoxDoesntExist = false;

                }
            }
            catch
            {
                TextWriteEffect textWriteEffect = new TextWriteEffect();
                await textWriteEffect.ScreenSay("Please enter legitimate numbers. ", TextWriteEffect_TextBlock, ReturnsButtons(), false);

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

        private void Buy_Buttonpage_Click(object sender, RoutedEventArgs e)
        {
            BuyWindow buyWindow = new BuyWindow();
            buyWindow.Show();
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)//menu control
        {
            if (e.Key == Key.Escape)
            {
                MainMenuEffect.OpenCloseMenu(Exit_Button, Buy_Buttonpage, Manage_Button);
                //open menu or close
            }
        }
        private async void Search_Button_Click(object sender, RoutedEventArgs e) //shows expired search
        {
            int ExpireDays = 0;

            try
            {
                ExpireDays = int.Parse(TextBox_Expire.Text);

                SortedDictionary<Box_Storage, Box_Storage> Box_Storagess_SortedDictionary_TEMP = new SortedDictionary<Box_Storage, Box_Storage>(new KeyComparer());

                foreach (Box_Storage storage in Box_Storage_Set.Box_Storagess_SortedDictionary.Values)
                {
                    storage.BoxToAdd = 0;
                    Box_Storage_Functions.AddBoxToStorage(storage, Box_Storagess_SortedDictionary_TEMP);
                }

                Box_Storagess_SortedDictionary_TEMP = Box_Storage_Functions.ExpiredFunc(Box_Storagess_SortedDictionary_TEMP, ExpireDays, true);

                BoxesDataGrid.ItemsSource = Box_Storagess_SortedDictionary_TEMP.Values.ToList();
            }
            catch
            {
                TextWriteEffect textWriteEffect = new TextWriteEffect();
                await textWriteEffect.ScreenSay("Please Enter a number.", TextWriteEffect_TextBlock, ReturnsButtons(), false);
            }

           

        }
        private void Reset_Button_Click(object sender, RoutedEventArgs e) //shows expired search
        {
            BoxesDataGrid.ItemsSource = Box_Storage_Set.Box_Storagess_SortedDictionary.Values.ToList();
        }


        private async void Remove_Button_Copy_Click(object sender, RoutedEventArgs e) //expired removal
        {
            int ExpireDays = 0;

            try
            {
                ExpireDays = int.Parse(TextBox_Expire.Text);

                Box_Storage_Set.Box_Storagess_SortedDictionary = Box_Storage_Functions.ExpiredFunc(Box_Storage_Set.Box_Storagess_SortedDictionary, ExpireDays, false);
                BoxesDataGrid.ItemsSource = Box_Storage_Set.Box_Storagess_SortedDictionary.Values.ToList();

                if (Box_Storage_Functions.TempExtraBoxes > 0)
                {
                    TextWriteEffect textWriteEffect = new TextWriteEffect();
                    await textWriteEffect.ScreenSay("We removed " + Box_Storage_Functions.TempExtraBoxes + " expired boxes.", TextWriteEffect_TextBlock, ReturnsButtons(), false);

                }
                else
                {
                    TextWriteEffect textWriteEffect = new TextWriteEffect();
                    await textWriteEffect.ScreenSay("No expired boxes detected. ", TextWriteEffect_TextBlock, ReturnsButtons(), false);

                }
                Box_Storage_Functions.TempExtraBoxes = 0;
            }
            catch
            {
                TextWriteEffect textWriteEffect = new TextWriteEffect();
                await textWriteEffect.ScreenSay("Please Enter a number.", TextWriteEffect_TextBlock, ReturnsButtons(), false);
            }

          


        }
    }
}
