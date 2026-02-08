using betareborn.Client.Resource;
using betareborn.Client.Resource.Language;
using betareborn.Util.Maths;
using java.io;
using java.util;
using Silk.NET.OpenGL.Legacy;

namespace betareborn.Client.Guis
{
    public class GuiMainMenu : GuiScreen
    {

        private static readonly java.util.Random rand = new();
        private string splashText = "missingno";
        private GuiButton multiplayerButton;

        public GuiMainMenu()
        {
            try
            {
                List<string> var1 = [];
                BufferedReader reader =
                    new(new java.io.StringReader(AssetManager.Instance.getAsset("title/splashes.txt")
                        .getTextContent()));
                string var3 = "";

                while (true)
                {
                    var3 = reader.readLine();
                    if (var3 == null)
                    {
                        splashText = var1[rand.nextInt(var1.Count)];
                        break;
                    }

                    var3 = var3.Trim();
                    if (var3.Length > 0)
                    {
                        var1.Add(var3);
                    }
                }
            }
            catch (Exception var4)
            {
            }
        }

        public override void updateScreen()
        {
        }

        protected override void keyTyped(char eventChar, int eventKey)
        {
        }

        public override void initGui()
        {
            Calendar calendar = Calendar.getInstance();

            // Special days
            calendar.setTime(new Date());
            if (calendar.get(2) + 1 == 11 && calendar.get(5) == 9)
            {
                splashText = "Happy birthday, ez!";
            }
            else if (calendar.get(2) + 1 == 6 && calendar.get(5) == 1)
            {
                splashText = "Happy birthday, Notch!";
            }
            else if (calendar.get(2) + 1 == 12 && calendar.get(5) == 24)
            {
                splashText = "Merry X-mas!";
            }
            else if (calendar.get(2) + 1 == 1 && calendar.get(5) == 1)
            {
                splashText = "Happy new year!";
            }

            TranslationStorage translator = TranslationStorage.getInstance();
            int var4 = height / 4 + 48;

            controlList.add(new GuiButton(1, width / 2 - 100, var4, translator.translateKey("menu.singleplayer")));
            controlList.add(multiplayerButton =
                new GuiButton(2, width / 2 - 100, var4 + 24, translator.translateKey("menu.multiplayer")));
            controlList.add(new GuiButton(3, width / 2 - 100, var4 + 48, translator.translateKey("menu.mods")));

            if (mc.hideQuitButton)
            {
                controlList.add(new GuiButton(0, width / 2 - 100, var4 + 72, translator.translateKey("menu.options")));
            }
            else
            {
                controlList.add(new GuiButton(0, width / 2 - 100, var4 + 72 + 12, 98, 20,
                    translator.translateKey("menu.options")));

                controlList.add(new GuiButton(4, width / 2 + 2, var4 + 72 + 12, 98, 20,
                    translator.translateKey("menu.quit")));
            }

            if (mc.session == null)
            {
                multiplayerButton.enabled = false;
            }
        }

        protected override void actionPerformed(GuiButton var1)
        {
            if (var1.id == 0)
            {
                mc.displayGuiScreen(new GuiOptions(this, mc.gameSettings));
            }

            if (var1.id == 1)
            {
                mc.displayGuiScreen(new GuiSelectWorld(this));
            }

            if (var1.id == 2)
            {
                mc.displayGuiScreen(new GuiMultiplayer(this));
            }

            if (var1.id == 3)
            {
                mc.displayGuiScreen(new GuiTexturePacks(this));
            }

            if (var1.id == 4)
            {
                mc.shutdown();
            }
        }

        public override void drawScreen(int var1, int var2, float var3)
        {
            drawDefaultBackground();
            Tessellator var4 = Tessellator.instance;
            short var5 = 274;
            int var6 = width / 2 - var5 / 2;
            byte var7 = 30;
            GLManager.GL.BindTexture(GLEnum.Texture2D, (uint)mc.textureManager.getTexture("/title/mclogo.png"));
            GLManager.GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
            drawTexturedModalRect(var6 + 0, var7 + 0, 0, 0, 155, 44);
            drawTexturedModalRect(var6 + 155, var7 + 0, 0, 45, 155, 44);
            var4.setColorOpaque_I(16777215);
            GLManager.GL.PushMatrix();
            GLManager.GL.Translate(width / 2 + 90, 70.0F, 0.0F);
            GLManager.GL.Rotate(-20.0F, 0.0F, 0.0F, 1.0F);
            float var8 = 1.8F - MathHelper.abs(MathHelper.sin(java.lang.System.currentTimeMillis() % 1000L /
                1000.0F * (float)Math.PI * 2.0F) * 0.1F);
            var8 = var8 * 100.0F / (fontRenderer.getStringWidth(splashText) + 32);
            GLManager.GL.Scale(var8, var8, var8);
            drawCenteredString(fontRenderer, splashText, 0, -8, 16776960);
            GLManager.GL.PopMatrix();
            drawString(fontRenderer, "Minecraft Beta 1.7.3", 2, 2, 5263440);
            string var9 = "Copyright Mojang Studios. Not an official Minecraft product.";
            drawString(fontRenderer, var9, width - fontRenderer.getStringWidth(var9) - 2, height - 20, 16777215);
            string var10 = "Not approved by or associated with Mojang Studios or Microsoft.";
            drawString(fontRenderer, var10, width - fontRenderer.getStringWidth(var10) - 2, height - 10, 16777215);
            base.drawScreen(var1, var2, var3);
        }
    }

}