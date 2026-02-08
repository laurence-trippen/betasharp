using betareborn.Inventorys;
using betareborn.Screens;
using betareborn.Worlds;

namespace betareborn.Client.Guis
{
    public class GuiCrafting : GuiContainer
    {

        public GuiCrafting(InventoryPlayer var1, World var2, int var3, int var4, int var5) : base(new CraftingScreenHandler(var1, var2, var3, var4, var5))
        {
        }

        public override void onGuiClosed()
        {
            base.onGuiClosed();
            inventorySlots.onClosed(mc.player);
        }

        protected override void drawGuiContainerForegroundLayer()
        {
            fontRenderer.drawString("Crafting", 28, 6, 4210752);
            fontRenderer.drawString("Inventory", 8, ySize - 96 + 2, 4210752);
        }

        protected override void drawGuiContainerBackgroundLayer(float var1)
        {
            int var2 = mc.textureManager.getTexture("/gui/crafting.png");
            GLManager.GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
            mc.textureManager.bindTexture(var2);
            int var3 = (width - xSize) / 2;
            int var4 = (height - ySize) / 2;
            drawTexturedModalRect(var3, var4, 0, 0, xSize, ySize);
        }
    }

}