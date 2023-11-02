using System;
using UnityEngine;

namespace Zombie.Player.Animations
{
    [Serializable]
    public struct AnimatorParameter
    {
        [SerializeField] private string key;
        private int hash;
            
        public void CacheHash()
        {
            hash = Animator.StringToHash(key);
        }

        public static implicit operator int(AnimatorParameter parameter)
        {
            return parameter.hash;
        }
        
        public static implicit operator AnimatorParameter(string parameterName)
        {
            AnimatorParameter parameter = new()
            {
                key = parameterName
            };
            
            return parameter;
        }
    }
}