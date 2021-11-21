using UnityEngine;

namespace Assets.Scripts
{
    public static class MathExtensions
    {
        public static float SqrMagnitude2D(this Vector3 op1, Vector3 op2)
        {
            var diff = op2 - op1;

            var mag = (diff.x * diff.x) + (diff.z * diff.z);

            return mag;
        }
    }
}
