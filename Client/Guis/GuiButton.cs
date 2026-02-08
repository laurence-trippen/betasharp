using Silk.NET.OpenGL.Legacy;

namespace betareborn.Client.Guis
{
    public class GuiButton : Gui
    {
        protected int width;
        protected int height;
        public int xPosition;
        public int yPosition;
        public string displayString;
        public int id;
        public bool enabled;
        public bool enabled2;

        public GuiButton(int _id, int xPos, int yPos, string var4) : this(_id, xPos, yPos, 200, 20, var4)
        {

        }

        public GuiButton(int _id, int xPos, int yPos, int wid, int hei, string displayStr)
        {
            width = 200;
            height = 20;
            enabled = true;
            enabled2 = true;
            id = _id;
            xPosition = xPos;
            yPosition = yPos;
            width = wid;
            height = hei;
            displayString = displayStr;
        }

        protected virtual int getHoverState(bool var1)
        {
            byte var2 = 1;
            if (!enabled)
            {
                var2 = 0;
            }
            else if (var1)
            {
                var2 = 2;
            }

            return var2;
        }

        public void drawButton(Minecraft var1, int var2, int var3)
        {
            if (enabled2)
            {
                FontRenderer var4 = var1.fontRenderer;
                GLManager.GL.BindTexture(GLEnum.Texture2D, (uint)var1.textureManager.getTexture("/gui/gui.png"));
                GLManager.GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
                bool var5 = var2 >= xPosition && var3 >= yPosition && var2 < xPosition + width && var3 < yPosition + height;
                int var6 = getHoverState(var5);
                drawTexturedModalRect(xPosition, yPosition, 0, 46 + var6 * 20, width / 2, height);
                drawTexturedModalRect(xPosition + width / 2, yPosition, 200 - width / 2, 46 + var6 * 20, width / 2, height);
                mouseDragged(var1, var2, var3);
                if (!enabled)
                {
                    drawCenteredString(var4, displayString, xPosition + width / 2, yPosition + (height - 8) / 2, -6250336);
                }
                else if (var5)
                {
                    drawCenteredString(var4, displayString, xPosition + width / 2, yPosition + (height - 8) / 2, 16777120);
                }
                else
                {
                    drawCenteredString(var4, displayString, xPosition + width / 2, yPosition + (height - 8) / 2, 14737632);
                }

            }
        }

        protected virtual void mouseDragged(Minecraft var1, int var2, int var3)
        {
        }

        public virtual void mouseReleased(int var1, int var2)
        {
        }

        public virtual bool mousePressed(Minecraft var1, int var2, int var3)
        {
            return enabled && var2 >= xPosition && var3 >= yPosition && var2 < xPosition + width && var3 < yPosition + height;
        }
    }
}