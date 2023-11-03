namespace Quantum.ZombieTest.Systems
{
    public unsafe class ShutdownSystem : SystemMainThreadFilter<ShutdownSystem.Filter>
    {
        public override void Update(Frame frame, ref Filter filter)
        {
            filter.Timer->timer += frame.DeltaTime;
            frame.Events.OnTimerUpdate(filter.Timer->timer, filter.Timer->time);
            
            if (filter.Timer->timer >= filter.Timer->time)
            {
                frame.Events.OnTimerFinished();
            }
        }
        
        public struct Filter
        {
            public EntityRef Entity;
            public ShutdownTimer* Timer;
        }
    }
}