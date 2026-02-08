using betareborn.Blocks;
using betareborn.Entities;
using betareborn.Items;
using betareborn.Worlds;

namespace betareborn
{
    public class PlayerControllerTest : PlayerController
    {

        public PlayerControllerTest(Minecraft var1) : base(var1)
        {
            field_1064_b = true;
        }

        public override void func_6473_b(EntityPlayer var1)
        {
            for (int var2 = 0; var2 < 9; ++var2)
            {
                if (var1.inventory.mainInventory[var2] == null)
                {
                    mc.player.inventory.mainInventory[var2] = new ItemStack((Block)Session.registeredBlocksList.get(var2));
                }
                else
                {
                    mc.player.inventory.mainInventory[var2].count = 1;
                }
            }

        }

        public override bool shouldDrawHUD()
        {
            return false;
        }

        public override void func_717_a(World var1)
        {
            base.func_717_a(var1);
        }

        public override void updateController()
        {
        }
    }

}