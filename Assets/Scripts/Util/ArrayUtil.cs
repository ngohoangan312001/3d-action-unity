using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN
{
    public class ArrayUtil
    {
        public static List<T> RemoveNullSlot<T>(List<T> inputList)
        {
            for (int i = inputList.Count - 1; i < -1; i--)
            {
                if (inputList[i] == null)
                {
                    inputList.RemoveAt(i);
                }
            }

            return inputList;
        }
    }
}
