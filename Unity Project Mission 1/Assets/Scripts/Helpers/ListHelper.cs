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
        public static void Shuffle(List<Creature> creatures)
        {
            for (int i = creatures.Count - 1; i > 0; i--)
            {
                int j = rearrange.Next(i + 1);
                object temp = creatures[i];
                creatures[i] = creatures[j];
                creatures[j] = (Creature)temp;
            }
        }
    }
}
