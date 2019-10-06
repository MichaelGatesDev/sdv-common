using System;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Monsters;

namespace SDVCommon.Extensions
{
    public static class PlayerExtensions
    {
        private const int HotbarSize = 12; // 1234567890-=

        public static Item[] GetHotbar(this Farmer farmer)
        {
            var result = new Item[HotbarSize];
            var items = farmer.Items;
            for (var i = 0; i < HotbarSize; i++)
            {
                var item = items[i];
                result[i] = item;
            }

            return result;
        }

        public static bool EquipTool(this Farmer farmer, ToolType type)
        {
            var items = farmer.GetHotbar();
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (item == null) continue;
                if (item.GetToolType() == type)
                {
                    farmer.EquipItemAt(index);
                }
            }

            return false;
        }

        public static void EquipItemAt(this Farmer farmer, int index)
        {
            farmer.CurrentToolIndex = index;
        }

        public static Monster GetNearestMonster(this Farmer farmer, out float distance)
        {
            Monster nearestMonster = null;
            var minDistance = -1f;
            var playerLoc = farmer.getTileLocation();
            foreach (var npc in farmer.currentLocation.characters)
            {
                if (!npc.IsMonster) continue;
                var monster = (Monster) npc;
                var monsterLoc = npc.getTileLocation();
                var distancePlayerToMonster = Vector2.Distance(playerLoc, monsterLoc);
                if (minDistance == -1f || distancePlayerToMonster < minDistance)
                {
                    minDistance = distancePlayerToMonster;
                    nearestMonster = monster;
                }
            }

            distance = minDistance;
            return nearestMonster;
        }

        public static bool IsMonsterInProximity(this Farmer farmer, float maxDistance, out Monster who)
        {
            who = GetNearestMonster(farmer, out var dist);
            if (who == null) return false;
            return dist <= maxDistance;
        }
    }
}