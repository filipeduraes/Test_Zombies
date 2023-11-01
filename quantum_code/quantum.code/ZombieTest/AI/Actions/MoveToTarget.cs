using System;
using Photon.Deterministic;
using Quantum.Core;

namespace Quantum
{
    [Serializable]
    public unsafe class MoveToTarget : BTLeaf
    {
        public FP minDistance;
        public AIBlackboardValueKey navMeshAssetKey;
        public AIBlackboardValueKey targetKey;
        
        private NavMesh navMesh;
        private Transform3D* targetTransform;
        private Transform3D* ownerTransform;
        private NavMeshPathfinder* pathFinder;
        
        public override void OnEnter(BTParams btParams, ref AIContext aiContext)
        {
            EntityRef target = btParams.Blackboard->GetEntityRef(btParams.Frame, targetKey.Key);
            AssetRef navMeshAsset = btParams.Blackboard->GetAssetRef(btParams.Frame, navMeshAssetKey.Key);

            if (TryGetAllPointers(btParams.Frame, target, btParams.Entity))
            {
                navMesh = btParams.Frame.FindAsset<NavMesh>(navMeshAsset);
                return;
            }
            
            SetStatus(btParams.Frame, BTStatus.Failure, btParams.Agent);
        }

        protected override BTStatus OnUpdate(BTParams btParams, ref AIContext aiContext)
        {
            if (targetTransform is null || navMesh is null || pathFinder is null || ownerTransform is null)
                return BTStatus.Failure;
            
            pathFinder->SetTarget(btParams.Frame, targetTransform->Position, navMesh);
            
            FP distance = (targetTransform->Position - ownerTransform->Position).SqrMagnitude;
            bool reachedTarget = distance < minDistance * minDistance;
            
            return reachedTarget ? BTStatus.Success : BTStatus.Running;
        }
        
        private bool TryGetAllPointers(Frame frame, EntityRef target, EntityRef entity)
        {
            FrameBase.FrameBaseUnsafe frameUnsafe = frame.Unsafe;
            
            return frameUnsafe.TryGetPointer(target, out targetTransform) 
                   && frameUnsafe.TryGetPointer(entity, out ownerTransform) 
                   && frameUnsafe.TryGetPointer(entity, out pathFinder);
        }
    }
}