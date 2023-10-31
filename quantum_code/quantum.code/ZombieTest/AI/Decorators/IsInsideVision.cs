using System;
using Photon.Deterministic;

namespace Quantum.ZombieTest.AI.Decorators
{
    [Serializable]
    public class IsInsideVision : BTDecorator
    {
        public FP minDistance;
        
        private ComponentFilter<CharacterController3D, Transform3D> players;
        private Transform3D ownerTransform;
        
        public override void OnEnter(BTParams btParams, ref AIContext aiContext)
        {
            base.OnEnter(btParams, ref aiContext);
            players = btParams.Frame.Filter<CharacterController3D, Transform3D>();
            ownerTransform = btParams.Frame.Get<Transform3D>(btParams.Entity);
        }

        public override bool DryRun(BTParams btParams, ref AIContext aiContext)
        {
            while (players.Next(out EntityRef _, out CharacterController3D _, out Transform3D playerTransform))
            {
                FP distance = (ownerTransform.Position - playerTransform.Position).SqrMagnitude;

                if (distance < minDistance)
                    return true;
            }

            return false;
        }
    }
}