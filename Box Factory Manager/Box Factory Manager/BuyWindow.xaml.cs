using Class_Library_Logic.BoxOverall;
using Class_Library_UI;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf_Class_Library_Logic.BoxOverall;

namespace Box_Factory_Manager
{
    public partial class BuyWindow : Window
    {
        public BuyWindow()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.ThreeDBorderWindow;

            InitializeComponent();
            MainMenuEffect.MenuIsOpen = false;

            MainGrid.Height = SystemParameters.PrimaryScreenHeight;
            MainGrid.Width = SystemParameters.PrimaryScreenWidth;
            BoxesDataGrid.Width = SystemParameters.PrimaryScreenWidth;

            BoxesDataGrid.ItemsSource = Box_Storage_Set.Box_Storagess_SortedDictionary.Values.ToList();
        }

        public Button[] ReturnsButtons()
        {
            Button[] buttons = { Buy_Button };
            return buttons;

        }

        public async void GlobalTextChange(TextBox textBox)
        {
            if (int.TryParse(textBox.Text, out int n) == false || int.Parse(textBox.Text) > 100)
            {
                try
                {
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1, 1);

                }
                catch
                {
                    textBox.Text = "";
                }

                TextWriteEffect textWriteEffect = new TextWriteEffect();

                if (TextWriteEffect_TextBlock.Text == "")
                {
                    await textWriteEffect.ScreenSay("Please Enter Numbers up to 100", TextWriteEffect_TextBlock, ReturnsButtons(), true);
                }
            }
        }

        private void TextBox_x1_TextChanged(object sender, TextChangedEventArgs e)
        {
            GlobalTextChange(TextBox_x1);
        }

        private void TextBox_x2_TextChanged(object sender, TextChangedEventArgs e)
        {
            GlobalTextChange(TextBox_x2);
        }

        private void TextBox_y_TextChanged(object sender, TextChangedEventArgs e)
        {
            GlobalTextChange(TextBox_y);
        }

        private void TextBox_NumOfBoxes_TextChanged(object sender, TextChangedEventArgs e)
        {
            GlobalTextChange(TextBox_NumOfBoxes);
        }


        private async void Buy_Button_Click(object sender, RoutedEventArgs e)
        {
            //Box_Storage box = new Box_Storage(int.Parse(TextBox_x1.Text), int.Parse(TextBox_x2.Text), int.Parse(TextBox_y.Text), int.Parse(TextBox_NumOfBoxes.Text));

            //BoxesDataGrid.ItemsSource = Box_Storage_Functions.SortDataByGiftRequirements(box).Values.ToList();
            int sample;

            if (int.TryParse(TextBox_x1.Text, out sample) == true && int.TryParse(TextBox_x2.Text, out sample) == true && int.TryParse(TextBox_y.Text, out sample) == true && int.TryParse(TextBox_NumOfBoxes.Text, out sample) == true)
            {
                Box_Storage box = new Box_Storage(int.Parse(TextBox_x1.Text), int.Parse(TextBox_x2.Text), int.Parse(TextBox_y.Text), int.Parse(TextBox_NumOfBoxes.Text));

                Box_Storage BestBox = Box_Storage_Functions.FindMostSuitableBox(box, Box_Storage_Set.Box_Storagess_SortedDictionary); // gets best box

                if (BestBox != null)
                {
                    BestBox._AmountToAdd = box._AmountToAdd;

                }


                //reduce and update amount of boxes after purchase



                bool ContinueFlag = true; //continue or stop?

                Button[] buttons = new Button[2]; //settings yes/no buttons
                buttons[0] = Yes_Button;
                buttons[1] = No_Button;

                if (Box_Storage_Functions.ProcBoxFoundTooBig == true)
                {
                    TextWriteEffect textWriteEffect = new TextWriteEffect();
                    await textWriteEffect.ScreenSay("Sorry, the most suitable box is 50% bigger than your box volume which makes it unsuitable.", TextWriteEffect_TextBlock, ReturnsButtons(), false);
                    ContinueFlag = false;
                }
                else if (BestBox == null)
                {
                    TextWriteEffect textWriteEffect = new TextWriteEffect();
                    await textWriteEffect.ScreenSay("Sorry, we could not find a box that will suit your needs.", TextWriteEffect_TextBlock, ReturnsButtons(), false);
                    ContinueFlag = false;
                }



                if (Box_Storage_Functions.ProcAreYouSureSizeMessage == true && ContinueFlag == true) //are you sure you want to buy not the exact box?
                {
                    MainMenuEffect.DisableButtons(buttons, true);

                    TextWriteEffect textWriteEffect = new TextWriteEffect();
                    await textWriteEffect.ScreenSay("We dont have the size you asked for, best size is " + BestBox.Width1 + "*" + BestBox.Width2 + "*" + BestBox.Height + ", Do you still want it?", TextWriteEffect_TextBlock, ReturnsButtons(), false);

                    while (MainMenuEffect.Yes_Button == false && MainMenuEffect.No_Button == false)
                    {
                        await textWriteEffect.ScreenSay("Waiting . . . ", TextWriteEffect_TextBlock, ReturnsButtons(), false);
                    }

                    if (MainMenuEffect.Yes_Button == true)
                    {
                        MainMenuEffect.Yes_Button = false;
                    }
                    else if (MainMenuEffect.No_Button == true)
                    {
                        ContinueFlag = false;

                        MainMenuEffect.No_Button = false;
                    }

                    Box_Storage_Functions.ProcAreYouSureSizeMessage = false;
                    MainMenuEffect.DisableButtons(buttons, false);

                }


                if (Box_Storage_Functions.ProcAreYouSureNotEnoughMessage == true && ContinueFlag == true) //are you sure you want to buy not the amount of boxes?
                {
                    MainMenuEffect.DisableButtons(buttons, true);

                    TextWriteEffect textWriteEffect = new TextWriteEffect();
                    await textWriteEffect.ScreenSay("We dont have enough boxes, do you wish to buy all the boxes of this size we have?", TextWriteEffect_TextBlock, ReturnsButtons(), false);

                    while (MainMenuEffect.Yes_Button == false && MainMenuEffect.No_Button == false)
                    {
                        await textWriteEffect.ScreenSay("Waiting . . . ", TextWriteEffect_TextBlock, ReturnsButtons(), false);
                    }

                    if (MainMenuEffect.Yes_Button == true)
                    {
                        MainMenuEffect.Yes_Button = false;
                    }
                    else if (MainMenuEffect.No_Button == true)
                    {
                        ContinueFlag = false;

                        MainMenuEffect.No_Button = false;
                    }

                    Box_Storage_Functions.ProcAreYouSureNotEnoughMessage = false;
                    MainMenuEffect.DisableButtons(buttons, false);


                }
                if (ContinueFlag == true)
                {

                    TextWriteEffect textWriteEffect = new TextWriteEffect();

                    Box_Storage_Set.Box_Storagess_SortedDictionary[BestBox].LastPurchased = Box_Storage_Functions.UpdatePurchaseDate();
                    Box_Storage_Set.Box_Storagess_SortedDictionary[BestBox]._LastBoughtRealTime = Box_Storage_Functions.UpdatePurchaseDateRealTime();

                    Box_Storage_Functions.RemoveBoxFromStorage(BestBox, Box_Storage_Set.Box_Storagess_SortedDictionary);

                    BoxesDataGrid.ItemsSource = Box_Storage_Set.Box_Storagess_SortedDictionary.Values.ToList();
                    await textWriteEffect.ScreenSay("Your purchase was accepted!", TextWriteEffect_TextBlock, ReturnsButtons(), false);





                }
                else if (ContinueFlag == false)
                {
                    TextWriteEffect textWriteEffect = new TextWriteEffect();
                    await textWriteEffect.ScreenSay("Your Purchase proccess has stopped.", TextWriteEffect_TextBlock, ReturnsButtons(), false);
                }

                if (Box_Storage_Functions.ProcNoBoxesOfThisKindLeft == true)
                {
                    TextWriteEffect textWriteEffect = new TextWriteEffect();
                    await textWriteEffect.ScreenSay("No boxes of this kind are left in the factory!", TextWriteEffect_TextBlock, ReturnsButtons(), false);

                    Box_Storage_Functions.ProcNoBoxesOfThisKindLeft = false;
                    Box_Storage_Functions.ProcLessThan10BoxesLeft = false;
                }
                else if (Box_Storage_Functions.ProcLessThan10BoxesLeft == true)
                {
                    TextWriteEffect textWriteEffect = new TextWriteEffect();
                    await textWriteEffect.ScreenSay("Less than 10 boxes of this kind are left in the factory!", TextWriteEffect_TextBlock, ReturnsButtons(), false);

                    Box_Storage_Functions.ProcLessThan10BoxesLeft = false;
                }


            }
            else
            {
                TextWriteEffect textWriteEffect = new TextWriteEffect();
                await textWriteEffect.ScreenSay("Fields Cannot Be Empty.", TextWriteEffect_TextBlock, ReturnsButtons(), false);
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainMenuEffect.OpenCloseMenu(Exit_Button, Buy_Buttonpage, Manage_Button);
                //open menu or close
            }
        }

        private void Yes_Button_Click(object sender, RoutedEventArgs e)
        {
            MainMenuEffect.Yes_Button = true;

            Yes_Button.IsEnabled = false;
            Yes_Button.Opacity = 0;
            No_Button.IsEnabled = false;
            No_Button.Opacity = 0;
        }

        private void No_Button_Click(object sender, RoutedEventArgs e)
        {
            MainMenuEffect.No_Button = true;

            Yes_Button.IsEnabled = false;
            Yes_Button.Opacity = 0;
            No_Button.IsEnabled = false;
            No_Button.Opacity = 0;
        }
    }
}
