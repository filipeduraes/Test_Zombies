using Photon.Deterministic;
using Quantum.Collections;
using Quantum.Physics3D;

namespace Quantum.ZombieTest.Systems
{
    public unsafe class ZombieSpawnerSystem : SystemMainThreadFilter<ZombieSpawnerSystem.Filter>, ISignalOnDeath
    {
        private QList<EntityRef> instances;
        private FP delayTime = 0;
        private FP timeSinceLastSpawn = 0;
        
        public override void OnInit(Frame frame)
        {
            delayTime = 0;
            timeSinceLastSpawn = 0;
        }

        public override void Update(Frame frame, ref Filter filter)
        {
            ResolveInstances(frame, filter);

            if (instances.Count > filter.Spawner->spawnCount)
            {
                foreach (EntityRef instance in instances)
                {
                    if (!instance.IsValid)
                        instances.Remove(instance);
                }
                return;
            }

            foreach (EntityRef instance in instances)
            {
                if(frame.Unsafe.TryGetPointer(instance, out AIBlackboardComponent* blackboard))
                    BTManager.Update(frame, instance, blackboard);
            }

            CheckSpawn(frame, filter);
        }
        
        private void ResolveInstances(Frame frame, Filter filter)
        {
            if (!frame.TryResolveList(filter.Spawner->instances, out instances))
            {
                instances = frame.AllocateList(filter.Spawner->instances);
                filter.Spawner->instances = instances;
            }
        }
        
        private void CheckSpawn(Frame frame, Filter filter)
        {
            if (delayTime >= filter.Spawner->spawnInitialDelay)
            {
                timeSinceLastSpawn += frame.DeltaTime;

                if (timeSinceLastSpawn >= filter.Spawner->spawnRate)
                {
                    SpawnAgent(frame, filter.Spawner);
                    timeSinceLastSpawn = 0;
                }
            }
            else
            {
                delayTime += frame.DeltaTime;
            }
        }

        private void SpawnAgent(Frame frame, ZombieSpawner* spawner)
        {
            EntityRef newAgent = frame.Create(spawner->prefab);
            
            if (frame.Unsafe.TryGetPointer(newAgent, out BTAgent* agent))
            {
                InitializeBehaviorTree(frame, agent, newAgent);
                InitializeBlackboard(frame, newAgent);
                InitializeNavmesh(frame, spawner->navmeshAgentConfig, spawner->navmeshAsset, newAgent);
                InitializeHealth(frame, spawner, newAgent);
                SetRandomPosition(frame, spawner, newAgent);

                instances.Add(newAgent);
                
                #if DEBUG
                BotSDKDebuggerSystem.AddToDebugger(newAgent);
                #endif
            }
        }

        private static void InitializeHealth(Frame frame, ZombieSpawner* spawner, EntityRef newAgent)
        {
            if(frame.TryAddComponent(newAgent, out Damageable* damageable))
            {
                damageable->MaxHealth = spawner->health;
                damageable->CurrentHealth = spawner->health;
            }
        }

        private static void InitializeBehaviorTree(Frame frame, BTAgent* agent, EntityRef newAgent)
        {
            BTRoot btRoot = frame.FindAsset<BTRoot>(agent->Tree.Id);
            BTManager.Init(frame, newAgent, btRoot);
        }

        private static void InitializeBlackboard(Frame frame, EntityRef newAgent)
        {
            AIBlackboardComponent bbComponent = new AIBlackboardComponent();
            AIBlackboardInitializer initializer = frame.FindAsset<AIBlackboardInitializer>(frame.RuntimeConfig.BlackboardInitializer.Id);
            AIBlackboardInitializer.InitializeBlackboard(frame, &bbComponent, initializer);
            frame.Set(newAgent, bbComponent);
        }
        
        private void InitializeNavmesh(Frame frame, AssetRef agentConfigAsset, AssetRef navMeshAsset, EntityRef newAgent)
        {
            NavMeshAgentConfig navMeshAgentConfig = frame.FindAsset<NavMeshAgentConfig>(agentConfigAsset);
            NavMesh navMesh = frame.FindAsset<NavMesh>(navMeshAsset);
            NavMeshPathfinder pathfinder = NavMeshPathfinder.Create(frame, newAgent, navMeshAgentConfig);
            NavMeshSteeringAgent steeringAgent = new NavMeshSteeringAgent();

            pathfinder.SetTarget(frame, FPVector3.Zero, navMesh);
            frame.Set(newAgent, pathfinder);
            frame.Set(newAgent, steeringAgent);
        }

        private static void SetRandomPosition(Frame frame, ZombieSpawner* spawner, EntityRef newAgent)
        {
            if (frame.Unsafe.TryGetPointer(newAgent, out Transform3D* agentTransform))
            {
                QList<FPVector3> spawnPoints = frame.ResolveList(spawner->spawnPoints);
                FPVector3 spawnPoint = spawnPoints.GetRandom(frame.RNG);

                Hit3D? hit = frame.Physics3D.Raycast(spawnPoint + FPVector3.Up, FPVector3.Down, 5);
                
                if (hit.HasValue)
                    spawnPoint.Y = hit.Value.Point.Y;
                
                agentTransform->Position = spawnPoint;
            }
        }
        
        public void OnDeath(Frame frame, EntityRef target)
        {
            instances.Remove(target);
        }

        public struct Filter
        {
            public EntityRef Entity;
            public ZombieSpawner* Spawner;
        }
    }
}