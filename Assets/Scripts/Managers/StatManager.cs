using UnityEngine;
using UnityEngine.UI;
using System;

namespace TheEternalTurn.Managers
{
    public class StatManager : MonoBehaviour
    {
        [Header("UI References")]
        public Text publicMoraleText;
        public Text churchTrustText;
        public Text plagueSpreadText;
        public Text resourcesText;
        
        [Header("Stat Icons")]
        public Image publicMoraleIcon;
        public Image churchTrustIcon;
        public Image plagueSpreadIcon;
        public Image resourcesIcon;

        [Header("Starting Values")]
        [SerializeField] private int startingPublicMorale = 50;
        [SerializeField] private int startingChurchTrust = 50;
        [SerializeField] private int startingPlagueSpread = 10;
        [SerializeField] private int startingResources = 50;

        // Current stat values
        private int publicMorale;
        private int churchTrust;
        private int plagueSpread;
        private int resources;

        // Events for game state changes
        public static event Action<GameEndType> OnGameEnd;
        public static event Action<int, int, int, int> OnStatsUpdated;

        // Properties for external access
        public int PublicMorale => publicMorale;
        public int ChurchTrust => churchTrust;
        public int PlagueSpread => plagueSpread;
        public int Resources => resources;

        private void Start()
        {
            InitializeStats();
            UpdateUI();
        }

        private void InitializeStats()
        {
            publicMorale = startingPublicMorale;
            churchTrust = startingChurchTrust;
            plagueSpread = startingPlagueSpread;
            resources = startingResources;
        }

        public void UpdateStats(int moraleDelta, int churchDelta, int plagueDelta, int resourceDelta)
        {
            // Update stats with bounds checking
            publicMorale = Mathf.Clamp(publicMorale + moraleDelta, 0, 100);
            churchTrust = Mathf.Clamp(churchTrust + churchDelta, 0, 100);
            plagueSpread = Mathf.Clamp(plagueSpread + plagueDelta, 0, 100);
            resources = Mathf.Clamp(resources + resourceDelta, 0, 100);

            UpdateUI();
            CheckGameEndConditions();
            
            // Notify other systems about stat changes
            OnStatsUpdated?.Invoke(publicMorale, churchTrust, plagueSpread, resources);
        }

        private void UpdateUI()
        {
            if (publicMoraleText != null)
                publicMoraleText.text = $"{publicMorale}%";
            
            if (churchTrustText != null)
                churchTrustText.text = $"{churchTrust}%";
            
            if (plagueSpreadText != null)
                plagueSpreadText.text = $"{plagueSpread}%";
            
            if (resourcesText != null)
                resourcesText.text = $"{resources}%";
        }

        private void CheckGameEndConditions()
        {
            // Victory condition: Plague eliminated
            if (plagueSpread <= 0)
            {
                OnGameEnd?.Invoke(GameEndType.Victory);
                return;
            }

            // Defeat conditions
            if (publicMorale <= 0)
            {
                OnGameEnd?.Invoke(GameEndType.Defeat_PublicRebellion);
            }
            else if (churchTrust <= 0)
            {
                OnGameEnd?.Invoke(GameEndType.Defeat_ChurchExcommunication);
            }
            else if (plagueSpread >= 100)
            {
                OnGameEnd?.Invoke(GameEndType.Defeat_TotalPlagueSpread);
            }
            else if (resources <= 0)
            {
                OnGameEnd?.Invoke(GameEndType.Defeat_ResourceDepletion);
            }
        }

        // Method to reset stats for new game
        public void ResetStats()
        {
            InitializeStats();
            UpdateUI();
        }

        // Debug method to set specific stat values
        [ContextMenu("Debug: Set Random Stats")]
        private void SetRandomStats()
        {
            UpdateStats(
                UnityEngine.Random.Range(-20, 20),
                UnityEngine.Random.Range(-20, 20),
                UnityEngine.Random.Range(-20, 20),
                UnityEngine.Random.Range(-20, 20)
            );
        }
    }

    public enum GameEndType
    {
        Victory,
        Defeat_PublicRebellion,
        Defeat_ChurchExcommunication,
        Defeat_TotalPlagueSpread,
        Defeat_ResourceDepletion
    }
} 