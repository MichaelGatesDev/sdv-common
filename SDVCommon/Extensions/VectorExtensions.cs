using Microsoft.Xna.Framework;

namespace SDVCommon.Extensions
{
    public static class VectorExtensions
    {
        public static float DistanceTo(this Vector2 from, Vector2 to)
        {
            return Vector2.Distance(from, to);
        }
    }
}