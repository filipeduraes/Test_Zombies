namespace Quantum.ZombieTest.Systems
{
    public unsafe class DamageSystem : SystemMainThreadFilter<DamageSystem.Filter>, ISignalOnDamage
    {
        private Damageable* damageable;
        
        public void OnDamage(Frame frame, EntityRef targetCharacter, EntityRef sourceCharacter, int damage)
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
        
        public override void Update(Frame frame, ref Filter filter)
        {
            if (damageable is null)
            {
                damageable = filter.Damageable;
                damageable->CurrentHealth = damageable->MaxHealth;
            }
        }
        
        public struct Filter
        {
            public EntityRef Entity;
            public Damageable* Damageable;
        }
    }
}