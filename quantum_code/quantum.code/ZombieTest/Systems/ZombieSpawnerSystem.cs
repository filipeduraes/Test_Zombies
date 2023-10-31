using Photon.Deterministic;
using Quantum.Collections;

namespace Quantum.ZombieTest.Systems
{
    public unsafe class ZombieSpawnerSystem : SystemMainThreadFilter<ZombieSpawnerSystem.Filter>
    {
        private BTRoot btRoot;
        private FP delayTime = 0;
        private FP timeSinceLastSpawn = 0;
        
        public override void OnInit(Frame frame)
        {
            delayTime = 0;
            timeSinceLastSpawn = 0;
        }

        public override void Update(Frame frame, ref Filter filter)
        {
            if (!frame.TryResolveList(filter.Spawner->instances, out QList<EntityRef> instances))
            {
                instances = frame.AllocateList(filter.Spawner->instances);
                filter.Spawner->instances = instances;
            }

            if (instances.Count > filter.Spawner->spawnCount)
                return;

            foreach (EntityRef instance in instances)
                BTManager.Update(frame, instance);
            
            delayTime += frame.DeltaTime;

            if (delayTime >= filter.Spawner->spawnInitialDelay)
            {
                timeSinceLastSpawn += frame.DeltaTime;

                if (timeSinceLastSpawn >= filter.Spawner->spawnRate)
                {
                    SpawnAgent(frame, filter.Spawner, instances);
                    timeSinceLastSpawn = 0;
                }
            }
        }

        private void SpawnAgent(Frame frame, ZombieSpawner* spawner, QList<EntityRef> instances)
        {
            EntityRef newAgent = frame.Create(spawner->prefab);
            
            if (frame.Unsafe.TryGetPointer(newAgent, out BTAgent* agent))
            {
                btRoot ??= frame.FindAsset<BTRoot>(agent->Tree.Id);
                BTManager.Init(frame, newAgent, btRoot);
                
                instances.Add(newAgent);

                if (frame.Unsafe.TryGetPointer(newAgent, out Transform3D* agentTransform))
                {
                    QList<FPVector3> spawnPoints = frame.ResolveList(spawner->spawnPoints);
                    FPVector3 spawnPoint = spawnPoints.GetRandom(frame.RNG);
                    agentTransform->Position = spawnPoint;
                }
            }
        }

        public struct Filter
        {
            public EntityRef Entity;
            public ZombieSpawner* Spawner;
        }
    }
}