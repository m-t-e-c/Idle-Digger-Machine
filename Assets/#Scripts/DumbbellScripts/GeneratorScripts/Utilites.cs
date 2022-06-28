using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dumbbell{
    public static class Utilities
    {
        public static string CrunchNumbers(float x)
    {
        float num = 0;
        if (x <= 1000)
        {
            num = x;
            return num.ToString("F0");
        }
        else if (x <= 1000000)
        {
            num = x / 1000;
            return num.ToString("F2") + "K";
        }
        else if (x <= 1000000000)
        {
            num = x / 1000000;
            return num.ToString("F2") + "M";
        }
        else if (x <= 1000000000000)
        {
            num = x / 1000000000;
            return num.ToString("F2") + "B";
        }
        else if (x <= 1000000000000000)
        {
            num = x / 1000000000000;
            return num.ToString("F2") + "T";
        }
        else if (x <= 1000000000000000000.0f)
        {
            num = x / 1000000000000000.0f;
            return num.ToString("F2") + "aa";
        }
        else if (x <= 1000000000000000000000.0f)
        {
            num = x / 1000000000000000000.0f;
            return num.ToString("F2") + "ab";
        }
        else if (x <= 1000000000000000000000000.0f)
        {
            num = x / 1000000000000000000000.0f;
            return num.ToString("F2") + "ac";
        }
        else if (x <= 1000000000000000000000000000.0f)
        {
            num = x / 1000000000000000000000000.0f;
            return num.ToString("F2") + "ad";
        }
        else if (x <= 1000000000000000000000000000000.0f)
        {
            num = x / 1000000000000000000000000000.0f;
            return num.ToString("F2") + "ae";
        }
        else if (x <= 1000000000000000000000000000000000.0f)
        {
            num = x / 1000000000000000000000000000000.0f;
            return num.ToString("F2") + "af";
        }
        else if (x <= 1000000000000000000000000000000000000.0f)
        {
            num = x / 1000000000000000000000000000000000.0f;
            return num.ToString("F2") + "ag";
        }
        // else if (x <= 1000000000000000000000000000000000000000.0)
        // {
        //     num = x / 1000000000000000000000000000000000000.0;
        //     return num.ToString("F2") + "ah";
        // }
        // else if (x <= 1000000000000000000000000000000000000000000.0)
        // {
        //     num = x / 1000000000000000000000000000000000000000.0;
        //     return num.ToString("F2") + "ai";
        // }
        // else if (x <= 1000000000000000000000000000000000000000000000.0)
        // {
        //     num = x / 1000000000000000000000000000000000000000000.0;
        //     return num.ToString("F2") + "aj";
        // }
        // else if (x <= 1000000000000000000000000000000000000000000000000.0)
        // {
        //     num = x / 1000000000000000000000000000000000000000000000.0;
        //     return num.ToString("F2") + "ak";
        // }
        // else if (x <= 1000000000000000000000000000000000000000000000000000.0)
        // {
        //     num = x / 1000000000000000000000000000000000000000000000000.0;
        //     return num.ToString("F2") + "al";
        // }
        // else if (x <= 1000000000000000000000000000000000000000000000000000000.0)
        // {
        //     num = x / 1000000000000000000000000000000000000000000000000000.0;
        //     return num.ToString("F2") + "am";
        // }
        // else if (x <= 1000000000000000000000000000000000000000000000000000000000.0)
        // {
        //     num = x / 1000000000000000000000000000000000000000000000000000000.0;
        //     return num.ToString("F2") + "an";
        // }
        // else if (x <= 1000000000000000000000000000000000000000000000000000000000000.0)
        // {
        //     num = x / 1000000000000000000000000000000000000000000000000000000000.0;
        //     return num.ToString("F2") + "ao";
        // }
        // else if (x <= 1000000000000000000000000000000000000000000000000000000000000000.0)
        // {
        //     num = x / 1000000000000000000000000000000000000000000000000000000000000.0;
        //     return num.ToString("F2") + "ap";
        // }
        // else if (x <= 1000000000000000000000000000000000000000000000000000000000000000000.0)
        // {
        //     num = x / 1000000000000000000000000000000000000000000000000000000000000000.0;
        //     return num.ToString("F2") + "aq";
        // }
        // else if (x <= 1000000000000000000000000000000000000000000000000000000000000000000000.0)
        // {
        //     num = x / 1000000000000000000000000000000000000000000000000000000000000000000.0;
        //     return num.ToString("F2") + "ar";
        // }
        return num.ToString("F2");
    }
        public static bool IntegerToBool(int x)
        {
            if (x == 1) return true;
            else return false;
        } 
        public static int StringToInteger(string x)
        {
            int i = int.Parse(x);
            return i;
        }
        public static string TimeFormatter(int x)
        {
            int minutes = Mathf.FloorToInt(x / 60F);
            int seconds = Mathf.FloorToInt(x - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            return niceTime;
        }
        [Serializable]
        class NamesList { public List<string> names; }
        static NamesList namesList;
        static NamesList CurrentNamesList
        {
            get
            {
                if (namesList == null)
                {
                    TextAsset textAsset = Resources.Load<TextAsset>("Resources/GamerTags");
                    namesList = JsonUtility.FromJson<NamesList>(textAsset.text);
                }
                return namesList;
            }
        }
        public static string GetRandomName() { return CurrentNamesList.names[UnityEngine.Random.Range(0, CurrentNamesList.names.Count)]; }
        public static List<string> GetRandomNames(int names)
        {
            if (names > CurrentNamesList.names.Count) throw new Exception("Names count exceeded!");
            
            NamesList copy = new NamesList();
            copy.names = new List<string>(CurrentNamesList.names);

            List<string> result = new List<string>();

            for (int i = 0; i < names; i++)
            {
                int rnd = UnityEngine.Random.Range(0, copy.names.Count);
                result.Add(copy.names[rnd]);
                copy.names.RemoveAt(rnd);
            }
            return result;
        }
    }
}
