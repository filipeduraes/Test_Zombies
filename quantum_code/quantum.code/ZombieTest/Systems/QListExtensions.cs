using Photon.Deterministic;
using Quantum.Collections;

namespace Quantum.ZombieTest.Systems
{
    public static unsafe class QListExtensions
    {
        public static T GetRandom<T>(this QList<T> list, RNGSession* range) where T : unmanaged
        {
            int randomIndex = range->Next(0, list.Count);
            return list[randomIndex];
        }
    }
}