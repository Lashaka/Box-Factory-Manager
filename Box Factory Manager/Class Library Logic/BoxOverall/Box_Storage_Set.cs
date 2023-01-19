using Class_Library_Logic.BoxOverall;
using System;
using System.Collections.Generic;

namespace Wpf_Class_Library_Logic.BoxOverall
{
    public class Box_Storage_Set
    {
        public static int CompareMethod = 2;

        // public static HashSet<Box_Storage> box_Storages = new HashSet<Box_Storage>(); //using hashset because it has a good runtime when using (contains) - O(1)

        //   public static SortedDictionary<string, Box_Storage> Box_Storagess_SortedDictionary = DataBaseService.GetData();

        public static SortedDictionary<Box_Storage, Box_Storage> Box_Storagess_SortedDictionary = new SortedDictionary<Box_Storage, Box_Storage>(new KeyComparer());

        private static readonly Random random = new Random();

        public static void AddDefaultsToData()
        {
            Box_Storage.AddingRandom = true;

            Random rand = new Random();
            for (int i = 0; i < 30; i++)
            {
                Box_Storage box = new Box_Storage(random.Next(1, 100), random.Next(1, 100), random.Next(1, 100), random.Next(1, 100));
                Box_Storage_Functions.AddBoxToStorage(box, Box_Storagess_SortedDictionary);
            }

            Box_Storage.AddingRandom = false;

        }

    }

    public class KeyComparer : IComparer<Box_Storage>
    {

        public int Compare(Box_Storage x, Box_Storage y)
        {
            //if (Box_Storage_Set.CompareMethod == 1)   // useless compare to be deleted later
            //{
            //    if(x.Width1 == y.Width1 && x.Width2 == y.Width2 || x.Width1 == y.Width2 && x.Width2 == y.Width1)
            //    {
            //        if(x.Height== y.Height)
            //        {
            //            return 0;
            //        }
            //    }
            //    else if (x.Width1 * 100 + x.Width2 * 10 + x.Height * 1 > y.Width1 * 100 + y.Width2 * 10 + y.Height * 1)
            //    {
            //        return 1;
            //    }
            //    else if (x.Width1 * 100 + x.Width2 * 10 + x.Height * 1 < y.Width1 * 100 + y.Width2 * 10 + y.Height * 1)
            //    {
            //        return -1;
            //    }
            //    else
            //    {
            //        return 0;
            //    }
            //}
            if (Box_Storage_Set.CompareMethod == 2) //compares by box volume
            {
                if (x.Width1 == y.Width1 && x.Width2 == y.Width2 || x.Width1 == y.Width2 && x.Width2 == y.Width1)
                {
                    if (x.Height == y.Height)
                    {
                        return 0;
                    }
                }
                else if (x.Width1 * x.Width2 * x.Height > y.Width1 * y.Width2 * y.Height)
                {
                    return 1;
                }
                else if (x.Width1 * x.Width2 * x.Height < y.Width1 * y.Width2 * y.Height)
                {
                    return -1;
                }
                else if (x.Width1 == y.Width1 && x.Width2 == y.Width2 && x.Height == y.Height)
                {
                    return 0;
                }
            }
            else if (Box_Storage_Set.CompareMethod == 3) //smart compare for specific search
            {
                if (x.Width1 == y.Width1 && x.Width2 == y.Width2 || x.Width1 == y.Width2 && x.Width2 == y.Width1) //if both same, compare heights and keep smaller height first. Works
                {
                    if (x.Height > y.Height)
                    {
                        return 1;
                    }
                    else if (x.Height < y.Height)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (x.Width1 > y.Width1 && x.Width2 > y.Width2 || x.Width1 > y.Width2 && x.Width2 > y.Width1) // if both x's bigger => push to end Works
                {
                    return 1;
                }
                else if (x.Width1 < y.Width1 && x.Width2 < y.Width2 || x.Width1 < y.Width2 && x.Width2 < y.Width1) // if both x's smaller => push to start Works
                {
                    return -1;
                }

                else if (x.Width1 > y.Width1 && x.Width2 < y.Width2 || x.Width1 > y.Width2 && x.Width2 < y.Width1) //if kinda balanced
                {
                    if (x.Width1 - y.Width1 > y.Width2 - x.Width2 || x.Width1 - y.Width2 > y.Width2 - x.Width1) //check more specificaly
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }


                else if (x.Width1 < y.Width1 && x.Width2 > y.Width2 || x.Width1 < y.Width2 && x.Width2 > y.Width1) //if kinda balanced
                {
                    if (y.Width1 - x.Width1 > x.Width2 - y.Width2 || y.Width1 - x.Width2 > x.Width2 - y.Width1)  //check more specificaly
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                return 0;
                //|| x.Width1 > y.Width1 && x.Width2 < y.Width2

            }
            else if (Box_Storage_Set.CompareMethod == 4) //method that puts earliest box buying date first
            {
                if (x._LastBoughtRealTime != null && y._LastBoughtRealTime != null)
                {
                    if (x._LastBoughtRealTime > y._LastBoughtRealTime)
                    {
                        return 1;
                    }
                    else if (x._LastBoughtRealTime < y._LastBoughtRealTime)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (x._LastBoughtRealTime == null && y._LastBoughtRealTime != null)
                {
                    return -1;
                }
                else if (x._LastBoughtRealTime != null && y._LastBoughtRealTime == null)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }


            return -1;


        }

        //public int Compare(string x, string y)
        //{
        //    if (Box_Storage_Set.CompareMethod == 1)
        //    {


        //        char[] Xchars = new char[x.Length];
        //        char[] Ychars = new char[y.Length];

        //        int counter = 0;

        //        foreach (char c in x)
        //        {
        //            Xchars[counter++] = c;
        //        }

        //        counter = 0;

        //        foreach (char c in y)
        //        {
        //            Ychars[counter++] = c;
        //        }

        //        int Localx = Xchars[0] * Xchars[1] * Xchars[2];
        //        int Localy = Ychars[0] * Ychars[1] * Ychars[2];

        //        if (Localx < Localy)
        //        {
        //            return -1;
        //        }
        //        if (Localx > Localy)
        //        {
        //            return 1;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    else if (Box_Storage_Set.CompareMethod==2)
        //    {
        //        if (x.Width1 * 100 + x.Width1 * 10 + x.Width1 * 1 > y.Width1 * 100 + y.Width1 * 10 + y.Width1 * 1)
        //        {
        //            return 1;
        //        }
        //        else if (x.Width1 * 100 + x.Width1 * 10 + x.Width1 * 1 < y.Width1 * 100 + y.Width1 * 10 + y.Width1 * 1)
        //        {
        //            return -1;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}
    }
}
