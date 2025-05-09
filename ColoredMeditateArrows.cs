using BepInEx;
using RiskOfOptions;
using On.RoR2;
using R2API;
using Rewired;
using RoR2;
using RoR2.UI;
using Unity;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using RiskOfOptions.Options;
using BepInEx.Configuration;

namespace ColoredMeditateArrows
{
    // This attribute is required, and lists metadata for your plugin.
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency("com.rune580.riskofoptions")]

    // This is the main declaration of our plugin class.
    // BepInEx searches for all classes inheriting from BaseUnityPlugin to initialize on startup.
    // BaseUnityPlugin itself inherits from MonoBehaviour,
    // so you can use this as a reference for what you can declare and use in your plugin class
    // More information in the Unity Docs: https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    public class ExamplePlugin : BaseUnityPlugin
    {
        // The Plugin GUID should be a unique ID for this plugin,
        // which is human readable (as it is used in places like the config).
        // If we see this PluginGUID as it is on thunderstore,
        // we will deprecate this mod.
        // Change the PluginAuthor and the PluginName !
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "SirFrogsAlot";
        public const string PluginName = "ColoredMeditateArrows";
        public const string PluginVersion = "1.3.0";

        ConfigEntry<Color> upArrowColor;
        ConfigEntry<Color> downArrowColor;
        ConfigEntry<Color> rightArrowColor;
        ConfigEntry<Color> leftArrowColor;
        ConfigEntry<Color> completedColor;

        Color completedArrowColor;

        // The Awake() method is run at the very start when the game is initialized.
        public void Awake()
        {
            // Init our logging class so that we can properly log for debugging
            Log.Init(Logger);
            completedArrowColor = new Color(255, 233, 124);
            ConfigOptions();
            On.EntityStates.Seeker.Meditate.SetupInputUIIcons += OnEntityStates_Seeker_Meditate_SetupInputUIIcons;
            On.EntityStates.Seeker.Meditate.UpdateUIInputSequence += OnEntityStates_Seeker_Meditate_UpdateUIInputSequence;
        }

        private void ConfigOptions()
        {
            upArrowColor = Config.Bind<Color>("General", "Up Arrow", Color.red, "Color Picker to modify Up Arrow");
            downArrowColor = Config.Bind<Color>("General", "Down Arrow", Color.blue, "Color Picker to modify Down Arrow");
            rightArrowColor = Config.Bind<Color>("General", "Right Arrow", Color.yellow, "Color Picker to modify Right Arrow");
            leftArrowColor = Config.Bind<Color>("General", "Left Arrow", Color.green, "Color Picker to modify Up Arrow");
            completedColor = Config.Bind<Color>("General", "Completed Arrow", completedArrowColor, "Color Picker to modify the Completed Arrow");

            ModSettingsManager.AddOption(new ColorOption(upArrowColor));
            ModSettingsManager.AddOption(new ColorOption(downArrowColor));
            ModSettingsManager.AddOption(new ColorOption(rightArrowColor));
            ModSettingsManager.AddOption(new ColorOption(leftArrowColor));
            ModSettingsManager.AddOption(new ColorOption(completedColor));
        }

        private void OnEntityStates_Seeker_Meditate_SetupInputUIIcons(On.EntityStates.Seeker.Meditate.orig_SetupInputUIIcons orig, EntityStates.Seeker.Meditate self)
        {
            orig(self);

            for (int index = 0; index < 5; ++index)
            {
                switch (self.seekerController.meditationStepAndSequence[index + 1])
                {
                    case 0: // Up Arrow
                        self.overlayInstanceChildLocator.FindChild(EntityStates.Seeker.Meditate.c[index]).GetComponent<Image>().color = upArrowColor.Value;
                        break;
                    case 1: // Down Arrow
                        self.overlayInstanceChildLocator.FindChild(EntityStates.Seeker.Meditate.c[index]).GetComponent<Image>().color = downArrowColor.Value;
                        break;
                    case 2: // Right Arrow
                        self.overlayInstanceChildLocator.FindChild(EntityStates.Seeker.Meditate.c[index]).GetComponent<Image>().color = rightArrowColor.Value;
                        break;
                    case 3: // Left Arrow
                        self.overlayInstanceChildLocator.FindChild(EntityStates.Seeker.Meditate.c[index]).GetComponent<Image>().color = leftArrowColor.Value;
                        break;
                }
            }
        }

        private void OnEntityStates_Seeker_Meditate_UpdateUIInputSequence(On.EntityStates.Seeker.Meditate.orig_UpdateUIInputSequence orig, EntityStates.Seeker.Meditate self)
        {
            orig(self);

            for (int index = 0; index < (int)self.seekerController.meditationInputStep; ++index)
                self.overlayInstanceChildLocator.FindChild(EntityStates.Seeker.Meditate.c[index]).GetComponent<Image>().color = completedColor.Value;
        }
        // The Update() method is run on every frame of the game.
        private void Update()
        {
            
        }
    }
}
