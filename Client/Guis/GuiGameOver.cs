using betareborn.Worlds;

namespace betareborn.Client.Guis
{
    public class GuiGameOver : GuiScreen
    {

        public override void initGui()
        {
            controlList.clear();
            controlList.add(new GuiButton(1, width / 2 - 100, height / 4 + 72, "Respawn"));
            controlList.add(new GuiButton(2, width / 2 - 100, height / 4 + 96, "Title menu"));
            if (mc.session == null)
            {
                ((GuiButton)controlList.get(1)).enabled = false;
            }

        }

        protected override void keyTyped(char eventChar, int eventKey)
        {
        }

        protected override void actionPerformed(GuiButton var1)
        {
            if (var1.id == 0)
            {
            }

            if (var1.id == 1)
            {
                mc.player.respawn();
                mc.displayGuiScreen(null);
            }

            if (var1.id == 2)
            {
                mc.changeWorld1(null);
                mc.displayGuiScreen(new GuiMainMenu());
            }

        }

        public override void drawScreen(int var1, int var2, float var3)
        {
            drawGradientRect(0, 0, width, height, 1615855616, -1602211792);
            GLManager.GL.PushMatrix();
            GLManager.GL.Scale(2.0F, 2.0F, 2.0F);
            drawCenteredString(fontRenderer, "Game over!", width / 2 / 2, 30, 16777215);
            GLManager.GL.PopMatrix();
            drawCenteredString(fontRenderer, "Score: &e" + mc.player.getScore(), width / 2, 100, 16777215);
            base.drawScreen(var1, var2, var3);
        }

        public override bool doesGuiPauseGame()
        {
            return false;
        }
    }

}