using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.TerrainFeatures;

namespace SDVCommon
{
    [Flags]
    [SuppressMessage("ReSharper", "ShiftExpressionRealShiftCountIsZero")]
    public enum TileProperties
    {
        None = 0,
        Diggable = 1 << 0,
        Mineable = 1 << 1,
        Choppable = 1 << 2,
        Cuttable = 1 << 3,
        Waterable = 1 << 4,
    }

    public class TileUtils
    {
        public static TileProperties GetTileProperties(Vector2 vector)
        {
            var result = TileProperties.None;
            var player = Game1.player;
            var location = player.currentLocation;

            if (location.doesTileHaveProperty((int) vector.X, (int) vector.Y, "Diggable", "Back") != null)
            {
                result |= TileProperties.Diggable;
            }

            if (location.doesTileHaveProperty((int) vector.X, (int) vector.Y, "Water", "Back") != null)
            {
                result |= TileProperties.Waterable;
            }

            var pathsTileIndex = location.getTileIndexAt((int) vector.X, (int) vector.Y, "Paths");
            if (pathsTileIndex == 19) // large log
            {
                result |= TileProperties.Choppable;
            }

            if (pathsTileIndex == 20) // boulder
            {
                result |= TileProperties.Mineable;
            }

            if (pathsTileIndex == 21) // hardwood stump
            {
                result |= TileProperties.Choppable;
            }

            var objects = location.objects;
            if (objects != null)
            {
                if (objects.ContainsKey(vector))
                {
                    if (objects[vector].name.Equals("Artifact Spot"))
                    {
                        result |= TileProperties.Diggable;
                    }

                    if (objects[vector].name.Equals("Stone"))
                    {
                        result |= TileProperties.Mineable;
                    }

                    if (objects[vector].name.Equals("Twig"))
                    {
                        result |= TileProperties.Choppable;
                    }

                    if (objects[vector].name.Equals("Weeds"))
                    {
                        result |= TileProperties.Cuttable;
                    }
                }
            }

            var terrainFeatures = location.terrainFeatures;
            if (terrainFeatures != null && terrainFeatures.ContainsKey(vector))
            {
                var feature = terrainFeatures[vector];
                if (feature is HoeDirt dirt)
                {
                    if (dirt.crop != null && // has crop on it
                        (
                            dirt.crop.harvestMethod.Value == 1 && dirt.crop.fullyGrown.Value || // fully grown and can be harvested with scythe
                            dirt.crop.dead.Value // dead and can be cut with scythe
                        )
                    )
                    {
                        result |= TileProperties.Cuttable;
                    }
                    else
                    {
                        result |= TileProperties.Waterable;
                    }
                }

                if (terrainFeatures[vector] is GiantCrop)
                {
                    result |= TileProperties.Choppable;
                }

                if (terrainFeatures[vector] is Tree)
                {
                    result |= TileProperties.Choppable;
                }

                if (terrainFeatures[vector] is Grass)
                {
                    result |= TileProperties.Cuttable;
                }
            }

            return result;
        }
    }
}