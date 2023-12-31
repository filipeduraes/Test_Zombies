﻿using Photon.Deterministic;

namespace Quantum
{
    public unsafe class EntitySpawnSystem : SystemMainThread
    {
        public override void Update(Frame f)
        {
            foreach (var (entity, spawner) in f.Unsafe.GetComponentBlockIterator<EntitySpawner>())
            {
                Log.Debug($"Spawner entity value {entity}");

                CheckSpawnedList(f, spawner);
                
                if (spawner->NextSpawn > FP._0) continue;
                
                SpawnEntity(f, spawner, entity);
                ResetTimer(f, spawner);
            }
        }

        private static void CheckSpawnedList(Frame f, EntitySpawner* spawner)
        {
            var l = f.ResolveList(spawner->Spawned);
            for (int i = 0; i < l.Count; i++)
            {
                if (f.Exists(l[i])) continue;
                
                l.RemoveAt(i);
                i--;
            }
        }
        
        private static void SpawnEntity(Frame f, EntitySpawner* spawner, EntityRef spawnerEntity)
        {
            var l = f.ResolveList(spawner->Spawned);
            if (l.Count >= spawner->MaxSpawnAmount) return;

            var lp = f.ResolveList(spawner->EntityPrototypes);
            var spawnedEntity = f.Create(lp[f.RNG->Next(0, lp.Count)]);

            var entityTransform = f.Unsafe.GetPointer<Transform3D>(spawnedEntity);
            var spawnerPosition = f.Unsafe.GetPointer<Transform3D>(spawnerEntity)->Position;

            var posX = spawnerPosition.X + f.RNG->Next(FP._0, spawner->SpawnRadius);
            var posZ = spawnerPosition.Z + f.RNG->Next(FP._0, spawner->SpawnRadius);
            
            entityTransform->Position = new FPVector3(posX, entityTransform->Position.Y, posZ);
            
            l.Add(spawnedEntity);
        }

        private static void ResetTimer(Frame f, EntitySpawner* spawner)
        {
            spawner->NextSpawn = f.RNG->Next(spawner->SpawnIntervalMin, spawner->SpawnIntervalMax);
        }
    }
}