using System;
using System.Globalization;
using Quantum;
using TMPro;
using UnityEngine;

namespace Zombie.UI.Timer
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;
        private DispatcherSubscription listener;

        private void OnEnable()
        {
            listener = QuantumEvent.Subscribe<EventOnTimerUpdate>(this, UpdateTimerUI);
        }

        private void OnDisable()
        {
            QuantumEvent.Unsubscribe(listener);
        }

        private void UpdateTimerUI(EventOnTimerUpdate callback)
        {
            float remainingTime = (callback.fullTime - callback.currentTime).AsFloat;
            timerText.SetText($"{remainingTime:0.0}");
        }
    }
}