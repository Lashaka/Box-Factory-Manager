using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Class_Library_UI
{
    public class TextWriteEffect
    {
        private const int TextSplitByNumber = 120;

        public int leftovercalc(int stringlength, int currentcount, int leftover)
        {
            if (stringlength - (currentcount * TextSplitByNumber) < TextSplitByNumber)
            {
                return (TextSplitByNumber * (currentcount)) + leftover;
            }
            return TextSplitByNumber * (currentcount + 1);
        }

        public async Task ScreenSay(string text, TextBlock textblock, Button[] buttons = null, bool showbuttons = false) // fills text in the window and says a specific text
        {
            if (buttons != null)
            {
                MainMenuEffect.DisableButtons(buttons, showbuttons);

            }
            //when happens disable add book
            textblock.Text = "";
            textblock.Background = Brushes.White;
            textblock.Foreground = Brushes.Black;


            char[] characters = text.ToCharArray(); // converts all text to characters

            if (characters.Length > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();

                //calculating string lenghts to know how many text events needed
                int CutStringToIntPieces = characters.Length / TextSplitByNumber;

                int LeftOver = characters.Length % TextSplitByNumber;

                for (int i = 0; i < CutStringToIntPieces + 1; i++)
                {
                    int counter = 0;
                    for (int j = i * TextSplitByNumber; j < leftovercalc(characters.Length, i, LeftOver); j++)
                    {
                        stringBuilder.Append(characters[j]);
                        textblock.Text = stringBuilder.ToString();
                        await Task.Delay(100);
                        counter++;
                    }
                    await Task.Delay(500);
                    stringBuilder.Remove(0, counter);

                }

                textblock.Text = "";
                textblock.Background = null;
                await Task.Delay(1000);



            }
            if (buttons != null)
            {
                MainMenuEffect.DisableButtons(buttons, true);
            }


        }
    }
}
