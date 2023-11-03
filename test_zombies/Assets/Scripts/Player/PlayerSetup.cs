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
        [SerializeField] private PlayerDamageSender damageSender;
        
        [Header("Simulation")]
        [SerializeField] private EntityView entityPrototype;
        [SerializeField] private Transform lookDirection;

        public static event Action<PlayerSetup> OnLocalPlayerInitialized = delegate {  };

        public PlayerRef? PlayerReference { get; private set; } = null;
        public Transform LookDirection => lookDirection;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            PlayerRef? localPlayerReference = GetLocalPlayerReference();

            animations.Initialize(QuantumRunner.Default.Game.Frames.Verified);
            lookView.Initialize();
            
            if (localPlayerReference != null)
            {
                PlayerReference = localPlayerReference;
                damageSender.Initialize(entityPrototype.EntityRef);
                
                OnLocalPlayerInitialized(this);
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