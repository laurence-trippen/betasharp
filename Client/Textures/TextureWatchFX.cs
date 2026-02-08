using betareborn.Client.Resource;
using betareborn.Items;
using java.awt.image;
using java.io;
using javax.imageio;

namespace betareborn.Client.Textures
{
    public class TextureWatchFX : TextureFX
    {

        private Minecraft mc;
        private int[] watchIconImageData = new int[256];
        private int[] dialImageData = new int[256];
        private double field_4222_j;
        private double field_4221_k;

        public TextureWatchFX(Minecraft var1) : base(Item.pocketSundial.getIconFromDamage(0))
        {
            mc = var1;
            tileImage = FXImage.Items;

            try
            {
                BufferedImage var2 = ImageIO.read(new ByteArrayInputStream(AssetManager.Instance.getAsset("gui/items.png").getBinaryContent()));
                int var3 = iconIndex % 16 * 16;
                int var4 = iconIndex / 16 * 16;
                var2.getRGB(var3, var4, 16, 16, watchIconImageData, 0, 16);
                var2 = ImageIO.read(new ByteArrayInputStream(AssetManager.Instance.getAsset("misc/dial.png").getBinaryContent()));
                var2.getRGB(0, 0, 16, 16, dialImageData, 0, 16);
            }
            catch (java.io.IOException var5)
            {
                var5.printStackTrace();
            }

        }

        public override void onTick()
        {
            double var1 = 0.0D;
            if (mc.world != null && mc.player != null)
            {
                float var3 = mc.world.getCelestialAngle(1.0F);
                var1 = (double)(-var3 * (float)Math.PI * 2.0F);
                if (mc.world.dimension.isNether)
                {
                    var1 = java.lang.Math.random() * (double)(float)Math.PI * 2.0D;
                }
            }

            double var22;
            for (var22 = var1 - field_4222_j; var22 < -Math.PI; var22 += Math.PI * 2.0D)
            {
            }

            while (var22 >= Math.PI)
            {
                var22 -= Math.PI * 2.0D;
            }

            if (var22 < -1.0D)
            {
                var22 = -1.0D;
            }

            if (var22 > 1.0D)
            {
                var22 = 1.0D;
            }

            field_4221_k += var22 * 0.1D;
            field_4221_k *= 0.8D;
            field_4222_j += field_4221_k;
            double var5 = java.lang.Math.sin(field_4222_j);
            double var7 = java.lang.Math.cos(field_4222_j);

            for (int var9 = 0; var9 < 256; ++var9)
            {
                int var10 = watchIconImageData[var9] >> 24 & 255;
                int var11 = watchIconImageData[var9] >> 16 & 255;
                int var12 = watchIconImageData[var9] >> 8 & 255;
                int var13 = watchIconImageData[var9] >> 0 & 255;
                if (var11 == var13 && var12 == 0 && var13 > 0)
                {
                    double var14 = -(var9 % 16 / 15.0D - 0.5D);
                    double var16 = var9 / 16 / 15.0D - 0.5D;
                    int var18 = var11;
                    int var19 = (int)((var14 * var7 + var16 * var5 + 0.5D) * 16.0D);
                    int var20 = (int)((var16 * var7 - var14 * var5 + 0.5D) * 16.0D);
                    int var21 = (var19 & 15) + (var20 & 15) * 16;
                    var10 = dialImageData[var21] >> 24 & 255;
                    var11 = (dialImageData[var21] >> 16 & 255) * var11 / 255;
                    var12 = (dialImageData[var21] >> 8 & 255) * var18 / 255;
                    var13 = (dialImageData[var21] >> 0 & 255) * var18 / 255;
                }

                if (anaglyphEnabled)
                {
                    int var23 = (var11 * 30 + var12 * 59 + var13 * 11) / 100;
                    int var15 = (var11 * 30 + var12 * 70) / 100;
                    int var24 = (var11 * 30 + var13 * 70) / 100;
                    var11 = var23;
                    var12 = var15;
                    var13 = var24;
                }

                imageData[var9 * 4 + 0] = (byte)var11;
                imageData[var9 * 4 + 1] = (byte)var12;
                imageData[var9 * 4 + 2] = (byte)var13;
                imageData[var9 * 4 + 3] = (byte)var10;
            }

        }
    }

}