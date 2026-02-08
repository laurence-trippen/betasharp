using betareborn.Client;
using betareborn.Client.Resource.Language;
using betareborn.Stats;
using java.io;
using java.lang;

namespace betareborn
{
    public class GameSettings : java.lang.Object
    {
        private static readonly string[] RENDER_DISTANCES = ["options.renderDistance.far", "options.renderDistance.normal", "options.renderDistance.short", "options.renderDistance.tiny"];
        private static readonly string[] DIFFICULTIES = ["options.difficulty.peaceful", "options.difficulty.easy", "options.difficulty.normal", "options.difficulty.hard"];
        private static readonly string[] GUISCALES = ["options.guiScale.auto", "options.guiScale.small", "options.guiScale.normal", "options.guiScale.large"];
        private static readonly string[] LIMIT_FRAMERATES = ["performance.max", "performance.balanced", "performance.powersaver"];
        private static readonly string[] ANISO_LEVELS = ["options.off", "2x", "4x", "8x", "16x"];
        private static readonly string[] MSAA_LEVELS = ["options.off", "2x", "4x", "8x"];
        public static float MaxAnisotropy = 1.0f;
        public float musicVolume = 1.0F;
        public float soundVolume = 1.0F;
        public float mouseSensitivity = 0.5F;
        public bool invertMouse = false;
        public int renderDistance = 0;
        public bool viewBobbing = true;
        public int limitFramerate = 1;
        public string skin = "Default";
        public KeyBinding keyBindForward = new("key.forward", 17);
        public KeyBinding keyBindLeft = new("key.left", 30);
        public KeyBinding keyBindBack = new("key.back", 31);
        public KeyBinding keyBindRight = new("key.right", 32);
        public KeyBinding keyBindJump = new("key.jump", 57);
        public KeyBinding keyBindInventory = new("key.inventory", 18);
        public KeyBinding keyBindDrop = new("key.drop", 16);
        public KeyBinding keyBindChat = new("key.chat", 20);
        public KeyBinding keyBindCommand = new("key.command", Keyboard.KEY_SLASH);
        public KeyBinding keyBindToggleFog = new("key.fog", 33);
        public KeyBinding keyBindSneak = new("key.sneak", 42);
        public KeyBinding[] keyBindings;
        protected Minecraft mc;
        private readonly java.io.File optionsFile;
        public int difficulty = 2;
        public bool hideGUI = false;
        public bool thirdPersonView = false;
        public bool showDebugInfo = false;
        public string lastServer = "";
        public bool field_22275_C = false;
        public bool smoothCamera = false;
        public bool field_22273_E = false;
        public float field_22272_F = 1.0F;
        public float field_22271_G = 1.0F;
        public int guiScale = 1;
        public int anisotropicLevel = 0;
        public int msaaLevel = 0;
        public int INITIAL_MSAA = 0;
        public bool useMipmaps = true;
        public bool debugMode = false;
        public bool environmentAnimation = true;

        public GameSettings(Minecraft var1, java.io.File var2)
        {
            keyBindings = [keyBindForward, keyBindLeft, keyBindBack, keyBindRight, keyBindJump, keyBindSneak, keyBindDrop, keyBindInventory, keyBindChat, keyBindToggleFog];
            mc = var1;
            optionsFile = new java.io.File(var2, "options.txt");
            loadOptions();
            INITIAL_MSAA = msaaLevel;
        }

        public GameSettings()
        {
        }

        public string getKeyBindingDescription(int var1)
        {
            TranslationStorage var2 = TranslationStorage.getInstance();
            return var2.translateKey(keyBindings[var1].keyDescription);
        }

        public string getOptionDisplayString(int var1)
        {
            return Keyboard.getKeyName(keyBindings[var1].keyCode);
        }

        public void setKeyBinding(int var1, int var2)
        {
            keyBindings[var1].keyCode = var2;
            saveOptions();
        }

        public void setOptionFloatValue(EnumOptions var1, float var2)
        {
            if (var1 == EnumOptions.MUSIC)
            {
                musicVolume = var2;
                mc.sndManager.onSoundOptionsChanged();
            }

            if (var1 == EnumOptions.SOUND)
            {
                soundVolume = var2;
                mc.sndManager.onSoundOptionsChanged();
            }

            if (var1 == EnumOptions.SENSITIVITY)
            {
                mouseSensitivity = var2;
            }

        }

        public void setOptionValue(EnumOptions var1, int var2)
        {
            if (var1 == EnumOptions.INVERT_MOUSE)
            {
                invertMouse = !invertMouse;
            }

            if (var1 == EnumOptions.RENDER_DISTANCE)
            {
                renderDistance = renderDistance + var2 & 3;
            }

            if (var1 == EnumOptions.GUI_SCALE)
            {
                guiScale = guiScale + var2 & 3;
            }

            if (var1 == EnumOptions.VIEW_BOBBING)
            {
                viewBobbing = !viewBobbing;
            }

            if (var1 == EnumOptions.FRAMERATE_LIMIT)
            {
                limitFramerate = (limitFramerate + var2 + 3) % 3;
            }

            if (var1 == EnumOptions.DIFFICULTY)
            {
                difficulty = difficulty + var2 & 3;
            }

            if (var1 == EnumOptions.ANISOTROPIC)
            {
                anisotropicLevel = (anisotropicLevel + var2) % 5;
                int val = anisotropicLevel == 0 ? 0 : (int)System.Math.Pow(2, anisotropicLevel);
                if (val > MaxAnisotropy)
                {
                    anisotropicLevel = 0;
                }

                if (Minecraft.INSTANCE?.textureManager != null)
                {
                    Minecraft.INSTANCE.textureManager.refreshTextures();
                }
            }

            if (var1 == EnumOptions.MIPMAPS)
            {
                useMipmaps = !useMipmaps;
                if (Minecraft.INSTANCE?.textureManager != null)
                {
                    Minecraft.INSTANCE.textureManager.refreshTextures();
                }
            }

            if (var1 == EnumOptions.MSAA)
            {
                msaaLevel = (msaaLevel + var2) % 4;
            }

            if (var1 == EnumOptions.DEBUG_MODE)
            {
                debugMode = !debugMode;
                Profiling.Profiler.Enabled = debugMode;
            }

            if (var1 == EnumOptions.ENVIRONMENT_ANIMATION)
            {
                environmentAnimation = !environmentAnimation;
            }

            saveOptions();
        }

        public float getOptionFloatValue(EnumOptions var1)
        {
            return var1 == EnumOptions.MUSIC ? musicVolume : (var1 == EnumOptions.SOUND ? soundVolume : (var1 == EnumOptions.SENSITIVITY ? mouseSensitivity : 0.0F));
        }

        public bool getOptionOrdinalValue(EnumOptions var1)
        {
            switch (EnumOptionsMappingHelper.enumOptionsMappingHelperArray[var1.ordinal()])
            {
                case 1:
                    return invertMouse;
                case 2:
                    return viewBobbing;
                case 3:
                    return useMipmaps;
                case 4:
                    return debugMode;
                case 5:
                    return environmentAnimation;
                default:
                    return false;
            }
        }

        public string getKeyBinding(EnumOptions var1)
        {
            TranslationStorage var2 = TranslationStorage.getInstance();
            string var3 = var2.translateKey(var1.getEnumString()) + ": ";
            if (var1.getEnumFloat())
            {
                float var5 = getOptionFloatValue(var1);
                return var1 == EnumOptions.SENSITIVITY ? (var5 == 0.0F ? var3 + var2.translateKey("options.sensitivity.min") : (var5 == 1.0F ? var3 + var2.translateKey("options.sensitivity.max") : var3 + (int)(var5 * 200.0F) + "%")) : (var5 == 0.0F ? var3 + var2.translateKey("options.off") : var3 + (int)(var5 * 100.0F) + "%");
            }
            else if (var1.getEnumBoolean())
            {
                bool var4 = getOptionOrdinalValue(var1);
                return var4 ? var3 + var2.translateKey("options.on") : var3 + var2.translateKey("options.off");
            }
            else if (var1 == EnumOptions.MSAA)
            {
                string label = var3 + (msaaLevel == 0 ? var2.translateKey("options.off") : MSAA_LEVELS[msaaLevel]);
                if (msaaLevel != INITIAL_MSAA)
                {
                    label += " (Reload required)";
                }
                return label;
            }
            else
            {
                return var1 == EnumOptions.RENDER_DISTANCE ? var3 + var2.translateKey(RENDER_DISTANCES[renderDistance]) : (var1 == EnumOptions.DIFFICULTY ? var3 + var2.translateKey(DIFFICULTIES[difficulty]) : (var1 == EnumOptions.GUI_SCALE ? var3 + var2.translateKey(GUISCALES[guiScale]) : (var1 == EnumOptions.FRAMERATE_LIMIT ? var3 + StatCollector.translateToLocal(LIMIT_FRAMERATES[limitFramerate]) : (var1 == EnumOptions.ANISOTROPIC ? var3 + (anisotropicLevel == 0 ? var2.translateKey("options.off") : ANISO_LEVELS[anisotropicLevel]) : var3))));
            }
        }

        public void loadOptions()
        {
            try
            {
                if (!optionsFile.exists())
                {
                    return;
                }

                BufferedReader var1 = new(new FileReader(optionsFile));
                string var2 = "";

                while (true)
                {
                    var2 = var1.readLine();
                    if (var2 == null)
                    {
                        var1.close();
                        break;
                    }

                    try
                    {
                        string[] var3 = var2.Split(":");
                        if (var3[0].Equals("music"))
                        {
                            musicVolume = parseFloat(var3[1]);
                        }

                        if (var3[0].Equals("sound"))
                        {
                            soundVolume = parseFloat(var3[1]);
                        }

                        if (var3[0].Equals("mouseSensitivity"))
                        {
                            mouseSensitivity = parseFloat(var3[1]);
                        }

                        if (var3[0].Equals("invertYMouse"))
                        {
                            invertMouse = var3[1].Equals("true");
                        }

                        if (var3[0].Equals("viewDistance"))
                        {
                            renderDistance = Integer.parseInt(var3[1]);
                        }

                        if (var3[0].Equals("guiScale"))
                        {
                            guiScale = Integer.parseInt(var3[1]);
                        }

                        if (var3[0].Equals("bobView"))
                        {
                            viewBobbing = var3[1].Equals("true");
                        }

                        if (var3[0].Equals("fpsLimit"))
                        {
                            limitFramerate = Integer.parseInt(var3[1]);
                        }

                        if (var3[0].Equals("difficulty"))
                        {
                            difficulty = Integer.parseInt(var3[1]);
                        }

                        if (var3[0].Equals("skin"))
                        {
                            skin = var3[1];
                        }

                        if (var3[0].Equals("lastServer") && var3.Length >= 2)
                        {
                            lastServer = var3[1];
                        }

                        if (var3[0].Equals("anisotropicLevel"))
                        {
                            anisotropicLevel = Integer.parseInt(var3[1]);
                        }
                        if (var3[0].Equals("msaaLevel"))
                        {
                            msaaLevel = Integer.parseInt(var3[1]);
                            if (msaaLevel > 3) msaaLevel = 3;
                        }

                        if (var3[0].Equals("useMipmaps"))
                        {
                            useMipmaps = var3[1].Equals("true");
                        }

                        if (var3[0].Equals("debugMode"))
                        {
                            debugMode = var3[1].Equals("true");
                        }

                        if (var3[0].Equals("envAnimation"))
                        {
                            environmentAnimation = var3[1].Equals("true");
                        }

                        for (int var4 = 0; var4 < keyBindings.Length; ++var4)
                        {
                            if (var3[0].Equals("key_" + keyBindings[var4].keyDescription))
                            {
                                keyBindings[var4].keyCode = Integer.parseInt(var3[1]);
                            }
                        }
                    }
                    catch (java.lang.Exception var5)
                    {
                        java.lang.System.@out.println("Skipping bad option: " + var2);
                    }
                }
            }
            catch (java.lang.Exception var6)
            {
                java.lang.System.@out.println("Failed to load options");
                var6.printStackTrace();
            }

        }

        private float parseFloat(string var1)
        {
            return var1.Equals("true") ? 1.0F : (var1.Equals("false") ? 0.0F : Float.parseFloat(var1));
        }

        public void saveOptions()
        {
            try
            {
                PrintWriter var1 = new(new FileWriter(optionsFile));
                var1.println("music:" + musicVolume);
                var1.println("sound:" + soundVolume);
                var1.println("invertYMouse:" + invertMouse);
                var1.println("mouseSensitivity:" + mouseSensitivity);
                var1.println("viewDistance:" + renderDistance);
                var1.println("guiScale:" + guiScale);
                var1.println("bobView:" + viewBobbing);
                var1.println("fpsLimit:" + limitFramerate);
                var1.println("difficulty:" + difficulty);
                var1.println("skin:" + skin);
                var1.println("lastServer:" + lastServer);
                var1.println("anisotropicLevel:" + anisotropicLevel);
                var1.println("msaaLevel:" + msaaLevel);
                var1.println("useMipmaps:" + useMipmaps);
                var1.println("debugMode:" + debugMode);
                var1.println("envAnimation:" + environmentAnimation);

                for (int var2 = 0; var2 < keyBindings.Length; ++var2)
                {
                    var1.println("key_" + keyBindings[var2].keyDescription + ":" + keyBindings[var2].keyCode);
                }

                var1.close();
            }
            catch (java.lang.Exception var3)
            {
                java.lang.System.@out.println("Failed to save options");
                var3.printStackTrace();
            }

        }
    }
}