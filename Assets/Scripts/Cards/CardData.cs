using UnityEngine;

namespace TheEternalTurn.Cards
{
    [CreateAssetMenu(fileName = "New Card", menuName = "The Eternal Turn/Card Data")]
    public class CardData : ScriptableObject
    {
        [Header("Card Information")]
        public string cardTitle;
        [TextArea(3, 6)]
        public string cardDescription;
        public Sprite cardImage;
        
        [Header("Card Metadata")]
        public int chapterNumber;
        public int cardNumber;
        public bool isChainCard; // Zincirli kart mı
        public CardData nextChainCard; // Zincirli kartsa bir sonraki kart
        
        [Header("Choice A")]
        public string choiceAText;
        public StatChange choiceAEffect;
        
        [Header("Choice B")]
        public string choiceBText;
        public StatChange choiceBEffect;
        
        [Header("Localization Keys")]
        public string titleKey;
        public string descriptionKey;
        public string choiceAKey;
        public string choiceBKey;
    }

    [System.Serializable]
    public class StatChange
    {
        [Header("Stat Changes (-100 to +100)")]
        [Range(-100, 100)]
        public int publicMoraleChange;
        
        [Range(-100, 100)]
        public int churchTrustChange;
        
        [Range(-100, 100)]
        public int plagueSpreadChange;
        
        [Range(-100, 100)]
        public int resourcesChange;
        
        [Header("Additional Effects")]
        public bool triggersChainCard;
        public string specialEffect; // Özel efektler için
        
        public bool HasAnyChange()
        {
            return publicMoraleChange != 0 || churchTrustChange != 0 || 
                   plagueSpreadChange != 0 || resourcesChange != 0;
        }
        
        public override string ToString()
        {
            var changes = new System.Collections.Generic.List<string>();
            
            if (publicMoraleChange != 0)
                changes.Add($"Morale: {(publicMoraleChange > 0 ? "+" : "")}{publicMoraleChange}");
            
            if (churchTrustChange != 0)
                changes.Add($"Church: {(churchTrustChange > 0 ? "+" : "")}{churchTrustChange}");
            
            if (plagueSpreadChange != 0)
                changes.Add($"Plague: {(plagueSpreadChange > 0 ? "+" : "")}{plagueSpreadChange}");
            
            if (resourcesChange != 0)
                changes.Add($"Resources: {(resourcesChange > 0 ? "+" : "")}{resourcesChange}");
            
            return string.Join(", ", changes);
        }
    }
} 