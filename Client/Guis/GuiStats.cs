using betareborn.Client.Rendering;
using betareborn.Client.Resource.Language;
using betareborn.Items;
using betareborn.Stats;
using java.util;
using Silk.NET.OpenGL.Legacy;

namespace betareborn.Client.Guis
{
    public class GuiStats : GuiScreen
    {

        private static RenderItem field_27153_j = new RenderItem();
        protected GuiScreen field_27152_a;
        protected string field_27154_i = "Select world";
        private GuiSlotStatsGeneral field_27151_l;
        private GuiSlotStatsItem field_27150_m;
        private GuiSlotStatsBlock field_27157_n;
        private StatFileWriter field_27156_o;
        private GuiSlot field_27155_p = null;

        public GuiStats(GuiScreen var1, StatFileWriter var2)
        {
            field_27152_a = var1;
            field_27156_o = var2;
        }

        public override void initGui()
        {
            field_27154_i = StatCollector.translateToLocal("gui.stats");
            field_27151_l = new GuiSlotStatsGeneral(this);
            field_27151_l.registerScrollButtons(controlList, 1, 1);
            field_27150_m = new GuiSlotStatsItem(this);
            field_27150_m.registerScrollButtons(controlList, 1, 1);
            field_27157_n = new GuiSlotStatsBlock(this);
            field_27157_n.registerScrollButtons(controlList, 1, 1);
            field_27155_p = field_27151_l;
            func_27130_k();
        }

        public void func_27130_k()
        {
            TranslationStorage var1 = TranslationStorage.getInstance();
            controlList.add(new GuiButton(0, width / 2 + 4, height - 28, 150, 20, var1.translateKey("gui.done")));
            controlList.add(new GuiButton(1, width / 2 - 154, height - 52, 100, 20, var1.translateKey("stat.generalButton")));
            List var10000 = controlList;
            GuiButton var2 = new GuiButton(2, width / 2 - 46, height - 52, 100, 20, var1.translateKey("stat.blocksButton"));
            var10000.add(var2);
            var10000 = controlList;
            GuiButton var3 = new GuiButton(3, width / 2 + 62, height - 52, 100, 20, var1.translateKey("stat.itemsButton"));
            var10000.add(var3);
            if (field_27157_n.getSize() == 0)
            {
                var2.enabled = false;
            }

            if (field_27150_m.getSize() == 0)
            {
                var3.enabled = false;
            }

        }

        protected override void actionPerformed(GuiButton var1)
        {
            if (var1.enabled)
            {
                if (var1.id == 0)
                {
                    mc.displayGuiScreen(field_27152_a);
                }
                else if (var1.id == 1)
                {
                    field_27155_p = field_27151_l;
                }
                else if (var1.id == 3)
                {
                    field_27155_p = field_27150_m;
                }
                else if (var1.id == 2)
                {
                    field_27155_p = field_27157_n;
                }
                else
                {
                    field_27155_p.actionPerformed(var1);
                }

            }
        }

        public override void drawScreen(int var1, int var2, float var3)
        {
            field_27155_p.drawScreen(var1, var2, var3);
            drawCenteredString(fontRenderer, field_27154_i, width / 2, 20, 16777215);
            base.drawScreen(var1, var2, var3);
        }

        private void func_27138_c(int var1, int var2, int var3)
        {
            func_27147_a(var1 + 1, var2 + 1);
            GLManager.GL.Enable(GLEnum.RescaleNormal);
            GLManager.GL.PushMatrix();
            GLManager.GL.Rotate(180.0F, 1.0F, 0.0F, 0.0F);
            RenderHelper.enableStandardItemLighting();
            GLManager.GL.PopMatrix();
            field_27153_j.drawItemIntoGui(fontRenderer, mc.textureManager, var3, 0, Item.itemsList[var3].getIconFromDamage(0), var1 + 2, var2 + 2);
            RenderHelper.disableStandardItemLighting();
            GLManager.GL.Disable(GLEnum.RescaleNormal);
        }

        private void func_27147_a(int var1, int var2)
        {
            func_27136_c(var1, var2, 0, 0);
        }

        private void func_27136_c(int var1, int var2, int var3, int var4)
        {
            int var5 = mc.textureManager.getTexture("/gui/slot.png");
            GLManager.GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
            mc.textureManager.bindTexture(var5);
            Tessellator var10 = Tessellator.instance;
            var10.startDrawingQuads();
            var10.addVertexWithUV(var1 + 0, var2 + 18, zLevel, (double)((var3 + 0) * 0.0078125F), (double)((var4 + 18) * 0.0078125F));
            var10.addVertexWithUV(var1 + 18, var2 + 18, zLevel, (double)((var3 + 18) * 0.0078125F), (double)((var4 + 18) * 0.0078125F));
            var10.addVertexWithUV(var1 + 18, var2 + 0, zLevel, (double)((var3 + 18) * 0.0078125F), (double)((var4 + 0) * 0.0078125F));
            var10.addVertexWithUV(var1 + 0, var2 + 0, zLevel, (double)((var3 + 0) * 0.0078125F), (double)((var4 + 0) * 0.0078125F));
            var10.draw();
        }

        public static Minecraft func_27141_a(GuiStats var0)
        {
            return var0.mc;
        }

        public static FontRenderer func_27145_b(GuiStats var0)
        {
            return var0.fontRenderer;
        }

        public static StatFileWriter func_27142_c(GuiStats var0)
        {
            return var0.field_27156_o;
        }

        public static FontRenderer func_27140_d(GuiStats var0)
        {
            return var0.fontRenderer;
        }

        public static FontRenderer func_27146_e(GuiStats var0)
        {
            return var0.fontRenderer;
        }

        public static Minecraft func_27143_f(GuiStats var0)
        {
            return var0.mc;
        }

        public static void func_27128_a(GuiStats var0, int var1, int var2, int var3, int var4)
        {
            var0.func_27136_c(var1, var2, var3, var4);
        }

        public static Minecraft func_27149_g(GuiStats var0)
        {
            return var0.mc;
        }

        public static FontRenderer func_27133_h(GuiStats var0)
        {
            return var0.fontRenderer;
        }

        public static FontRenderer func_27137_i(GuiStats var0)
        {
            return var0.fontRenderer;
        }

        public static FontRenderer func_27132_j(GuiStats var0)
        {
            return var0.fontRenderer;
        }

        public static FontRenderer func_27134_k(GuiStats var0)
        {
            return var0.fontRenderer;
        }

        public static FontRenderer func_27139_l(GuiStats var0)
        {
            return var0.fontRenderer;
        }

        public static void func_27129_a(GuiStats var0, int var1, int var2, int var3, int var4, int var5, int var6)
        {
            var0.drawGradientRect(var1, var2, var3, var4, var5, var6);
        }

        public static FontRenderer func_27144_m(GuiStats var0)
        {
            return var0.fontRenderer;
        }

        public static FontRenderer func_27127_n(GuiStats var0)
        {
            return var0.fontRenderer;
        }

        public static void func_27135_b(GuiStats var0, int var1, int var2, int var3, int var4, int var5, int var6)
        {
            var0.drawGradientRect(var1, var2, var3, var4, var5, var6);
        }

        public static FontRenderer func_27131_o(GuiStats var0)
        {
            return var0.fontRenderer;
        }

        public static void func_27148_a(GuiStats var0, int var1, int var2, int var3)
        {
            var0.func_27138_c(var1, var2, var3);
        }
    }

}