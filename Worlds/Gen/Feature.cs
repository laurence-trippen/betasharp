namespace betareborn.Worlds.Gen
{
    public abstract class Feature : java.lang.Object
    {
        public abstract bool generate(World world, java.util.Random random, int x, int y, int z);

        public virtual void prepare(double d0, double d1, double d2)
        {
        }
    }
}