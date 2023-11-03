using UnityEngine;
using UnityEngine.UI;
using Zombie.Player;

namespace Zombie.UI.Damage
{
    public class DamageUI : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private RectTransform gameOver;

        private void OnEnable()
        {
            PlayerDamageSender.OnDamage += UpdateView;
            PlayerDamageSender.OnDeath += ShowGameOverScreen;
        }

        private void OnDisable()
        {
            PlayerDamageSender.OnDamage -= UpdateView;
            PlayerDamageSender.OnDeath -= ShowGameOverScreen;
        }

        private void UpdateView(float t)
        {
            slider.value = t;
        }
        
        private void ShowGameOverScreen()
        {
            gameOver.gameObject.SetActive(true);
        }
    }
}
