using System;

namespace Wpf_Class_Library_Logic.BoxOverall
{
    public class Box : IComparable //class that creates and defines every type of Box
    {
        private int _X1; //Box Width 1
        private int _X2; //Box Width 2
        private int _Y; //Box Height

        public Box(int x1, int x2, int y)
        {
            _X1 = x1;
            _X2 = x2;
            _Y = y;

        }

        public int Boxx1
        {
            get { return _X1; }
            set { _X1 = value; }
        }
        public int Boxx2
        {
            get { return _X2; }
            set { _X2 = value; }
        }
        public int Boxy
        {
            get { return _Y; }
            set { _Y = value; }
        }



        public int CompareTo(Box obj)
        {
            if (this.Boxx1 == obj.Boxx1 && this.Boxx2 == obj.Boxx2 && this.Boxy == obj.Boxy)
            {
                return 0;
            }
            else if (this.Boxx1 * this.Boxx2 * this.Boxy > obj.Boxx1 * obj.Boxx2 * obj.Boxy)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return (_X1 + " " + _X2 + " " + _Y);
        }
    }
}
