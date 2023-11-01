using Quantum;
using UnityEngine;

namespace Photon.QuantumDemo.Menu.Scripts
{
    public class CustomCallbacks : QuantumCallbacks
    {
        [SerializeField] private RuntimePlayer playerData;
    
        public override void OnGameStart(QuantumGame game) 
        {
            if (game.Session.IsPaused) 
                return;

            foreach (var lp in game.GetLocalPlayers()) 
            {
                Debug.Log($"CustomCallbacks - sending player: {lp}"); 
                game.SendPlayerData(lp, playerData);
            }
        }

        public override void OnGameResync(QuantumGame game)
        {
            Debug.Log($"Detected Resync. Verified tick: {game.Frames.Verified.Number}");
        }
    }
}

