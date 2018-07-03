﻿namespace SMLHelper.V2.Handlers
{
    using Crafting;
    using Patchers;

    /// <summary>
    /// A handler class for adding and editing crafted items.
    /// </summary>
    public static class CraftDataHandler
    {
        /// <summary>
        /// Contains possible background colors
        /// </summary>
        public enum BackgroundColor
        {
            /// <summary>
            /// The default, blue background color
            /// </summary>
            Normal = CraftData.BackgroundType.Normal,
            /// <summary>
            /// The purple background color seen in prawn suit arms and seeds
            /// </summary>
            Purple = CraftData.BackgroundType.ExosuitArm,
            /// <summary>
            /// The green background color seen in plants
            /// </summary>
            Green = CraftData.BackgroundType.PlantAirSeed
        }

        #region Core Methods
        
        /// <summary>
        /// <para>Allows you to edit recipes, i.e. TechData for TechTypes.</para>
        /// <para>Can be used for existing TechTypes too.</para>
        /// </summary>
        /// <param name="techType">The TechType whose TechData you want to edit.</param>
        /// <param name="techData">The TechData for that TechType.</param>
        /// <seealso cref="TechData"/>
        public static void EditTechData(TechType techType, TechData techData)
        {
            CraftDataPatcher.CustomTechData[techType] = techData;
        }

        /// <summary>
        /// <para>Allows you to edit EquipmentTypes for TechTypes.</para>
        /// <para>Can be used for existing TechTypes too.</para>
        /// </summary>
        /// <param name="techType">The TechType whose EqiupmentType you want to edit.</param>
        /// <param name="equipmentType">The EquipmentType for that TechType.</param>
        public static void EditEquipmentType(TechType techType, EquipmentType equipmentType)
        {
            CraftDataPatcher.CustomEquipmentTypes[techType] = equipmentType;
        }

        /// <summary>
        /// <para>Allows you to edit QuickSlotType for TechTypes.</para>
        /// <para>Can be used for existing TechTypes too.</para>
        /// </summary>
        /// <param name="techType">The TechType whose QuickSlotType you want to edit.</param>
        /// <param name="slotType">The QuickSlotType for that TechType.</param>
        public static void EditQuickSlotType(TechType techType, QuickSlotType slotType)
        {
            CraftDataPatcher.CustomSlotTypes[techType] = slotType;
        }

        /// <summary>
        /// <para>Allows you to edit harvest output, i.e. what TechType you get when you "harvest" a TechType.</para>
        /// <para>Can be used for existing TechTypes too.</para>
        /// </summary>
        /// <param name="techType">The TechType whose harvest output you want to edit.</param>
        /// <param name="harvestOutput">The harvest output for that TechType.</param>
        public static void EditHarvestOutput(TechType techType, TechType harvestOutput)
        {
            CraftDataPatcher.CustomHarvestOutputList[techType] = harvestOutput;
        }

        /// <summary>
        /// <para>Allows you to edit how TechTypes are harvested.</para>
        /// <para>Can be used for existing TechTypes too.</para>
        /// </summary>
        /// <param name="techType">The TechType whose HarvestType you want to edit.</param>
        /// <param name="harvestType">The HarvestType for that TechType.</param>
        public static void EditHarvestType(TechType techType, HarvestType harvestType)
        {
            CraftDataPatcher.CustomHarvestTypeList[techType] = harvestType;
        }

        /// <summary>
        /// <para>Allows you to edit how much additional slices/seeds are given upon last knife hit.</para>
        /// <para>Can be used for existing TechTypes too.</para>
        /// </summary>
        /// <param name="techType">The TechType whose final cut bonus you want to edit.</param>
        /// <param name="bonus">The number of additional slices/seeds you'll receive on last cut.</param>
        public static void EditHarvestFinalCutBonus(TechType techType, int bonus)
        {
            CraftDataPatcher.CustomFinalCutBonusList[techType] = bonus;
        }

        /// <summary>
        /// <para>Allows you to edit item sizes for TechTypes.</para>
        /// <para>Can be used for existing TechTypes too.</para>
        /// </summary>
        /// <param name="techType">The TechType whose item size you want to edit.</param>
        /// <param name="size">The item size for that TechType.</param>
        public static void EditItemSize(TechType techType, Vector2int size)
        {
            CraftDataPatcher.CustomItemSizes[techType] = size;
        }

        /// <summary>
        /// <para>Allows you to edit item sizes for TechTypes.</para>
        /// <para>Can be used for existing TechTypes too.</para>
        /// </summary>
        /// <param name="techType">The TechType whose item size you want to edit.</param>
        /// <param name="x">The width of the item</param>
        /// <param name="y">The height of the item</param>
        public static void EditItemSize(TechType techType, int x, int y)
        {
            CraftDataPatcher.CustomItemSizes[techType] = new Vector2int(x, y);
        }

        /// <summary>
        /// <para>Allows you to edit crafting times for TechTypes.</para>
        /// <para>Can be used for existing TechTypes too.</para>
        /// </summary>
        /// <param name="techType">The TechType whose crafting time you want to edit.</param>
        /// <param name="time">The crafting time, in seconds, for that TechType.</param>
        public static void EditCraftingTime(TechType techType, float time)
        {
            CraftDataPatcher.CustomCraftingTimes[techType] = time;
        }

        /// <summary>
        /// <para>Allows you to edit the cooked creature list, i.e. associate the unedible TechType to the cooked TechType.</para>
        /// <para>Can be used for existing TechTypes too.</para>
        /// </summary>
        /// <param name="uncooked">The TechType whose cooked creature counterpart to edit.</param>
        /// <param name="cooked">The cooked creature counterpart for that TechType.</param>
        public static void EditCookedVariant(TechType uncooked, TechType cooked)
        {
            CraftDataPatcher.CustomCookedCreatureList[uncooked] = cooked;
        }

        /// <summary>
        /// <para>Allows you to edit inventory background colors for TechTypes.</para>
        /// </summary>
        /// <param name="techType">The TechType whose BackgroundType you want to edit.</param>
        /// <param name="backgroundColor">The background color for that TechType.</param>
        /// <seealso cref="CraftData.BackgroundType"/>
        public static void EditBackgroundColor(TechType techType, BackgroundColor backgroundColor) => EditBackgroundColor(techType, (CraftData.BackgroundType)(int)backgroundColor);

        /// <summary>
        /// <para>Allows you to edit inventory background colors for TechTypes.</para>
        /// </summary>
        /// <param name="techType">The TechType whose BackgroundType you want to edit.</param>
        /// <param name="backgroundColor">The background color for that TechType.</param>
        /// <seealso cref="CraftData.BackgroundType"/>
        public static void EditBackgroundColor(TechType techType, CraftData.BackgroundType backgroundColor)
        {
            CraftDataPatcher.CustomBackgroundTypes[techType] = backgroundColor;
        }

        /// <summary>
        /// Allows you to add items to the buildable list.
        /// </summary>
        /// <param name="techType">The TechType which you want to add to the buildable list.</param>
        public static void AddToBuildableList(TechType techType)
        {
            CraftDataPatcher.CustomBuildables.Add(techType);
        }

        #endregion

        // Typically, when adding custom items, other modders will likely be looking for "Add" methods without realising that the "Edit" methods above also add.
        // This set of methods below is here to to address the naming expectations without altering actual functionality.
        #region Redundant but friendly
        
        /// <summary>
        /// <para>Allows you to add a recipe, i.e. TechData for your TechType.</para>
        /// </summary>
        /// <param name="techType">The TechType that is receiving a new TechData recipe.</param>
        /// <param name="techData">The TechData recipe for that TechType.</param>
        /// <seealso cref="TechData"/>
        public static void AddTechData(TechType techType, TechData techData) => EditTechData(techType, techData);

        /// <summary>
        /// <para>Allows you to add an EquipmentType attribute to your TechType.</para>
        /// </summary>
        /// <param name="techType">The TechType whose EqiupmentType you want to set.</param>
        /// <param name="equipmentType">The EquipmentType for that TechType.</param>
        public static void SetEquipmentType(TechType techType, EquipmentType equipmentType) => EditEquipmentType(techType, equipmentType);

        /// <summary>
        /// <para>Allows you to add a QuickSlotType attribute to your TechType.</para>
        /// </summary>
        /// <param name="techType">The TechType whose QuickSlotType you want to set.</param>
        /// <param name="slotType">The QuickSlotType for that TechType.</param>
        public static void SetQuickSlotType(TechType techType, QuickSlotType slotType) => EditQuickSlotType(techType, slotType);

        /// <summary>
        /// <para>Allows you to add harvest output, i.e. what TechType you get when you "harvest" your TechType.</para>        
        /// </summary>
        /// <param name="techType">The TechType whose harvest output you want to set.</param>
        /// <param name="harvestOutput">The harvest output for that TechType.</param>
        public static void SetHarvestOutput(TechType techType, TechType harvestOutput) => EditHarvestOutput(techType, harvestOutput);

        /// <summary>
        /// <para>Allows you to set how your TechType is harvested.</para>
        /// </summary>
        /// <param name="techType">The TechType whose HarvestType you want to set.</param>
        /// <param name="harvestType">The HarvestType for that TechType.</param>
        public static void SetHarvestType(TechType techType, HarvestType harvestType) => EditHarvestType(techType, harvestType);

        /// <summary>
        /// <para>Allows you to add final cut bonus slices/seeds to your TechType.</para>
        /// </summary>
        /// <param name="techType">The TechType whose final cut bonus you want to set.</param>
        /// <param name="bonus">The number of additional slices/seeds you'll receive on last cut.</param>
        public static void SetHarvestFinalCutBonus(TechType techType, int bonus) => EditHarvestFinalCutBonus(techType, bonus);

        /// <summary>
        /// <para>Allows you to set a non-default item size for your TechType.</para>
        /// <para>By default item sizes are 1x1 in the inventory.</para>
        /// </summary>
        /// <param name="techType">The TechType whose item size you want to set.</param>
        /// <param name="size">The item size for that TechType.</param>
        public static void SetItemSize(TechType techType, Vector2int size) => EditItemSize(techType, size);

        /// <summary>
        /// <para>Allows you to edit item sizes for TechTypes.</para>
        /// <para>By default item sizes are 1x1 in the inventory.</para>
        /// </summary>
        /// <param name="techType">The TechType whose item size you want to edit.</param>
        /// <param name="x">The width of the item</param>
        /// <param name="y">The height of the item</param>
        public static void SetItemSize(TechType techType, int x, int y) => EditItemSize(techType, x, y);

        /// <summary>
        /// <para>Allows you to add a non-default crafting time for your TechType.</para>
        /// </summary>
        /// <param name="techType">The TechType whose crafting time you want to set.</param>
        /// <param name="time">The crafting time, in seconds, for that TechType.</param>
        public static void SetCraftingTime(TechType techType, float time) => EditCraftingTime(techType, time);

        /// <summary>
        /// <para>Allows you to pair your cooked creature TechType with your unedible/uncooked creature TechType.</para>
        /// </summary>
        /// <param name="uncooked">The TechType whose cooked creature counterpart to edit.</param>
        /// <param name="cooked">The cooked creature counterpart for that TechType.</param>
        public static void SetCookedVariant(TechType uncooked, TechType cooked) => EditCookedVariant(uncooked, cooked);

        /// <summary>
        /// <para>Allows you to associate an inventory background color to your TechType.</para>
        /// </summary>
        /// <param name="techType">The TechType whose BackgroundType you want to set.</param>
        /// <param name="backgroundColor">The background color for that TechType.</param>
        /// <seealso cref="CraftData.BackgroundType"/>
        public static void SetBackgroundColor(TechType techType, BackgroundColor backgroundColor) => EditBackgroundColor(techType, backgroundColor);

        /// <summary>
        /// <para>Allows you to associate an inventory background color to your TechType.</para>
        /// </summary>
        /// <param name="techType">The TechType whose BackgroundType you want to set.</param>
        /// <param name="backgroundColor">The background color for that TechType.</param>
        /// <seealso cref="CraftData.BackgroundType"/>
        public static void SetBackgroundColor(TechType techType, CraftData.BackgroundType backgroundColor) => EditBackgroundColor(techType, backgroundColor);

        #endregion
    }
}
