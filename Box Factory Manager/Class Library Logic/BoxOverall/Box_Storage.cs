using System;

namespace Wpf_Class_Library_Logic.BoxOverall
{
    public class Box_Storage : IComparable<Box_Storage> //Class that holds boxes of each type and all info about them, ????static because no new instance of this class is needed when used?????
    {
        private readonly Box _Box; //the box it holds,once set cannot be changed
        private const int _MaxBoxes = 100; //max amount of boxes for all is 100
        private int _CurrentAmountOfBoxes = 0; //starts at 0, everytime this box is added adds +X to the current amount
        private readonly string _FirstAddedDate; //date when first added
        private string _LastPurchasedDate; //date when last bought
        public int _AmountToAdd; //amount to add the first time when set

        public DateTime _LastBoughtRealTime;
        public static bool AddingRandom = false;


        public Box_Storage(int x1, int x2, int y, int amountToAdd)
        {
            Box LocalBox = new Box(x1, x2, y);
            _Box = LocalBox;



            _AmountToAdd = amountToAdd;

            if(AddingRandom==true)
            {
                _LastBoughtRealTime = SetRandomPurchaseDate();
                _LastPurchasedDate = _LastBoughtRealTime.ToString();

                TimeSpan timeago = new TimeSpan(100, 0, 0); //random -hours

                _FirstAddedDate = (SetRandomPurchaseDate() - timeago).ToString();

            }
            else
            {
                _LastPurchasedDate = "Not Purchased Yet";
                _FirstAddedDate = getDateString();
            }


        }

       static Random rand = new Random();

        public static DateTime SetRandomPurchaseDate()
        {
            DateTime Now = DateTime.Now;
            int number = rand.Next(0, 400);
            TimeSpan MinusRandom = new TimeSpan(-number, 0, 0); //random -hours
            DateTime RandomTime = Now.Add(MinusRandom);

            return RandomTime;
        }
        private static string getDateString() //returns date
        {
            DateTime Now = DateTime.Now;
            return Now.ToString();
        }

        public Box GetBox()
        {
            return _Box;
        }

        public int Width1
        {
            get { return _Box.Boxx1; }
        }
        public int Width2
        {
            get { return _Box.Boxx2; }
        }
        public int Height
        {
            get { return _Box.Boxy; }
        }
        public int BoxAmount
        {
            get { return _CurrentAmountOfBoxes; }
            set { _CurrentAmountOfBoxes = value; }
        }
        public int BoxToAdd
        {
            set { _AmountToAdd = value; }
        }

        public string FirstAdded
        {
            get { return _FirstAddedDate.ToString(); }
        }

        public string LastPurchased
        {
            get { return _LastPurchasedDate; }
            set { _LastPurchasedDate = value; }

        }

 

        public int Compare(Box_Storage x, Box_Storage y)
        {
            if (x._Box.Boxx1 == y._Box.Boxx1 && x._Box.Boxx2 == y._Box.Boxx2 && x._Box.Boxy == y._Box.Boxy)
            {
                return 0;
            }
            else if (x._Box.Boxx1 * x._Box.Boxx2 * x._Box.Boxy > y._Box.Boxx1 * y._Box.Boxx2 * y._Box.Boxy)
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }

        public int CompareTo(Box_Storage other)
        {
            if (this._Box.Boxx1 == other._Box.Boxx1 && this._Box.Boxx2 == other._Box.Boxx2 && this._Box.Boxy == other._Box.Boxy)
            {
                return 0;
            }
            else if (this._Box.Boxx1 * this._Box.Boxx2 * this._Box.Boxy > other._Box.Boxx1 * other._Box.Boxx2 * other._Box.Boxy)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }







    }
}
