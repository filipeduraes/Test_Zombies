namespace Quantum.ZombieTest.Systems
{
    public unsafe class DamageSystem : SystemMainThreadFilter<DamageSystem.Filter>, ISignalOnDamage
    {
        public void OnDamage(Frame frame, EntityRef targetCharacter, EntityRef sourceCharacter, int damage)
        {
            if(frame.Unsafe.TryGetPointer(targetCharacter, out Damageable* damageable))
            {
                damageable->CurrentHealth -= damage;
                damageable->IsDead = damageable->CurrentHealth <= 0;
                frame.Events.OnDamage(targetCharacter, damageable->CurrentHealth);

                if (damageable->IsDead)
                {
                    frame.Destroy(targetCharacter);
                    frame.Signals.OnDeath(targetCharacter);
                }
            }
        }
        
        public override void Update(Frame frame, ref Filter filter)
        {
            if (!filter.Damageable->HasInitialized)
            {
                filter.Damageable->CurrentHealth = filter.Damageable->MaxHealth;
                filter.Damageable->HasInitialized = true;
            }
        }
        
        public struct Filter
        {
            public EntityRef Entity;
            public Damageable* Damageable;
        }
    }
}