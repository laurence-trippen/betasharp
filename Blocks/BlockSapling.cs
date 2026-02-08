using betareborn.Worlds;
using betareborn.Worlds.Gen;

namespace betareborn.Blocks
{
    public class BlockSapling : BlockPlant
    {
        public BlockSapling(int i, int j) : base(i, j)
        {
            float var3 = 0.4F;
            setBoundingBox(0.5F - var3, 0.0F, 0.5F - var3, 0.5F + var3, var3 * 2.0F, 0.5F + var3);
        }

        public override void onTick(World world, int x, int y, int z, java.util.Random random)
        {
            if (!world.isRemote)
            {
                base.onTick(world, x, y, z, random);
                if (world.getLightLevel(x, y + 1, z) >= 9 && random.nextInt(30) == 0)
                {
                    int var6 = world.getBlockMeta(x, y, z);
                    if ((var6 & 8) == 0)
                    {
                        world.setBlockMeta(x, y, z, var6 | 8);
                    }
                    else
                    {
                        generate(world, x, y, z, random);
                    }
                }

            }
        }

        public override int getTexture(int side, int meta)
        {
            meta &= 3;
            return meta == 1 ? 63 : (meta == 2 ? 79 : base.getTexture(side, meta));
        }

        public void generate(World world, int x, int y, int z, java.util.Random random)
        {
            int var6 = world.getBlockMeta(x, y, z) & 3;
            world.setBlock(x, y, z, 0);
            object var7 = null;
            if (var6 == 1)
            {
                var7 = new SpruceTreeFeature();
            }
            else if (var6 == 2)
            {
                var7 = new BirchTreeFeature();
            }
            else
            {
                var7 = new OakTreeFeature();
                if (random.nextInt(10) == 0)
                {
                    var7 = new LargeOakTreeFeature();
                }
            }

            if (!((Feature)var7).generate(world, random, x, y, z))
            {
                world.setBlockAndMetadata(x, y, z, id, var6);
            }

        }

        protected override int getDroppedItemMeta(int blockMeta)
        {
            return blockMeta & 3;
        }
    }

}