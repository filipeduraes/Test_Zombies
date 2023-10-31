using System;
using Photon.Deterministic;

namespace Quantum.ZombieTest.AI.Actions
{
    [Serializable]
    public class FindClosestCharacter : BTLeaf
    {
        public string resultKey;
        
        protected override unsafe BTStatus OnUpdate(BTParams btParams, ref AIContext aiContext)
        {
            ComponentFilter<CharacterController3D,Transform3D> filter = btParams.Frame.Filter<CharacterController3D, Transform3D>();
            Transform3D ownerTransform = btParams.Frame.Get<Transform3D>(btParams.Entity);

            FP closestDistance = FP.MaxValue;
            EntityRef? closestEntity = null;
            
            while (filter.Next(out EntityRef characterEntity, out CharacterController3D _, out Transform3D characterTransform))
            {
                FP distance = (characterTransform.Position - ownerTransform.Position).SqrMagnitude;

                if (distance < closestDistance)
                {
                    closestEntity = characterEntity;
                    closestDistance = distance;
                }
            }

            if (closestEntity.HasValue)
            {
                btParams.Blackboard->Set(btParams.Frame, resultKey, closestEntity.Value);
                return BTStatus.Success;
            }

            return BTStatus.Failure;
        }
    }
}