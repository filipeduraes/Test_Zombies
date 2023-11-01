using Photon.Deterministic;
using Quantum.Physics3D;

namespace Quantum.ZombieTest.Systems
{
    public unsafe class ShooterSystem : SystemMainThreadFilter<ShooterSystem.Filter>, ISignalChangeLookDirection
    {
        private EntityRef shooterEntity;
        private FPVector3 lookDirection;
        private bool shootInputWasPressed = false;
        
        public override void Update(Frame frame, ref Filter filter)
        {
            shooterEntity = filter.Entity;
            Input* playerInput = frame.GetPlayerInput(filter.PlayerLink->Player);

            if (playerInput->Aim && !shootInputWasPressed && playerInput->Shoot)
            {
                FPVector3 muzzlePosition = playerInput->MuzzlePosition + lookDirection * FP.FromFloat_UNSAFE(0.5f);
                Hit3D? hit = frame.Physics3D.Raycast(muzzlePosition, lookDirection, 10, filter.Shooter->DamageLayer);
                
                if (hit != null && hit.Value.Entity != filter.Entity)
                    frame.Signals.OnDamage(hit.Value.Entity, filter.Entity, filter.Shooter->Damage);
            }

            shootInputWasPressed = playerInput->Shoot;
        }

        public void ChangeLookDirection(Frame frame, EntityRef sender, FPVector3 direction)
        {
            if(sender != shooterEntity)
                return;
            
            lookDirection = direction;
        }
        
        public struct Filter
        {
            public EntityRef Entity;
            public Shooter* Shooter;
            public PlayerLink* PlayerLink;
            public Transform3D* Transform;
        }
    }
}