using betareborn.Biomes;
using betareborn.Blocks;
using betareborn.Chunks;
using Silk.NET.Maths;

namespace betareborn.Worlds
{
    public class WorldProviderHell : WorldProvider
    {
        public override void registerWorldChunkManager()
        {
            worldChunkMgr = new WorldChunkManagerHell(Biome.HELL, 1.0D, 0.0D);
            isNether = true;
            isHellWorld = true;
            hasNoSky = true;
            worldType = -1;
        }

        public override Vector3D<double> func_4096_a(float var1, float var2)
        {
            return new((double)0.2F, (double)0.03F, (double)0.03F);
        }

        protected override void generateLightBrightnessTable()
        {
            float var1 = 0.1F;

            for (int var2 = 0; var2 <= 15; ++var2)
            {
                float var3 = 1.0F - (float)var2 / 15.0F;
                lightBrightnessTable[var2] = (1.0F - var3) / (var3 * 3.0F + 1.0F) * (1.0F - var1) + var1;
            }

        }

        public override ChunkSource getChunkProvider()
        {
            return new NetherChunkGenerator(worldObj, worldObj.getSeed());
        }

        public override bool canCoordinateBeSpawn(int var1, int var2)
        {
            int var3 = worldObj.getFirstUncoveredBlock(var1, var2);
            return var3 == Block.BEDROCK.id ? false : (var3 == 0 ? false : Block.BLOCKS_OPAQUE[var3]);
        }

        public override float calculateCelestialAngle(long var1, float var3)
        {
            return 0.5F;
        }

        public override bool hasWorldSpawn()
        {
            return false;
        }
    }

}