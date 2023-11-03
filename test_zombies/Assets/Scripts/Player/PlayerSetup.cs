using System;
using Quantum;
using UnityEngine;
using Zombie.Player.Animations;

namespace Zombie.Player
{
    public class PlayerSetup : MonoBehaviour
    {
        [Header("Systems")]
        [SerializeField] private PlayerAnimations animations;
        [SerializeField] private PlayerLookView lookView;
        
        [Header("Simulation")]
        [SerializeField] private EntityView entityPrototype;
        [SerializeField] private Transform lookDirection;

        public static event Action<PlayerSetup> OnPlayerInitialized = delegate {  };

        public PlayerRef? PlayerReference { get; private set; } = null;
        public Transform LookDirection => lookDirection;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            PlayerRef? localPlayerReference = GetLocalPlayerReference();

            if (localPlayerReference != null)
            {
                PlayerReference = localPlayerReference;
                animations.Initialize(QuantumRunner.Default.Game.Frames.Verified, entityPrototype.EntityRef);
                lookView.Initialize(entityPrototype.EntityRef);
                
                OnPlayerInitialized(this);
            }
        }

        private unsafe PlayerRef? GetLocalPlayerReference()
        {
            if (QuantumRunner.Default)
            {
                QuantumGame game = QuantumRunner.Default.Game;
                Frame frame = game.Frames.Verified;

                if (frame.Unsafe.TryGetPointer(entityPrototype.EntityRef, out PlayerLink* link))
                {
                    PlayerRef playerRef = link->Player;
                    
                    if(game.PlayerIsLocal(playerRef))
                        return playerRef;
                }
            }

            return null;
        }
    }
}