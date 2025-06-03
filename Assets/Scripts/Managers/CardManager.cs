using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TheEternalTurn.Cards;

namespace TheEternalTurn.Managers
{
    public class CardManager : MonoBehaviour
    {
        [Header("UI References")]
        public Image cardImage;
        public Text cardTitle;
        public Text cardDescription;
        public Button choiceAButton;
        public Button choiceBButton;
        public Text choiceAText;
        public Text choiceBText;
        
        [Header("Card Data")]
        public List<CardData> allCards = new List<CardData>();
        
        [Header("Chapter Management")]
        public Text chapterTitle;
        public int currentChapter = 1;
        private int currentCardIndex = 0;
        
        [Header("Animation")]
        public float cardTransitionDuration = 0.5f;
        public AnimationCurve cardTransitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        
        // Private variables
        private CardData currentCard;
        private List<CardData> currentChapterCards;
        private StatManager statManager;
        private CardData nextChainCard;
        
        // Events
        public System.Action<int> OnChapterChanged;
        public System.Action<CardData> OnCardChanged;
        
        private void Start()
        {
            statManager = FindObjectOfType<StatManager>();
            
            if (statManager == null)
            {
                Debug.LogError("StatManager not found! Please add StatManager to the scene.");
                return;
            }
            
            // Setup button listeners
            choiceAButton.onClick.AddListener(() => OnChoiceSelected(true));
            choiceBButton.onClick.AddListener(() => OnChoiceSelected(false));
            
            // Load first chapter
            LoadChapter(currentChapter);
        }
        
        private void LoadChapter(int chapterNumber)
        {
            currentChapter = chapterNumber;
            currentCardIndex = 0;
            
            // Get cards for current chapter
            currentChapterCards = allCards.Where(card => card.chapterNumber == chapterNumber).ToList();
            
            if (currentChapterCards.Count == 0)
            {
                Debug.LogWarning($"No cards found for chapter {chapterNumber}");
                return;
            }
            
            // Update chapter title
            if (chapterTitle != null)
            {
                string chapterName = GetChapterName(chapterNumber);
                chapterTitle.text = $"Bölüm {chapterNumber}: {chapterName}";
            }
            
            // Load first card
            LoadCard(currentChapterCards[0]);
            
            OnChapterChanged?.Invoke(chapterNumber);
        }
        
        private string GetChapterName(int chapterNumber)
        {
            return chapterNumber switch
            {
                1 => "Sakin Fırtına Öncesi",
                2 => "Gizli Yayılım",
                3 => "Açık Salgın",
                4 => "Kargaşa Dönemi",
                5 => "Son Umut",
                _ => "Bilinmeyen Bölüm"
            };
        }
        
        private void LoadCard(CardData card)
        {
            if (card == null)
            {
                Debug.LogError("Trying to load null card!");
                return;
            }
            
            currentCard = card;
            
            // Update UI
            if (cardImage != null && card.cardImage != null)
                cardImage.sprite = card.cardImage;
            
            if (cardTitle != null)
                cardTitle.text = card.cardTitle;
            
            if (cardDescription != null)
                cardDescription.text = card.cardDescription;
            
            if (choiceAText != null)
                choiceAText.text = card.choiceAText;
            
            if (choiceBText != null)
                choiceBText.text = card.choiceBText;
            
            // Enable buttons
            choiceAButton.interactable = true;
            choiceBButton.interactable = true;
            
            OnCardChanged?.Invoke(card);
            
            Debug.Log($"Loaded card: {card.cardTitle} (Chapter {card.chapterNumber}, Card {card.cardNumber})");
        }
        
        private void OnChoiceSelected(bool isChoiceA)
        {
            if (currentCard == null) return;
            
            // Disable buttons to prevent multiple clicks
            choiceAButton.interactable = false;
            choiceBButton.interactable = false;
            
            // Get the selected choice effect
            StatChange selectedEffect = isChoiceA ? currentCard.choiceAEffect : currentCard.choiceBEffect;
            
            // Apply stat changes
            if (selectedEffect != null && selectedEffect.HasAnyChange())
            {
                statManager.UpdateStats(
                    selectedEffect.publicMoraleChange,
                    selectedEffect.churchTrustChange,
                    selectedEffect.plagueSpreadChange,
                    selectedEffect.resourcesChange
                );
                
                Debug.Log($"Applied stat changes: {selectedEffect.ToString()}");
            }
            
            // Handle chain cards
            if (selectedEffect != null && selectedEffect.triggersChainCard && currentCard.nextChainCard != null)
            {
                nextChainCard = currentCard.nextChainCard;
                Invoke(nameof(LoadNextCard), cardTransitionDuration);
            }
            else
            {
                // Move to next card in chapter
                Invoke(nameof(LoadNextCard), cardTransitionDuration);
            }
        }
        
        private void LoadNextCard()
        {
            // Load chain card if available
            if (nextChainCard != null)
            {
                LoadCard(nextChainCard);
                nextChainCard = null;
                return;
            }
            
            // Move to next card in chapter
            currentCardIndex++;
            
            if (currentCardIndex < currentChapterCards.Count)
            {
                LoadCard(currentChapterCards[currentCardIndex]);
            }
            else
            {
                // Chapter completed, move to next chapter
                LoadChapter(currentChapter + 1);
            }
        }
        
        // Public methods for external control
        public void RestartGame()
        {
            currentChapter = 1;
            currentCardIndex = 0;
            nextChainCard = null;
            LoadChapter(1);
        }
        
        public void JumpToChapter(int chapterNumber)
        {
            if (allCards.Any(card => card.chapterNumber == chapterNumber))
            {
                LoadChapter(chapterNumber);
            }
            else
            {
                Debug.LogWarning($"Chapter {chapterNumber} not found!");
            }
        }
        
        // Debug methods
        [ContextMenu("Load Next Card")]
        private void DebugLoadNextCard()
        {
            LoadNextCard();
        }
        
        [ContextMenu("Restart Game")]
        private void DebugRestartGame()
        {
            RestartGame();
        }
    }
} 