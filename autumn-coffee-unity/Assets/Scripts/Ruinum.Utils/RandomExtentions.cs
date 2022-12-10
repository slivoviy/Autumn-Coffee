using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ruinum.Utils
{
    public static class RandomExtentions
    {
        public static T RandomList<T>(List<T> ts)
        {
            return ts[Random.Range(0, ts.Count)];
        }
        public static bool HalfByHalf()
        {
            return Random.value > .5f ? false : true;
        }
        public static bool RandomLess(float a)
        {
            return Random.value < a ? false : true;
        }
    }
}
