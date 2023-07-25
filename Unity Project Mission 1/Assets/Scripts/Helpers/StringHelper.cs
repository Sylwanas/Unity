using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterQuest
{
    public static class StringHelper
    {
        public static string JoinWithAnd(IEnumerable<string> items, bool useSerialComma = true)
        {
            int count = items.Count();
            switch (count)
            {
                case 0:
                    return "";

                case 1:
                    return items.ElementAt(0);

                case 2:
                    return $"{items.ElementAt(0)} and {items.ElementAt(1)}";

                default:
                    var itemsCopy = new List<string>(items);
                    if (useSerialComma)
                    {
                        string lastItem = $"and {items.ElementAt(count - 1)}";
                        itemsCopy[count - 1] = lastItem;
                    }
                    else
                    {
                        string joinedItems = $"{items.ElementAt(count - 2)} and {items.ElementAt(count - 1)}";
                        itemsCopy[count - 2] = joinedItems;
                        itemsCopy.RemoveAt(count - 1);
                    }
                    return string.Join($", ", itemsCopy);
            }
        }
    }
}
