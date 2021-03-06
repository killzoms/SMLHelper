﻿namespace SMLHelper.V2.Patchers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Harmony;

    // This entire patcher is only here for performance reasons.
    // This is not intended to be exposed by the public API.
    internal static class InventoryPatcher
    {
        private static Dictionary<Vector2int, bool> HasRoomCache = new Dictionary<Vector2int, bool>();

        internal static void Patch(HarmonyInstance harmony)
        {
            // Original methods
            Type inventoryType = typeof(Inventory);
            MethodInfo HasRoomFor_Pickupable_Method = inventoryType.GetMethod("HasRoomFor", new Type[] { typeof(Pickupable) });
            MethodInfo HasRoomFor_XY_Method = inventoryType.GetMethod("HasRoomFor", new Type[] { typeof(int), typeof(int) });
            MethodInfo OnAddItem_Method = inventoryType.GetMethod("OnAddItem", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo OnRemoveItem_Method = inventoryType.GetMethod("OnRemoveItem", BindingFlags.NonPublic | BindingFlags.Instance);

            // Patcher methods
            Type patcherType = typeof(InventoryPatcher);
            MethodInfo HasRoomFor_Pickupable_Prefix_Method = patcherType.GetMethod("HasRoomFor_Pickupable_Prefix", BindingFlags.NonPublic | BindingFlags.Static);
            MethodInfo HasRoomFor_XY_Prefix_Method = patcherType.GetMethod("HasRoomFor_XY_Prefix", BindingFlags.NonPublic | BindingFlags.Static);
            MethodInfo HasRoomFor_Postfix_Method = patcherType.GetMethod("HasRoomFor_Postfix", BindingFlags.NonPublic | BindingFlags.Static);
            MethodInfo AddRemoveItem_Postfix_Method = patcherType.GetMethod("AddRemoveItem_Postfix", BindingFlags.NonPublic | BindingFlags.Static);

            // Harmony methods
            harmony.Patch(
                original: HasRoomFor_Pickupable_Method,
                prefix: new HarmonyMethod(HasRoomFor_Pickupable_Prefix_Method),
                postfix: new HarmonyMethod(HasRoomFor_Postfix_Method)); // Both HasRoomFor methods share the same Postfix method

            harmony.Patch(
                original: HasRoomFor_XY_Method,
                prefix: new HarmonyMethod(HasRoomFor_XY_Prefix_Method),
                postfix: new HarmonyMethod(HasRoomFor_Postfix_Method)); // Both HasRoomFor methods share the same Postfix method

            harmony.Patch(
                original: OnAddItem_Method,
                prefix: null, // No prefix call
                postfix: new HarmonyMethod(AddRemoveItem_Postfix_Method)); // OnAddItem and OnRemoveItem share the same Postfix method

            harmony.Patch(
                original: OnRemoveItem_Method,
                prefix: null, // No prefix call
                postfix: new HarmonyMethod(AddRemoveItem_Postfix_Method)); // OnAddItem and OnRemoveItem share the same Postfix method

            Logger.Log($"InventoryPatcher is done.");
        }

        private static bool HasRoomFor_Pickupable_Prefix(Inventory __instance, Pickupable item, ref bool __result, ref Vector2int __state)
        {
            Vector2int itemSize = CraftData.GetItemSize(item.overrideTechUsed ? item.overrideTechType : item.GetTechType());

            return CheckInventoryCache(itemSize, ref __result, ref __state);
        }

        private static bool HasRoomFor_XY_Prefix(Inventory __instance, int width, int height, ref bool __result, ref Vector2int __state)
        {
            var itemSize = new Vector2int(width, height);

            return CheckInventoryCache(itemSize, ref __result, ref __state);
        }

        private static bool CheckInventoryCache(Vector2int itemSize, ref bool __result, ref Vector2int __state)
        {
            // Minimum size should always be 1,1
            itemSize.x = itemSize.x == 0 ? 1 : itemSize.x;
            itemSize.y = itemSize.y == 0 ? 1 : itemSize.y;

            if (HasRoomCache.TryGetValue(itemSize, out bool cachedResult))
            {
                // We've seen this size before.
                __result = cachedResult;
                return false; // Override the result and avoid the heavy calculation.
            }

            // The result wasn't in the cache. Let the original code run to make the calculation.
            // We'll catch the result on the Postfix for the next frame.
            __state = itemSize;
            return true;
        }

        private static void HasRoomFor_Postfix(Inventory __instance, bool __result, ref Vector2int __state)
        {
            // We should only enter this method if the Prefix didn't have a cached value to use.
            // Catch the result and map it to the size provided by the Prefix.
            if (__state.x == 0 || __state.y == 0)
                return; // Somehow, this can still happen. Don't store 0,0 in the cache. It breaks the game.

            // The large calculation won't have to be done again until something is added or removed from the inventory.
            // How does the base game get away with calculating this mess on every frame???
            HasRoomCache.Add(__state, __result);
        }

        private static void AddRemoveItem_Postfix()
        {
            // Items in the inventory have changed. Clear out the cache.
            HasRoomCache.Clear();
        }
    }
}
