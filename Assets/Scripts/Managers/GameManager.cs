using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TheEternalTurn.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game State")]
        public bool isGameActive = true;
        public GameState currentGameState = GameState.Playing;
        
        [Header("UI References")]
        public GameObject gameOverPanel;
        public Text gameOverTitle;
        public Text gameOverDescription;
        public Button restartButton;
        public Button mainMenuButton;
        public Button continueButton;
        
        [Header("Victory/Defeat Panels")]
        public GameObject victoryPanel;
        public GameObject defeatPanel;
        
        [Header("Audio")]
        public AudioSource victoryAudioSource;
        public AudioSource defeatAudioSource;
        public AudioClip victoryClip;
        public AudioClip defeatClip;
        
        // Managers
        private StatManager statManager;
        private CardManager cardManager;
        
        // Game statistics
        private float gameStartTime;
        private int cardsPlayed = 0;
        private int chaptersCompleted = 0;
        
        private void Start()
        {
            // Initialize game
            gameStartTime = Time.time;
            currentGameState = GameState.Playing;
            isGameActive = true;
            
            // Find managers
            statManager = FindObjectOfType<StatManager>();
            cardManager = FindObjectOfType<CardManager>();
            
            // Subscribe to events
            StatManager.OnGameEnd += HandleGameEnd;
            
            if (cardManager != null)
            {
                cardManager.OnChapterChanged += HandleChapterChanged;
                cardManager.OnCardChanged += HandleCardChanged;
            }
            
            // Setup UI
            SetupUI();
            
            Debug.Log("The Eternal Turn - Game Started");
        }
        
        private void OnDestroy()
        {
            // Unsubscribe from events
            StatManager.OnGameEnd -= HandleGameEnd;
            
            if (cardManager != null)
            {
                cardManager.OnChapterChanged -= HandleChapterChanged;
                cardManager.OnCardChanged -= HandleCardChanged;
            }
        }
        
        private void SetupUI()
        {
            // Hide game over panels initially
            if (gameOverPanel != null)
                gameOverPanel.SetActive(false);
            
            if (victoryPanel != null)
                victoryPanel.SetActive(false);
            
            if (defeatPanel != null)
                defeatPanel.SetActive(false);
            
            // Setup button listeners
            if (restartButton != null)
                restartButton.onClick.AddListener(RestartGame);
            
            if (mainMenuButton != null)
                mainMenuButton.onClick.AddListener(GoToMainMenu);
            
            if (continueButton != null)
                continueButton.onClick.AddListener(ContinueGame);
        }
        
        private void HandleGameEnd(GameEndType endType)
        {
            isGameActive = false;
            
            float gameTime = Time.time - gameStartTime;
            
            switch (endType)
            {
                case GameEndType.Victory:
                    HandleVictory(gameTime);
                    break;
                
                case GameEndType.Defeat_PublicRebellion:
                    HandleDefeat("Halk İsyanı", "Halkın morali tamamen düştü ve isyan ettiler. Liderliğiniz sona erdi.", gameTime);
                    break;
                
                case GameEndType.Defeat_ChurchExcommunication:
                    HandleDefeat("Kilise Aforozu", "Kilise güveni tamamen kayboldu ve aforoz edildiniz. Köyde hiçbir otoriteniz kalmadı.", gameTime);
                    break;
                
                case GameEndType.Defeat_TotalPlagueSpread:
                    HandleDefeat("Veba Salgını", "Veba tüm köyü kapladı. Herkes öldü ve siz de hayatta kalamadınız.", gameTime);
                    break;
                
                case GameEndType.Defeat_ResourceDepletion:
                    HandleDefeat("Kaynak Tükenmesi", "Tüm kaynaklar tükendi. Köy açlık ve sefalet içinde çöktü.", gameTime);
                    break;
            }
        }
        
        private void HandleVictory(float gameTime)
        {
            currentGameState = GameState.Victory;
            
            // Show victory UI
            if (victoryPanel != null)
                victoryPanel.SetActive(true);
            
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);
            
            if (gameOverTitle != null)
                gameOverTitle.text = "ZAFERİ KAZANDINIZ!";
            
            if (gameOverDescription != null)
            {
                string timeText = FormatTime(gameTime);
                gameOverDescription.text = $"Veba salgınını başarıyla yendirişiniz!\n\n" +
                                         $"Oyun Süresi: {timeText}\n" +
                                         $"Oynanan Kart: {cardsPlayed}\n" +
                                         $"Tamamlanan Bölüm: {chaptersCompleted}";
            }
            
            // Play victory audio
            if (victoryAudioSource != null && victoryClip != null)
                victoryAudioSource.PlayOneShot(victoryClip);
            
            Debug.Log($"VICTORY! Game completed in {gameTime:F2} seconds with {cardsPlayed} cards played.");
        }
        
        private void HandleDefeat(string defeatType, string description, float gameTime)
        {
            currentGameState = GameState.Defeat;
            
            // Show defeat UI
            if (defeatPanel != null)
                defeatPanel.SetActive(true);
            
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);
            
            if (gameOverTitle != null)
                gameOverTitle.text = $"YENİLGİ - {defeatType}";
            
            if (gameOverDescription != null)
            {
                string timeText = FormatTime(gameTime);
                gameOverDescription.text = $"{description}\n\n" +
                                         $"Oyun Süresi: {timeText}\n" +
                                         $"Oynanan Kart: {cardsPlayed}\n" +
                                         $"Tamamlanan Bölüm: {chaptersCompleted}";
            }
            
            // Play defeat audio
            if (defeatAudioSource != null && defeatClip != null)
                defeatAudioSource.PlayOneShot(defeatClip);
            
            Debug.Log($"DEFEAT ({defeatType})! Game ended in {gameTime:F2} seconds with {cardsPlayed} cards played.");
        }
        
        private void HandleChapterChanged(int chapterNumber)
        {
            chaptersCompleted = chapterNumber - 1;
            Debug.Log($"Chapter changed to: {chapterNumber}");
        }
        
        private void HandleCardChanged(TheEternalTurn.Cards.CardData card)
        {
            if (currentGameState == GameState.Playing)
            {
                cardsPlayed++;
            }
        }
        
        // UI Button Methods
        public void RestartGame()
        {
            // Reset game state
            isGameActive = true;
            currentGameState = GameState.Playing;
            gameStartTime = Time.time;
            cardsPlayed = 0;
            chaptersCompleted = 0;
            
            // Hide game over panels
            if (gameOverPanel != null)
                gameOverPanel.SetActive(false);
            
            if (victoryPanel != null)
                victoryPanel.SetActive(false);
            
            if (defeatPanel != null)
                defeatPanel.SetActive(false);
            
            // Restart managers
            if (statManager != null)
                statManager.ResetStats();
            
            if (cardManager != null)
                cardManager.RestartGame();
            
            Debug.Log("Game Restarted");
        }
        
        public void GoToMainMenu()
        {
            // Load main menu scene (you'll need to create this)
            SceneManager.LoadScene("MainMenu");
        }
        
        public void ContinueGame()
        {
            if (currentGameState == GameState.Victory)
            {
                // For victory, you might want to show credits or stats
                Debug.Log("Continue from victory - showing credits or stats");
            }
            else if (currentGameState == GameState.Defeat)
            {
                // For defeat, restart the game
                RestartGame();
            }
        }
        
        // Utility methods
        private string FormatTime(float timeInSeconds)
        {
            int minutes = Mathf.FloorToInt(timeInSeconds / 60);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60);
            return $"{minutes:00}:{seconds:00}";
        }
        
        // Public getters for external systems
        public bool IsGameActive => isGameActive;
        public GameState CurrentGameState => currentGameState;
        public float GameTime => Time.time - gameStartTime;
        public int CardsPlayed => cardsPlayed;
        public int ChaptersCompleted => chaptersCompleted;
        
        // Debug methods
        [ContextMenu("Force Victory")]
        private void DebugForceVictory()
        {
            HandleGameEnd(GameEndType.Victory);
        }
        
        [ContextMenu("Force Defeat")]
        private void DebugForceDefeat()
        {
            HandleGameEnd(GameEndType.Defeat_PublicRebellion);
        }
    }
    
    public enum GameState
    {
        Playing,
        Victory,
        Defeat,
        Paused
    }
} 