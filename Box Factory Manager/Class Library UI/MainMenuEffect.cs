using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Class_Library_UI
{
    public class MainMenuEffect
    {
        public static bool MenuIsOpen = true;
        public static bool No_Button = false;
        public static bool Yes_Button = false;


        public static void OpenCloseMenu(Button button1, Button button2, Button button3)
        {
            if(MenuIsOpen==true)
            {
                button1.IsEnabled = false;
                button2.IsEnabled = false;
                button3.IsEnabled = false;

                button1.Opacity = 0;
                button2.Opacity = 0;
                button3.Opacity = 0;

                MenuIsOpen = false;
            }
            else
            {
                button1.IsEnabled = true;
                button2.IsEnabled = true;
                button3.IsEnabled = true;

                button1.Opacity = 100;
                button2.Opacity = 100;
                button3.Opacity = 100;
                MenuIsOpen = true;
            }
        }

        public static void DisableButtons(Button[] buttons,bool enable)
        {
            if (enable == false)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].IsEnabled = false;
                    buttons[i].Opacity = 0;

                }
            }

            if (enable == true)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].IsEnabled = true;
                    buttons[i].Opacity = 100;
                }
            }
        }

        
    }
}
