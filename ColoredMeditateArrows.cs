using BepInEx;
using EntityStates.Seeker;
using IL.RoR2;
using On.EntityStates.Seeker;
using On.EntityStates.Toolbot;
using On.RoR2;
using R2API;
using Rewired;
using RoR2;
using RoR2.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace ColoredMeditateArrows
{
    // This attribute is required, and lists metadata for your plugin.
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

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
        public const string PluginVersion = "1.0.0";

        // The Awake() method is run at the very start when the game is initialized.
        public void Awake()
        {
            // Init our logging class so that we can properly log for debugging
            Log.Init(Logger);

            On.EntityStates.Seeker.MeditationUI.SetupInputUIIcons += OnEntityStates_Seeker_MeditationUI_SetupInputUIIcons;
        }

        private void OnEntityStates_Seeker_MeditationUI_SetupInputUIIcons(On.EntityStates.Seeker.MeditationUI.orig_SetupInputUIIcons orig, EntityStates.Seeker.MeditationUI self)
        {
            orig(self);

            for (int index = 0; index < 5; ++index)
            {
                switch (self.seekerController.meditationStepAndSequence[index + 1])
                {
                    case 0:
                        self.overlayInstanceChildLocator.FindChild(EntityStates.Seeker.MeditationUI.c[index]).GetComponent<Image>().color = Color.red;
                        break;
                    case 1:
                        self.overlayInstanceChildLocator.FindChild(EntityStates.Seeker.MeditationUI.c[index]).GetComponent<Image>().color = Color.green;
                        break;
                    case 2:
                        self.overlayInstanceChildLocator.FindChild(EntityStates.Seeker.MeditationUI.c[index]).GetComponent<Image>().color = Color.blue;
                        break;
                    case 3:
                        self.overlayInstanceChildLocator.FindChild(EntityStates.Seeker.MeditationUI.c[index]).GetComponent<Image>().color = Color.yellow;
                        break;
                }
            }
        }
        // The Update() method is run on every frame of the game.
        private void Update()
        {
            
        }
    }
}
