using UnityEngine;

namespace Utils
{
    public static class OrganicMovements
    {
        public static Quaternion ConvertInputIntoRotation(int x, int y)
        {
            if (y == 1)
                return Quaternion.Euler(0, 0, 0);
            if (x == 1)
                return Quaternion.Euler(0, 90, 0);
            if (y == -1)
                return Quaternion.Euler(0, 180, 0);
            if (x == -1)
                return Quaternion.Euler(0, -90, 0);
            return Quaternion.identity;
        }
    }
}