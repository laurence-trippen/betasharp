using betareborn.Blocks;
using betareborn.Client.Models;
using betareborn.Entities;
using betareborn.Items;
using betareborn.Util.Maths;
using Silk.NET.OpenGL.Legacy;

namespace betareborn.Client.Rendering
{
    public class RenderPlayer : RenderLiving
    {

        private ModelBiped modelBipedMain;
        private ModelBiped modelArmorChestplate = new ModelBiped(1.0F);
        private ModelBiped modelArmor = new ModelBiped(0.5F);
        private static readonly string[] armorFilenamePrefix = new string[] { "cloth", "chain", "iron", "diamond", "gold" };

        public RenderPlayer() : base(new ModelBiped(0.0F), 0.5F)
        {
            modelBipedMain = (ModelBiped)mainModel;
        }

        protected bool setArmorModel(EntityPlayer var1, int var2, float var3)
        {
            ItemStack var4 = var1.inventory.armorItemInSlot(3 - var2);
            if (var4 != null)
            {
                Item var5 = var4.getItem();
                if (var5 is ItemArmor)
                {
                    ItemArmor var6 = (ItemArmor)var5;
                    loadTexture("/armor/" + armorFilenamePrefix[var6.renderIndex] + "_" + (var2 == 2 ? 2 : 1) + ".png");
                    ModelBiped var7 = var2 == 2 ? modelArmor : modelArmorChestplate;
                    var7.bipedHead.visible = var2 == 0;
                    var7.bipedHeadwear.visible = var2 == 0;
                    var7.bipedBody.visible = var2 == 1 || var2 == 2;
                    var7.bipedRightArm.visible = var2 == 1;
                    var7.bipedLeftArm.visible = var2 == 1;
                    var7.bipedRightLeg.visible = var2 == 2 || var2 == 3;
                    var7.bipedLeftLeg.visible = var2 == 2 || var2 == 3;
                    setRenderPassModel(var7);
                    return true;
                }
            }

            return false;
        }

        public void renderPlayer(EntityPlayer var1, double var2, double var4, double var6, float var8, float var9)
        {
            ItemStack var10 = var1.inventory.getCurrentItem();
            modelArmorChestplate.field_1278_i = modelArmor.field_1278_i = modelBipedMain.field_1278_i = var10 != null;
            modelArmorChestplate.isSneak = modelArmor.isSneak = modelBipedMain.isSneak = var1.isSneaking();
            double var11 = var4 - var1.yOffset;
            if (var1.isSneaking() && !(var1 is ClientPlayerEntity))
            {
                var11 -= 0.125D;
            }

            base.doRenderLiving(var1, var2, var11, var6, var8, var9);
            modelArmorChestplate.isSneak = modelArmor.isSneak = modelBipedMain.isSneak = false;
            modelArmorChestplate.field_1278_i = modelArmor.field_1278_i = modelBipedMain.field_1278_i = false;
        }

        protected void renderName(EntityPlayer var1, double var2, double var4, double var6)
        {
            if (Minecraft.isGuiEnabled() && var1 != renderManager.livingPlayer)
            {
                float var8 = 1.6F;
                float var9 = (float)(1.0D / 60.0D) * var8;
                float var10 = var1.getDistanceToEntity(renderManager.livingPlayer);
                float var11 = var1.isSneaking() ? 32.0F : 64.0F;
                if (var10 < var11)
                {
                    string var12 = var1.username;
                    if (!var1.isSneaking())
                    {
                        if (var1.isSleeping())
                        {
                            renderLivingLabel(var1, var12, var2, var4 - 1.5D, var6, 64);
                        }
                        else
                        {
                            renderLivingLabel(var1, var12, var2, var4, var6, 64);
                        }
                    }
                    else
                    {
                        FontRenderer var13 = getFontRendererFromRenderManager();
                        GLManager.GL.PushMatrix();
                        GLManager.GL.Translate((float)var2 + 0.0F, (float)var4 + 2.3F, (float)var6);
                        GLManager.GL.Normal3(0.0F, 1.0F, 0.0F);
                        GLManager.GL.Rotate(-renderManager.playerViewY, 0.0F, 1.0F, 0.0F);
                        GLManager.GL.Rotate(renderManager.playerViewX, 1.0F, 0.0F, 0.0F);
                        GLManager.GL.Scale(-var9, -var9, var9);
                        GLManager.GL.Disable(GLEnum.Lighting);
                        GLManager.GL.Translate(0.0F, 0.25F / var9, 0.0F);
                        GLManager.GL.DepthMask(false);
                        GLManager.GL.Enable(GLEnum.Blend);
                        GLManager.GL.BlendFunc(GLEnum.SrcAlpha, GLEnum.OneMinusSrcAlpha);
                        Tessellator var14 = Tessellator.instance;
                        GLManager.GL.Disable(GLEnum.Texture2D);
                        var14.startDrawingQuads();
                        int var15 = var13.getStringWidth(var12) / 2;
                        var14.setColorRGBA_F(0.0F, 0.0F, 0.0F, 0.25F);
                        var14.addVertex(-var15 - 1, -1.0D, 0.0D);
                        var14.addVertex(-var15 - 1, 8.0D, 0.0D);
                        var14.addVertex(var15 + 1, 8.0D, 0.0D);
                        var14.addVertex(var15 + 1, -1.0D, 0.0D);
                        var14.draw();
                        GLManager.GL.Enable(GLEnum.Texture2D);
                        GLManager.GL.DepthMask(true);
                        var13.drawString(var12, -var13.getStringWidth(var12) / 2, 0, 553648127);
                        GLManager.GL.Enable(GLEnum.Lighting);
                        GLManager.GL.Disable(GLEnum.Blend);
                        GLManager.GL.Color4(1.0F, 1.0F, 1.0F, 1.0F);
                        GLManager.GL.PopMatrix();
                    }
                }
            }

        }

        protected void renderSpecials(EntityPlayer var1, float var2)
        {
            ItemStack var3 = var1.inventory.armorItemInSlot(3);
            if (var3 != null && var3.getItem().id < 256)
            {
                GLManager.GL.PushMatrix();
                modelBipedMain.bipedHead.postRender(1.0F / 16.0F);
                if (RenderBlocks.renderItemIn3d(Block.BLOCKS[var3.itemID].getRenderType()))
                {
                    float var4 = 10.0F / 16.0F;
                    GLManager.GL.Translate(0.0F, -0.25F, 0.0F);
                    GLManager.GL.Rotate(180.0F, 0.0F, 1.0F, 0.0F);
                    GLManager.GL.Scale(var4, -var4, var4);
                }

                renderManager.itemRenderer.renderItem(var1, var3);
                GLManager.GL.PopMatrix();
            }

            float var5;
            if (var1.username.Equals("deadmau5") && loadDownloadableImageTexture(var1.skinUrl, null))
            {
                for (int var19 = 0; var19 < 2; ++var19)
                {
                    var5 = var1.prevRotationYaw + (var1.rotationYaw - var1.prevRotationYaw) * var2 - (var1.prevRenderYawOffset + (var1.renderYawOffset - var1.prevRenderYawOffset) * var2);
                    float var6 = var1.prevRotationPitch + (var1.rotationPitch - var1.prevRotationPitch) * var2;
                    GLManager.GL.PushMatrix();
                    GLManager.GL.Rotate(var5, 0.0F, 1.0F, 0.0F);
                    GLManager.GL.Rotate(var6, 1.0F, 0.0F, 0.0F);
                    GLManager.GL.Translate(6.0F / 16.0F * (var19 * 2 - 1), 0.0F, 0.0F);
                    GLManager.GL.Translate(0.0F, -(6.0F / 16.0F), 0.0F);
                    GLManager.GL.Rotate(-var6, 1.0F, 0.0F, 0.0F);
                    GLManager.GL.Rotate(-var5, 0.0F, 1.0F, 0.0F);
                    float var7 = 4.0F / 3.0F;
                    GLManager.GL.Scale(var7, var7, var7);
                    modelBipedMain.renderEars(1.0F / 16.0F);
                    GLManager.GL.PopMatrix();
                }
            }

            if (loadDownloadableImageTexture(var1.playerCloakUrl, null))
            {
                GLManager.GL.PushMatrix();
                GLManager.GL.Translate(0.0F, 0.0F, 2.0F / 16.0F);
                double var20 = var1.field_20066_r + (var1.field_20063_u - var1.field_20066_r) * (double)var2 - (var1.prevPosX + (var1.posX - var1.prevPosX) * (double)var2);
                double var22 = var1.field_20065_s + (var1.field_20062_v - var1.field_20065_s) * (double)var2 - (var1.prevPosY + (var1.posY - var1.prevPosY) * (double)var2);
                double var8 = var1.field_20064_t + (var1.field_20061_w - var1.field_20064_t) * (double)var2 - (var1.prevPosZ + (var1.posZ - var1.prevPosZ) * (double)var2);
                float var10 = var1.prevRenderYawOffset + (var1.renderYawOffset - var1.prevRenderYawOffset) * var2;
                double var11 = (double)MathHelper.sin(var10 * (float)Math.PI / 180.0F);
                double var13 = (double)-MathHelper.cos(var10 * (float)Math.PI / 180.0F);
                float var15 = (float)var22 * 10.0F;
                if (var15 < -6.0F)
                {
                    var15 = -6.0F;
                }

                if (var15 > 32.0F)
                {
                    var15 = 32.0F;
                }

                float var16 = (float)(var20 * var11 + var8 * var13) * 100.0F;
                float var17 = (float)(var20 * var13 - var8 * var11) * 100.0F;
                if (var16 < 0.0F)
                {
                    var16 = 0.0F;
                }

                float var18 = var1.prevStepBobbingAmount + (var1.stepBobbingAmount - var1.prevStepBobbingAmount) * var2;
                var15 += MathHelper.sin((var1.prevDistanceWalkedModified + (var1.distanceWalkedModified - var1.prevDistanceWalkedModified) * var2) * 6.0F) * 32.0F * var18;
                if (var1.isSneaking())
                {
                    var15 += 25.0F;
                }

                GLManager.GL.Rotate(6.0F + var16 / 2.0F + var15, 1.0F, 0.0F, 0.0F);
                GLManager.GL.Rotate(var17 / 2.0F, 0.0F, 0.0F, 1.0F);
                GLManager.GL.Rotate(-var17 / 2.0F, 0.0F, 1.0F, 0.0F);
                GLManager.GL.Rotate(180.0F, 0.0F, 1.0F, 0.0F);
                modelBipedMain.renderCloak(1.0F / 16.0F);
                GLManager.GL.PopMatrix();
            }

            ItemStack var21 = var1.inventory.getCurrentItem();
            if (var21 != null)
            {
                GLManager.GL.PushMatrix();
                modelBipedMain.bipedRightArm.postRender(1.0F / 16.0F);
                GLManager.GL.Translate(-(1.0F / 16.0F), 7.0F / 16.0F, 1.0F / 16.0F);
                if (var1.fishEntity != null)
                {
                    var21 = new ItemStack(Item.stick);
                }

                if (var21.itemID < 256 && RenderBlocks.renderItemIn3d(Block.BLOCKS[var21.itemID].getRenderType()))
                {
                    var5 = 0.5F;
                    GLManager.GL.Translate(0.0F, 3.0F / 16.0F, -(5.0F / 16.0F));
                    var5 *= 12.0F / 16.0F;
                    GLManager.GL.Rotate(20.0F, 1.0F, 0.0F, 0.0F);
                    GLManager.GL.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
                    GLManager.GL.Scale(var5, -var5, var5);
                }
                else if (Item.itemsList[var21.itemID].isFull3D())
                {
                    var5 = 10.0F / 16.0F;
                    if (Item.itemsList[var21.itemID].shouldRotateAroundWhenRendering())
                    {
                        GLManager.GL.Rotate(180.0F, 0.0F, 0.0F, 1.0F);
                        GLManager.GL.Translate(0.0F, -(2.0F / 16.0F), 0.0F);
                    }

                    GLManager.GL.Translate(0.0F, 3.0F / 16.0F, 0.0F);
                    GLManager.GL.Scale(var5, -var5, var5);
                    GLManager.GL.Rotate(-100.0F, 1.0F, 0.0F, 0.0F);
                    GLManager.GL.Rotate(45.0F, 0.0F, 1.0F, 0.0F);
                }
                else
                {
                    var5 = 6.0F / 16.0F;
                    GLManager.GL.Translate(0.25F, 3.0F / 16.0F, -(3.0F / 16.0F));
                    GLManager.GL.Scale(var5, var5, var5);
                    GLManager.GL.Rotate(60.0F, 0.0F, 0.0F, 1.0F);
                    GLManager.GL.Rotate(-90.0F, 1.0F, 0.0F, 0.0F);
                    GLManager.GL.Rotate(20.0F, 0.0F, 0.0F, 1.0F);
                }

                renderManager.itemRenderer.renderItem(var1, var21);
                GLManager.GL.PopMatrix();
            }

        }

        protected void func_186_b(EntityPlayer var1, float var2)
        {
            float var3 = 15.0F / 16.0F;
            GLManager.GL.Scale(var3, var3, var3);
        }

        public void drawFirstPersonHand()
        {
            modelBipedMain.onGround = 0.0F;
            modelBipedMain.setRotationAngles(0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 1.0F / 16.0F);
            modelBipedMain.bipedRightArm.render(1.0F / 16.0F);
        }

        protected void func_22016_b(EntityPlayer var1, double var2, double var4, double var6)
        {
            if (var1.isEntityAlive() && var1.isSleeping())
            {
                base.func_22012_b(var1, var2 + var1.sleepOffsetX, var4 + var1.sleepOffsetY, var6 + var1.sleepOffsetZ);
            }
            else
            {
                base.func_22012_b(var1, var2, var4, var6);
            }

        }

        protected void func_22017_a(EntityPlayer var1, float var2, float var3, float var4)
        {
            if (var1.isEntityAlive() && var1.isSleeping())
            {
                GLManager.GL.Rotate(var1.getSleepingRotation(), 0.0F, 1.0F, 0.0F);
                GLManager.GL.Rotate(getDeathMaxRotation(var1), 0.0F, 0.0F, 1.0F);
                GLManager.GL.Rotate(270.0F, 0.0F, 1.0F, 0.0F);
            }
            else
            {
                base.rotateCorpse(var1, var2, var3, var4);
            }

        }

        protected override void passSpecialRender(EntityLiving var1, double var2, double var4, double var6)
        {
            renderName((EntityPlayer)var1, var2, var4, var6);
        }

        protected override void preRenderCallback(EntityLiving var1, float var2)
        {
            func_186_b((EntityPlayer)var1, var2);
        }

        protected override bool shouldRenderPass(EntityLiving var1, int var2, float var3)
        {
            return setArmorModel((EntityPlayer)var1, var2, var3);
        }

        protected override void renderEquippedItems(EntityLiving var1, float var2)
        {
            renderSpecials((EntityPlayer)var1, var2);
        }

        protected override void rotateCorpse(EntityLiving var1, float var2, float var3, float var4)
        {
            func_22017_a((EntityPlayer)var1, var2, var3, var4);
        }

        protected override void func_22012_b(EntityLiving var1, double var2, double var4, double var6)
        {
            func_22016_b((EntityPlayer)var1, var2, var4, var6);
        }

        public override void doRenderLiving(EntityLiving var1, double var2, double var4, double var6, float var8, float var9)
        {
            renderPlayer((EntityPlayer)var1, var2, var4, var6, var8, var9);
        }

        public override void doRender(Entity var1, double var2, double var4, double var6, float var8, float var9)
        {
            renderPlayer((EntityPlayer)var1, var2, var4, var6, var8, var9);
        }
    }

}