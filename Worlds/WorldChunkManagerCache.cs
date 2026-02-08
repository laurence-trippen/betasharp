using betareborn.Worlds.Biomes.Source;
using System.Collections.Concurrent;

namespace betareborn.Worlds
{
    public sealed class WorldChunkManagerCache
    {
        private readonly ConcurrentDictionary<int, BiomeSource> _perThread
            = new();

        public BiomeSource get(World world)
        {
            int tid = Environment.CurrentManagedThreadId;

            return _perThread.GetOrAdd(
                tid,
                _ => new BiomeSource(world)
            );
        }

        public void clear()
        {
            _perThread.Clear();
        }
    }
}