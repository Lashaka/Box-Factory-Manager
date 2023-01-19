using System;
using System.Collections.Generic;
using Wpf_Class_Library_Logic.BoxOverall;

namespace Class_Library_Logic.BoxOverall
{
    public class Box_Storage_Functions
    {
        public static int TempExtraBoxes = 0;
        public static Box_Storage TempBox;
        public static bool ProcExtraBoxMessage = false;
        public static bool ProcMaxDeleteMessage = false;
        public static bool ProcBoxDoesntExist = false;
        public static bool ProcBuyMessage = false;
        public static bool ProcAreYouSureNotEnoughMessage = false;
        public static bool ProcAreYouSureSizeMessage = false;
        public static bool ProcBoxFoundTooBig = false;
        public static bool ProcLessThan10BoxesLeft = false;
        public static bool ProcNoBoxesOfThisKindLeft = false;


        public static SortedDictionary<Box_Storage, Box_Storage> ExpiredFunc(SortedDictionary<Box_Storage, Box_Storage> SortedDictionary, int days, bool NonExpired) //removes all boxes that werent bought for a week+
        {

            SortedDictionary<Box_Storage, Box_Storage> New_SortedDictionary = new SortedDictionary<Box_Storage, Box_Storage>(new KeyComparer());

            TempExtraBoxes = 0; //IPUS

            int counter = Box_Storage_Set.Box_Storagess_SortedDictionary.Count;

            foreach (Box_Storage key in Box_Storage_Set.Box_Storagess_SortedDictionary.Values)
            {
                Box_Storage_Set.CompareMethod = 4; //compare by date

                DateTime Now = DateTime.Now;
                TimeSpan MinusWeek = new TimeSpan(-(days * 24), 0, 0);
                DateTime TimeAgo = Now.Add(MinusWeek);

                if (NonExpired == true) //filter time bigger
                {
                    if (TimeAgo > key._LastBoughtRealTime)
                    {
                        key.BoxToAdd = 0; //reseting amount so wont influence amount
                        AddBoxToStorage(key, New_SortedDictionary);
                        counter--;
                    }
                }
                else if (NonExpired == false)//filter time smaller
                {
                    if (TimeAgo <= key._LastBoughtRealTime)
                    {
                        key.BoxToAdd = 0; //reseting amount so wont influence amount
                        AddBoxToStorage(key, New_SortedDictionary);
                        counter--;
                    }
                }



            }

            SortedDictionary = New_SortedDictionary;

            TempExtraBoxes = counter;
            Box_Storage_Set.CompareMethod = 2;
            return New_SortedDictionary;

        }
        public static string UpdatePurchaseDate()
        {
            DateTime Now = DateTime.Now;

            return Now.ToString();
        }
        public static DateTime UpdatePurchaseDateRealTime()
        {
            DateTime Now = DateTime.Now;

            return Now;
        }

        public static void AddBoxToStorage(Box_Storage box_Storage, SortedDictionary<Box_Storage, Box_Storage> SortedDictionary) //Checks if box exists in list, if does add amount to it, if not create and add.
        {
            Box_Storage_Set.CompareMethod = 2;
            if (SortedDictionary.ContainsKey(box_Storage) == true) //if the box exists in the set
            {
                if (SortedDictionary[box_Storage].BoxAmount + box_Storage._AmountToAdd <= 100)
                {
                    SortedDictionary[box_Storage].BoxAmount += box_Storage._AmountToAdd; //add the local boxes to the existing box
                }
                else
                {
                    TempExtraBoxes = SortedDictionary[box_Storage].BoxAmount + box_Storage._AmountToAdd - 100;
                    SortedDictionary[box_Storage].BoxAmount = 100;
                    ProcExtraBoxMessage = true;
                }
            }

            else
            {
                SortedDictionary.Add(box_Storage, box_Storage); //add this new box

                if (SortedDictionary.ContainsKey(box_Storage) == true)
                {
                    if (SortedDictionary[box_Storage].BoxAmount + box_Storage._AmountToAdd <= 100)
                    {
                        SortedDictionary[box_Storage].BoxAmount += box_Storage._AmountToAdd; //add the local boxes to the existing box
                    }
                    else
                    {
                        TempExtraBoxes = SortedDictionary[box_Storage].BoxAmount + box_Storage._AmountToAdd - 100;
                        SortedDictionary[box_Storage].BoxAmount = 100;
                        ProcExtraBoxMessage = true;
                    }
                }

            }



        }

        public static void RemoveBoxFromStorage(Box_Storage box_Storage, SortedDictionary<Box_Storage, Box_Storage> SortedDictionary) //Checks if box exists in list, if does add amount to it, if not create and add.
        {
            //string[] Keys = new string[2];
            //Keys[0] = box_Storage.GetBox().ToString();
            //Keys[1] = (box_Storage.Width1 * box_Storage.Width2 * box_Storage.Height).ToString();

            string Keys = box_Storage.GetBox().ToString();


            if (SortedDictionary.ContainsKey(box_Storage) == true) //if the box exists in the set
            {
                if (SortedDictionary[box_Storage].BoxAmount - box_Storage._AmountToAdd > 0)
                {
                    SortedDictionary[box_Storage].BoxAmount -= box_Storage._AmountToAdd; //remove amount
                }
                else if (SortedDictionary[box_Storage].BoxAmount - box_Storage._AmountToAdd == 0)
                {
                    SortedDictionary.Remove(box_Storage); //remove all
                    ProcNoBoxesOfThisKindLeft = true;
                }
                else
                {
                    SortedDictionary.Remove(box_Storage);
                    ProcMaxDeleteMessage = true;
                    ProcNoBoxesOfThisKindLeft = true;
                }
            }
            else
            {
                ProcBoxDoesntExist = true;
            }
        }

        public static void BuyBoxFromStorage(Box_Storage box_Storage, SortedDictionary<Box_Storage, Box_Storage> SortedDictionary) //Checks if box exists in list, if does add amount to it, if not create and add.
        {
            //string[] Keys = new string[2];
            //Keys[0] = box_Storage.GetBox().ToString();
            //Keys[1] = (box_Storage.Width1 * box_Storage.Width2 * box_Storage.Height).ToString();
            string Keys = box_Storage.GetBox().ToString();



            if (SortedDictionary.ContainsKey(box_Storage) == true) //if the box exists in the set
            {
                if (SortedDictionary[box_Storage].BoxAmount - box_Storage._AmountToAdd > 0)
                {
                    SortedDictionary[box_Storage].BoxAmount -= box_Storage._AmountToAdd; //remove amount
                    ProcBuyMessage = true;
                }
                else if (SortedDictionary[box_Storage].BoxAmount - box_Storage._AmountToAdd == 0)
                {
                    SortedDictionary.Remove(box_Storage);
                    ProcBuyMessage = true;
                }
                else
                {
                    SortedDictionary.Remove(box_Storage);
                    //message+request are you sure you still wanna buy X boxes
                }
            }
            else //calculate most suitable box
            {

            }
        }

        public static SortedDictionary<Box_Storage, Box_Storage> SortDataBySize() //sorting data by size needed ????? WHAT SORTS DO I NEED
        {
            Box_Storage_Set.CompareMethod = 2;

            SortedDictionary<Box_Storage, Box_Storage> New_SortedDictionary = new SortedDictionary<Box_Storage, Box_Storage>(new KeyComparer());


            foreach (Box_Storage key in Box_Storage_Set.Box_Storagess_SortedDictionary.Values)
            {
                key.BoxToAdd = 0; //reseting amount so wont influence amount
                AddBoxToStorage(key, New_SortedDictionary);
            }


            Box_Storage_Set.CompareMethod = 2;

            return New_SortedDictionary;

        }

        public static SortedDictionary<Box_Storage, Box_Storage> SortDataByGiftRequirements(Box_Storage box_Storage) //sorting data with compare method 3, puts the most suitable first
        {
            Box_Storage_Set.CompareMethod = 3;

            SortedDictionary<Box_Storage, Box_Storage> New_SortedDictionary = new SortedDictionary<Box_Storage, Box_Storage>(new KeyComparer());


            foreach (Box_Storage key in Box_Storage_Set.Box_Storagess_SortedDictionary.Values)
            {
                if (key.Width1 >= box_Storage.Width1 && key.Width2 >= box_Storage.Width2 || key.Width1 >= box_Storage.Width2 && key.Width2 >= box_Storage.Width1)
                {
                    key.BoxToAdd = 0; //reseting amount so wont influence amount
                    AddBoxToStorage(key, New_SortedDictionary);
                }
            }



            Box_Storage_Set.CompareMethod = 2;

            return New_SortedDictionary;

        }

        public static Box_Storage FindMostSuitableBox(Box_Storage box_Storage, SortedDictionary<Box_Storage, Box_Storage> SortedDictionary) //sorting data by size needed ????? WHAT SORTS DO I NEED
        {
            if (SortedDictionary.ContainsKey(box_Storage) == true) //if the box exists in the set
            {
                if (box_Storage._AmountToAdd > SortedDictionary[box_Storage].BoxAmount)
                {
                    ProcAreYouSureNotEnoughMessage = true;
                }

                if (SortedDictionary[box_Storage].BoxAmount - box_Storage._AmountToAdd < 10)
                {
                    ProcLessThan10BoxesLeft = true;
                }

                return box_Storage;
            }
            else
            {
                //message no found box, want something close?
                TempBox = null;
                SortedDictionary<Box_Storage, Box_Storage> New_SortedDictionary = SortDataByGiftRequirements(box_Storage);

                foreach (Box_Storage key in Box_Storage_Set.Box_Storagess_SortedDictionary.Values)
                {
                    if (key.Width1 >= box_Storage.Width1 && key.Width2 >= box_Storage.Width2)
                    {

                        if (key.Height >= box_Storage.Height && ((key.Width1 >= box_Storage.Width1 && key.Width2 >= box_Storage.Width2)))
                        {
                            if ((key.Width1 * key.Width2 * key.Height) * 0.5 < (box_Storage.Width1 * box_Storage.Width2 * box_Storage.Height)) //if volume is 50% bigger than what was asked for, box isnt suited
                            {
                                TempBox = key;
                                ProcBoxFoundTooBig = false; //if was true, now make false because new value is found

                                break;

                            }
                            else if (TempBox == null) //print message, box is too big, only happens if no box was found
                            {
                                ProcBoxFoundTooBig = true;
                            }
                        }
                        else
                        {
                            if (TempBox != null)
                            {
                                if (TempBox.Height >= box_Storage.Height) //if height is good, tempbox is good. else continue searching
                                {
                                    break; //because of the sort, means others are larger x*x and wont suit the gift requirements
                                }
                            }

                        }

                    }
                }

                if (TempBox == null)
                {
                    return null;
                }
                else
                {
                    ProcAreYouSureSizeMessage = true;

                    if (TempBox.BoxAmount < box_Storage._AmountToAdd)
                    {
                        ProcAreYouSureNotEnoughMessage = true;
                    }

                    if (TempBox.BoxAmount - box_Storage._AmountToAdd < 10)
                    {
                        ProcLessThan10BoxesLeft = true;
                    }

                    //if temp box is good
                    return TempBox;

                    //else return message

                    //sort by the way i want
                }


            }




            //  return New_SortedDictionary;

        }
    }
}
