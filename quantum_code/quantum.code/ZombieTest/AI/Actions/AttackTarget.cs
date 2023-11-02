using System;
using Photon.Deterministic;

namespace Quantum
{
    [Serializable]
    public unsafe class AttackTarget : BTLeaf
    {
        public AIBlackboardValueKey target;
        public FP minimumAttackDistance;
        public int damage;
        
        protected override BTStatus OnUpdate(BTParams btParams, ref AIContext aiContext)
        {
            Frame frame = btParams.Frame;
            
            if (frame.Unsafe.TryGetPointer(btParams.Entity, out Transform3D* zombieTransform))
            {
                EntityRef targetEntity = btParams.Blackboard->GetEntityRef(frame, target.Key);

                if (frame.Unsafe.TryGetPointer(targetEntity, out Transform3D* targetTransform))
                {
                    if (CanTakeDamage(targetTransform, zombieTransform))
                    {
                        frame.Events.OnAIAttack(btParams.Entity);
                        frame.Signals.OnDamage(targetEntity, btParams.Entity, damage);
                    }
                }
            }
            
            return BTStatus.Success;
        }

        private bool CanTakeDamage(Transform3D* targetTransform, Transform3D* zombieTransform)
        {
            FPVector3 zombieToTarget = targetTransform->Position - zombieTransform->Position;
            FP distanceToTarget = zombieToTarget.Magnitude;

            if (distanceToTarget == 0)
                return false;
            
            FPVector3 directionToTarget = zombieToTarget / distanceToTarget;
            bool isInsideMinimumDistance = distanceToTarget <= minimumAttackDistance;
            
            return IsInsideVisionRange(zombieTransform, directionToTarget) && isInsideMinimumDistance;
        }

        private static bool IsInsideVisionRange(Transform3D* zombieTransform, FPVector3 directionToTarget)
        {
            FP visionRange = FP.FromFloat_UNSAFE(0.2f);
            return FPVector3.Dot(zombieTransform->Forward, directionToTarget) > visionRange;
        }
    }
}