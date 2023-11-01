using Quantum.ZombieTest.Systems;

namespace Quantum 
{
    public static class SystemSetup 
    {
        public static SystemBase[] CreateSystems(RuntimeConfig gameConfig, SimulationConfig simulationConfig) 
        {
            return new SystemBase[] 
            {
                // pre-defined core systems
                //new Core.CullingSystem2D(), 
                new Core.CullingSystem3D(),

                // Uncomment injection and retrieval system activate projectiles
                // new ProjectileHitQueryInjectionSystem(), 

                //new Core.PhysicsSystem2D(),
                new Core.PhysicsSystem3D(),

                new Core.NavigationSystem(),
                new Core.EntityPrototypeSystem(),

                // needed for the BotSDK Debugger
                new BotSDKDebuggerSystem(),

                // user systems go here
                new CharacterSystem(),
                new ZombieSpawnerSystem(), 

                // new ProjectileHitRetrievalSystem(), 
                new PlayerInitSystem(), 
                new ComponentsAddedRemovedSystem(), 
                new TimeLapsedSystem(), 
                new MovementSystem(),
                new HazardSystem(), 
                new AttackSystem(),
                new DefendSystem(), 
                new HealthSystem(), 
                new PickUpSystem(), 
                new EntitySpawnSystem(), 
                new AISystem(),

                // user command related systems
                new PlayerCommandsSystem()
            };
        }
    }
}