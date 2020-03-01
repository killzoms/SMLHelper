﻿#if BELOWZERO
namespace SMLHelper.V2.Patchers
{
    using System.Collections.Generic;
    using Harmony;
    using SMLHelper.V2.Assets;

    internal partial class CraftDataPatcher
    {
#region Internal Fields

        internal static IDictionary<TechType, JsonValue> CustomTechData = new SelfCheckingDictionary<TechType, JsonValue>("CustomTechData", AsStringFunction);

        #endregion

        internal static void AddToCustomTechData(TechType techType, JsonValue techData)
        {
            CustomTechData.Add(techType, techData);
        }

        private static void PatchForBelowZero(HarmonyInstance harmony)
        {
            harmony.Patch(AccessTools.Method(typeof(TechData), nameof(TechData.TryGetValue)),
                prefix: new HarmonyMethod(AccessTools.Method(typeof(CraftDataPatcher), nameof(CheckPatchRequired))));

            harmony.Patch(AccessTools.Method(typeof(TechData), nameof(TechData.Cache)),
                prefix: new HarmonyMethod(AccessTools.Method(typeof(CraftDataPatcher), nameof(AddCustomTechDataToOriginalDictionary))));

            harmony.Patch(AccessTools.Method(typeof(CraftData), nameof(CraftData.PreparePrefabIDCache)),
               postfix: new HarmonyMethod(AccessTools.Method(typeof(CraftDataPatcher), nameof(CraftDataPrefabIDCachePostfix))));
        }

        private static void CraftDataPrefabIDCachePostfix()
        {
            Dictionary<TechType, string> techMapping = CraftData.techMapping;
            Dictionary<string, TechType> entClassTechTable = CraftData.entClassTechTable;
            foreach (ModPrefab prefab in ModPrefab.Prefabs)
            {
                techMapping[prefab.TechType] = prefab.ClassID;
                entClassTechTable[prefab.ClassID] = prefab.TechType;
            }
        }

        private static void CheckPatchRequired(TechType techType)
        {
            if(CustomTechData.TryGetValue(techType, out JsonValue SMLTechData))
            {
                if (TechData.entries.ContainsKey(techType))
                {
                    if(SMLTechData != TechData.entries[techType])
                        AddCustomTechDataToOriginalDictionary();
                }
                else
                {
                    AddCustomTechDataToOriginalDictionary();
                }
            }
        }

        private static void AddCustomTechDataToOriginalDictionary()
        {
            short added = 0;
            short replaced = 0;
            foreach (TechType techType in CustomTechData.Keys)
            {
                JsonValue SMLTechData = CustomTechData[techType];
                bool techDataExists = TechData.entries.ContainsKey(techType);

                if (techDataExists)
                {
                    JsonValue techData = TechData.entries[techType];
                    if (techData != SMLTechData)
                    {
                        foreach (int key in SMLTechData.Keys)
                        {
                            techData[key] = SMLTechData[key];
                        }

                        Logger.Log($"{techType} TechType already existed in the CraftData.techData dictionary. Original value was Changed.", LogLevel.Warn);
                        replaced++;
                    }
                }
                else
                {
                    TechData.Add(techType, SMLTechData);
                    added++;
                }
            }

            if (added > 0)
                Logger.Log($"Added {added} new entries to the CraftData.techData dictionary.", LogLevel.Info);
            if (replaced > 0)
                Logger.Log($"Replaced {replaced} existing entries to the CraftData.techData dictionary.", LogLevel.Info);
        }
    }
}
#endif
