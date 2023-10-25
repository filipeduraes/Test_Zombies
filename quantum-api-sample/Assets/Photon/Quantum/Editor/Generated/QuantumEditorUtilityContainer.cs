// <auto-generated>
// This code was auto-generated by a tool, every time
// the tool executes this code will be reset.
//
// If you need to extend the classes generated to add
// fields or methods to them, please create partial  
// declarations in another file.
// </auto-generated>

#if UNITY_EDITOR
namespace Quantum.Editor {
  using Quantum.Prototypes;
  using UnityEditor;
  using UnityEngine;

  internal sealed class QuantumEditorUtilityContainer : ScriptableSingleton<QuantumEditorUtilityContainer> {

    public static new QuantumEditorUtilityContainer instance {
      get {
        var result = ScriptableSingleton<QuantumEditorUtilityContainer>.instance;
        result.hideFlags = HideFlags.None;
        return result;
      }
    }

    public KnownObjectsContainer ObjectsContainer = new KnownObjectsContainer();
    public FlatEntityPrototypeContainer PendingPrototype = new FlatEntityPrototypeContainer();

    [System.Serializable]
    public partial class KnownObjectsContainer : QuantumEditorUtility.SerializableObjectsContainerBase {
      public Quantum.BTSelector[] BTSelector = {};
      public Quantum.BTSequence[] BTSequence = {};
      public Quantum.BTRoot[] BTRoot = {};
      public Quantum.BTBlackboardCompare[] BTBlackboardCompare = {};
      public Quantum.BTCooldown[] BTCooldown = {};
      public Quantum.BTForceResult[] BTForceResult = {};
      public Quantum.BTLoop[] BTLoop = {};
      public Quantum.DebugLeaf[] DebugLeaf = {};
      public Quantum.WaitLeaf[] WaitLeaf = {};
      public Quantum.DebugService[] DebugService = {};
      public Quantum.DefaultAIFunctionAssetRef[] DefaultAIFunctionAssetRef = {};
      public Quantum.DefaultAIFunctionBool[] DefaultAIFunctionBool = {};
      public Quantum.DefaultAIFunctionByte[] DefaultAIFunctionByte = {};
      public Quantum.DefaultAIFunctionEntityRef[] DefaultAIFunctionEntityRef = {};
      public Quantum.DefaultAIFunctionFP[] DefaultAIFunctionFP = {};
      public Quantum.DefaultAIFunctionFPVector2[] DefaultAIFunctionFPVector2 = {};
      public Quantum.DefaultAIFunctionFPVector3[] DefaultAIFunctionFPVector3 = {};
      public Quantum.DefaultAIFunctionInt[] DefaultAIFunctionInt = {};
      public Quantum.DefaultAIFunctionString[] DefaultAIFunctionString = {};
      public Quantum.AIFunctionAND[] AIFunctionAND = {};
      public Quantum.AIFunctionNOT[] AIFunctionNOT = {};
      public Quantum.AIFunctionOR[] AIFunctionOR = {};
      public Quantum.AIConfig[] AIConfig = {};
      public Quantum.ResponseCurve[] ResponseCurve = {};
      public Quantum.GOAPDefaultAction[] GOAPDefaultAction = {};
      public Quantum.GOAPDefaultGoal[] GOAPDefaultGoal = {};
      public Quantum.GOAPRoot[] GOAPRoot = {};
      public Quantum.HFSMOrDecision[] HFSMOrDecision = {};
      public Quantum.HFSMAndDecision[] HFSMAndDecision = {};
      public Quantum.HFSMNotDecision[] HFSMNotDecision = {};
      public Quantum.HFSMRoot[] HFSMRoot = {};
      public Quantum.HFSMState[] HFSMState = {};
      public Quantum.HFSMTransitionSet[] HFSMTransitionSet = {};
      public Quantum.Consideration[] Consideration = {};
      public Quantum.UTRoot[] UTRoot = {};
      public Quantum.AIBlackboard[] AIBlackboard = {};
      public Quantum.AIBlackboardInitializer[] AIBlackboardInitializer = {};
      public Quantum.DebugAction[] DebugAction = {};
      public Quantum.IdleAction[] IdleAction = {};
      public Quantum.IncreaseBlackboardInt[] IncreaseBlackboardInt = {};
      public Quantum.SetBlackboardInt[] SetBlackboardInt = {};
      public Quantum.CheckBlackboardInt[] CheckBlackboardInt = {};
      public Quantum.TimerDecision[] TimerDecision = {};
      public Quantum.TrueDecision[] TrueDecision = {};
      public Quantum.AIFunction[] AIFunction = {};
      public Quantum.GOAPBackValidation[] GOAPBackValidation = {};
      public Quantum.GOAPHeuristic[] GOAPHeuristic = {};
      public Quantum.ClipData[] ClipData = {};
      public Quantum.GameInitData[] GameInitData = {};
      public Quantum.WeaponSpec[] WeaponSpec = {};
      public Quantum.SimulationConfig[] SimulationConfig = {};
      public Quantum.DecreaseTimer[] DecreaseTimer = {};
      public Quantum.ResetNavMeshTarget[] ResetNavMeshTarget = {};
      public Quantum.RotateTowardsWaypoint[] RotateTowardsWaypoint = {};
      public Quantum.SelectTargetFleeFromPredator[] SelectTargetFleeFromPredator = {};
      public Quantum.SelectTargetPosition[] SelectTargetPosition = {};
      public Quantum.SelectTargetPositionRandom[] SelectTargetPositionRandom = {};
      public Quantum.SenseEnvironment[] SenseEnvironment = {};
      public Quantum.SetNavMeshAgent[] SetNavMeshAgent = {};
      public Quantum.SetNavMeshTarget[] SetNavMeshTarget = {};
      public Quantum.SetTimerDazed[] SetTimerDazed = {};
      public Quantum.SetTimerRandom[] SetTimerRandom = {};
      public Quantum.SimpleDebugAction[] SimpleDebugAction = {};
      public Quantum.CoinFlip[] CoinFlip = {};
      public Quantum.CoinFlipFiftyFifty[] CoinFlipFiftyFifty = {};
      public Quantum.HasTimerEnded[] HasTimerEnded = {};
      public Quantum.IsWaypointInView[] IsWaypointInView = {};
      public Quantum.HasReachedPosition[] HasReachedPosition = {};
      public Quantum.ItemCoin[] ItemCoin = {};
      public Quantum.ItemPotionHealth[] ItemPotionHealth = {};
      public Quantum.ItemPotionMana[] ItemPotionMana = {};
      public Quantum.BinaryData[] BinaryData = {};
      public Quantum.CharacterController2DConfig[] CharacterController2DConfig = {};
      public Quantum.CharacterController3DConfig[] CharacterController3DConfig = {};
      public Quantum.EntityPrototype[] EntityPrototype = {};
      public Quantum.EntityView[] EntityView = {};
      public Quantum.Map[] Map = {};
      public Quantum.NavMesh[] NavMesh = {};
      public Quantum.NavMeshAgentConfig[] NavMeshAgentConfig = {};
      public Quantum.PhysicsMaterial[] PhysicsMaterial = {};
      public Quantum.PolygonCollider[] PolygonCollider = {};
      public Quantum.TerrainCollider[] TerrainCollider = {};

    }
  }
}
#endif
