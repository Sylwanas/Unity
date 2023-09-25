using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

namespace MonsterQuest
{
    public static class ListHelper
    {
        static Random rearrange = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rearrange.Next(i + 1);
                object temp = list[i];
                list[i] = list[j];
                list[j] = (T)temp;
            }
        }
    }
}
