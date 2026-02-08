using betareborn.Client.Resource.Language;
using System.Diagnostics;

namespace betareborn.Client.Guis
{
    public class GuiTexturePacks : GuiScreen
    {

        protected GuiScreen guiScreen;
        private int field_6454_o = -1;
        private string fileLocation = "";
        private GuiTexturePackSlot guiTexturePackSlot;

        public GuiTexturePacks(GuiScreen var1)
        {
            guiScreen = var1;
        }

        public override void initGui()
        {
            TranslationStorage var1 = TranslationStorage.getInstance();
            controlList.add(new GuiSmallButton(5, width / 2 - 154, height - 48, var1.translateKey("texturePack.openFolder")));
            controlList.add(new GuiSmallButton(6, width / 2 + 4, height - 48, var1.translateKey("gui.done")));
            mc.texturePackList.updateAvaliableTexturePacks();
            fileLocation = new java.io.File(Minecraft.getMinecraftDir(), "texturepacks").getAbsolutePath();
            guiTexturePackSlot = new GuiTexturePackSlot(this);
            guiTexturePackSlot.registerScrollButtons(controlList, 7, 8);
        }

        protected override void actionPerformed(GuiButton var1)
        {
            if (var1.enabled)
            {
                if (var1.id == 5)
                {
                    //THIS SHOULD WORK THE SAME?
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "file://" + fileLocation,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to open URL: {ex.Message}");
                    }
                }
                else if (var1.id == 6)
                {
                    mc.textureManager.refreshTextures();
                    mc.displayGuiScreen(guiScreen);
                }
                else
                {
                    guiTexturePackSlot.actionPerformed(var1);
                }

            }
        }

        protected override void mouseClicked(int var1, int var2, int var3)
        {
            base.mouseClicked(var1, var2, var3);
        }

        protected override void mouseMovedOrUp(int var1, int var2, int var3)
        {
            base.mouseMovedOrUp(var1, var2, var3);
        }

        public override void drawScreen(int var1, int var2, float var3)
        {
            guiTexturePackSlot.drawScreen(var1, var2, var3);
            if (field_6454_o <= 0)
            {
                mc.texturePackList.updateAvaliableTexturePacks();
                field_6454_o += 20;
            }

            TranslationStorage var4 = TranslationStorage.getInstance();
            drawCenteredString(fontRenderer, var4.translateKey("texturePack.title"), width / 2, 16, 16777215);
            drawCenteredString(fontRenderer, var4.translateKey("texturePack.folderInfo"), width / 2 - 77, height - 26, 8421504);
            base.drawScreen(var1, var2, var3);
        }

        public override void updateScreen()
        {
            base.updateScreen();
            --field_6454_o;
        }

        public static Minecraft func_22124_a(GuiTexturePacks var0)
        {
            return var0.mc;
        }

        public static Minecraft func_22126_b(GuiTexturePacks var0)
        {
            return var0.mc;
        }

        public static Minecraft func_22119_c(GuiTexturePacks var0)
        {
            return var0.mc;
        }

        public static Minecraft func_22122_d(GuiTexturePacks var0)
        {
            return var0.mc;
        }

        public static Minecraft func_22117_e(GuiTexturePacks var0)
        {
            return var0.mc;
        }

        public static Minecraft func_22118_f(GuiTexturePacks var0)
        {
            return var0.mc;
        }

        public static Minecraft func_22116_g(GuiTexturePacks var0)
        {
            return var0.mc;
        }

        public static Minecraft func_22121_h(GuiTexturePacks var0)
        {
            return var0.mc;
        }

        public static Minecraft func_22123_i(GuiTexturePacks var0)
        {
            return var0.mc;
        }

        public static FontRenderer func_22127_j(GuiTexturePacks var0)
        {
            return var0.fontRenderer;
        }

        public static FontRenderer func_22120_k(GuiTexturePacks var0)
        {
            return var0.fontRenderer;
        }

        public static FontRenderer func_22125_l(GuiTexturePacks var0)
        {
            return var0.fontRenderer;
        }
    }

}