using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Util
    {
        private static Random random = new Random();

        public static int[] GenerateRandomArray(int min, int max)
        {
            int arrayLength = max - min + 1;
            var arr = new int[arrayLength];

            for (var i = 0; i < arrayLength; i++)
            {
                arr[i] = i + min;
            }

            for (var i = 0; i < arrayLength; i++)
            {
                var randomIndex = random.Next(i, arrayLength - 1);
                var temp = arr[i];
                arr[i] = arr[randomIndex];
                arr[randomIndex] = temp;
            }

            return arr;
        }
    }
}
