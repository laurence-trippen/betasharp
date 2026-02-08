using betareborn.Blocks.BlockEntities;
using betareborn.Inventorys;
using betareborn.Screens;

namespace betareborn.Client.Guis
{
    public class GuiDispenser : GuiContainer
    {

        public GuiDispenser(InventoryPlayer var1, BlockEntityDispenser var2) : base(new DispenserScreenHandler(var1, var2))
        {
        }

        protected override void drawGuiContainerForegroundLayer()
        {
            fontRenderer.drawString("Dispenser", 60, 6, 4210752);
            fontRenderer.drawString("Inventory", 8, ySize - 96 + 2, 4210752);
        }

        protected override void drawGuiContainerBackgroundLayer(float var1)
        {
            int var2 = mc.textureManager.getTexture("/gui/trap.png");
            GLManager.GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
            mc.textureManager.bindTexture(var2);
            int var3 = (width - xSize) / 2;
            int var4 = (height - ySize) / 2;
            drawTexturedModalRect(var3, var4, 0, 0, xSize, ySize);
        }
    }

}