using System.Linq;
using StardewValley;
using StardewValley.Tools;

namespace SDVCommon.Extensions
{
    public static class ItemExtensions
    {
        public static ToolType GetToolType(this Item item)
        {
            var id = item.ParentSheetIndex;

            if (item is Axe)
            {
                return ToolType.Axe;
            }

            if (item is Hoe)
            {
                return ToolType.Hoe;
            }

            if (item is FishingRod)
            {
                return ToolType.FishingPole;
            }

            if (item is Pickaxe)
            {
                return ToolType.Pickaxe;
            }

            if (item is WateringCan)
            {
                return ToolType.WateringCan;
            }

            if (item is MeleeWeapon)
            {
                if (id == 47) return ToolType.Scythe;
                return ToolType.MeleeWeapon;
            }

            return ToolType.Invalid;
        }
    }
}