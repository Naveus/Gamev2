using UnityEngine;
using UnityEngine.UI;

namespace TheEternalTurn.UI
{
    public class UICanvasSetup : MonoBehaviour
    {
        [Header("Canvas Settings")]
        public Canvas mainCanvas;
        public CanvasScaler canvasScaler;
        public GraphicRaycaster graphicRaycaster;
        
        [Header("Background")]
        public Image backgroundImage;
        public Sprite backgroundSprite;
        
        [Header("Main Panel")]
        public RectTransform mainPanel;
        
        [Header("Stat Panel")]
        public HorizontalLayoutGroup statPanel;
        public GameObject statPrefab;
        
        [Header("Card Panel")]
        public RectTransform cardPanel;
        public Image cardImage;
        public Text cardTitle;
        public Text cardDescription;
        
        [Header("Choice Buttons")]
        public Button choiceAButton;
        public Button choiceBButton;
        public Text choiceAText;
        public Text choiceBText;
        
        [Header("Chapter Info")]
        public Text chapterTitle;
        
        [Header("Game Over Panel")]
        public GameObject gameOverPanel;
        public Text gameOverTitle;
        public Text gameOverDescription;
        public Button restartButton;
        public Button mainMenuButton;
        
        private void Start()
        {
            SetupCanvas();
            SetupMainPanel();
            SetupStatPanel();
            SetupCardPanel();
            SetupGameOverPanel();
        }
        
        private void SetupCanvas()
        {
            if (mainCanvas == null)
                mainCanvas = GetComponent<Canvas>();
            
            // Canvas settings for PC
            mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            mainCanvas.sortingOrder = 0;
            
            // Canvas Scaler for 1920x1080
            if (canvasScaler == null)
                canvasScaler = GetComponent<CanvasScaler>();
            
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;
            
            // Graphic Raycaster
            if (graphicRaycaster == null)
                graphicRaycaster = GetComponent<GraphicRaycaster>();
        }
        
        private void SetupMainPanel()
        {
            if (mainPanel == null)
            {
                GameObject panelGO = new GameObject("MainPanel");
                panelGO.transform.SetParent(transform);
                mainPanel = panelGO.AddComponent<RectTransform>();
                panelGO.AddComponent<Image>().color = new Color(0, 0, 0, 0.1f);
            }
            
            // Center the panel like a phone screen (1080x1920)
            mainPanel.anchorMin = new Vector2(0.5f, 0.5f);
            mainPanel.anchorMax = new Vector2(0.5f, 0.5f);
            mainPanel.pivot = new Vector2(0.5f, 0.5f);
            mainPanel.anchoredPosition = Vector2.zero;
            mainPanel.sizeDelta = new Vector2(1080, 1920);
            
            // Add background image
            if (backgroundImage == null)
            {
                GameObject bgGO = new GameObject("BackgroundImage");
                bgGO.transform.SetParent(transform, false);
                backgroundImage = bgGO.AddComponent<Image>();
                
                // Set background to full screen
                RectTransform bgRect = backgroundImage.GetComponent<RectTransform>();
                bgRect.anchorMin = Vector2.zero;
                bgRect.anchorMax = Vector2.one;
                bgRect.offsetMin = Vector2.zero;
                bgRect.offsetMax = Vector2.zero;
                
                if (backgroundSprite != null)
                    backgroundImage.sprite = backgroundSprite;
                else
                    backgroundImage.color = new Color(0.2f, 0.15f, 0.1f, 1f); // Dark medieval color
            }
        }
        
        private void SetupStatPanel()
        {
            if (statPanel == null)
            {
                GameObject statGO = new GameObject("StatPanel");
                statGO.transform.SetParent(mainPanel, false);
                statPanel = statGO.AddComponent<HorizontalLayoutGroup>();
                statGO.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
            
            // Position at top of main panel
            RectTransform statRect = statPanel.GetComponent<RectTransform>();
            statRect.anchorMin = new Vector2(0, 1);
            statRect.anchorMax = new Vector2(1, 1);
            statRect.pivot = new Vector2(0.5f, 1);
            statRect.anchoredPosition = new Vector2(0, -50);
            statRect.sizeDelta = new Vector2(-100, 100);
            
            // Layout settings
            statPanel.spacing = 20;
            statPanel.padding = new RectOffset(20, 20, 20, 20);
            statPanel.childAlignment = TextAnchor.MiddleCenter;
            statPanel.childControlWidth = true;
            statPanel.childControlHeight = false;
        }
        
        private void SetupCardPanel()
        {
            if (cardPanel == null)
            {
                GameObject cardGO = new GameObject("CardPanel");
                cardGO.transform.SetParent(mainPanel, false);
                cardPanel = cardGO.AddComponent<RectTransform>();
            }
            
            // Position in center of main panel
            cardPanel.anchorMin = new Vector2(0.1f, 0.2f);
            cardPanel.anchorMax = new Vector2(0.9f, 0.8f);
            cardPanel.offsetMin = Vector2.zero;
            cardPanel.offsetMax = Vector2.zero;
            
            // Setup card image
            if (cardImage == null)
            {
                GameObject imgGO = new GameObject("CardImage");
                imgGO.transform.SetParent(cardPanel, false);
                cardImage = imgGO.AddComponent<Image>();
                
                RectTransform imgRect = cardImage.GetComponent<RectTransform>();
                imgRect.anchorMin = new Vector2(0, 0.6f);
                imgRect.anchorMax = new Vector2(1, 1);
                imgRect.offsetMin = Vector2.zero;
                imgRect.offsetMax = Vector2.zero;
                
                cardImage.color = new Color(0.8f, 0.8f, 0.8f, 1f);
            }
            
            // Setup card title
            if (cardTitle == null)
            {
                GameObject titleGO = new GameObject("CardTitle");
                titleGO.transform.SetParent(cardPanel, false);
                cardTitle = titleGO.AddComponent<Text>();
                
                RectTransform titleRect = cardTitle.GetComponent<RectTransform>();
                titleRect.anchorMin = new Vector2(0, 0.45f);
                titleRect.anchorMax = new Vector2(1, 0.6f);
                titleRect.offsetMin = Vector2.zero;
                titleRect.offsetMax = Vector2.zero;
                
                cardTitle.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                cardTitle.fontSize = 32;
                cardTitle.fontStyle = FontStyle.Bold;
                cardTitle.alignment = TextAnchor.MiddleCenter;
                cardTitle.color = Color.white;
            }
            
            // Setup card description
            if (cardDescription == null)
            {
                GameObject descGO = new GameObject("CardDescription");
                descGO.transform.SetParent(cardPanel, false);
                cardDescription = descGO.AddComponent<Text>();
                
                RectTransform descRect = cardDescription.GetComponent<RectTransform>();
                descRect.anchorMin = new Vector2(0.05f, 0.25f);
                descRect.anchorMax = new Vector2(0.95f, 0.45f);
                descRect.offsetMin = Vector2.zero;
                descRect.offsetMax = Vector2.zero;
                
                cardDescription.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                cardDescription.fontSize = 24;
                cardDescription.alignment = TextAnchor.UpperCenter;
                cardDescription.color = Color.white;
                cardDescription.lineSpacing = 1.2f;
            }
            
            // Setup choice buttons
            SetupChoiceButton(ref choiceAButton, ref choiceAText, "ChoiceAButton", new Vector2(0, 0.05f), new Vector2(0.45f, 0.2f));
            SetupChoiceButton(ref choiceBButton, ref choiceBText, "ChoiceBButton", new Vector2(0.55f, 0.05f), new Vector2(1, 0.2f));
        }
        
        private void SetupChoiceButton(ref Button button, ref Text buttonText, string name, Vector2 anchorMin, Vector2 anchorMax)
        {
            if (button == null)
            {
                GameObject btnGO = new GameObject(name);
                btnGO.transform.SetParent(cardPanel, false);
                button = btnGO.AddComponent<Button>();
                btnGO.AddComponent<Image>().color = new Color(0.5f, 0.3f, 0.2f, 1f);
                
                RectTransform btnRect = button.GetComponent<RectTransform>();
                btnRect.anchorMin = anchorMin;
                btnRect.anchorMax = anchorMax;
                btnRect.offsetMin = Vector2.zero;
                btnRect.offsetMax = Vector2.zero;
                
                // Button text
                GameObject txtGO = new GameObject("Text");
                txtGO.transform.SetParent(btnGO.transform, false);
                buttonText = txtGO.AddComponent<Text>();
                
                RectTransform txtRect = buttonText.GetComponent<RectTransform>();
                txtRect.anchorMin = Vector2.zero;
                txtRect.anchorMax = Vector2.one;
                txtRect.offsetMin = Vector2.zero;
                txtRect.offsetMax = Vector2.zero;
                
                buttonText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                buttonText.fontSize = 20;
                buttonText.alignment = TextAnchor.MiddleCenter;
                buttonText.color = Color.white;
            }
        }
        
        private void SetupGameOverPanel()
        {
            if (gameOverPanel == null)
            {
                gameOverPanel = new GameObject("GameOverPanel");
                gameOverPanel.transform.SetParent(mainPanel, false);
                gameOverPanel.AddComponent<Image>().color = new Color(0, 0, 0, 0.8f);
                
                RectTransform goRect = gameOverPanel.GetComponent<RectTransform>();
                goRect.anchorMin = Vector2.zero;
                goRect.anchorMax = Vector2.one;
                goRect.offsetMin = Vector2.zero;
                goRect.offsetMax = Vector2.zero;
                
                gameOverPanel.SetActive(false);
            }
        }
        
        // Method to create stat UI elements
        public GameObject CreateStatElement(string statName, Sprite icon)
        {
            GameObject statGO = new GameObject($"Stat_{statName}");
            statGO.transform.SetParent(statPanel.transform, false);
            
            // Add layout element
            LayoutElement layout = statGO.AddComponent<LayoutElement>();
            layout.preferredWidth = 200;
            layout.preferredHeight = 80;
            
            // Icon
            GameObject iconGO = new GameObject("Icon");
            iconGO.transform.SetParent(statGO.transform, false);
            Image iconImage = iconGO.AddComponent<Image>();
            if (icon != null) iconImage.sprite = icon;
            
            RectTransform iconRect = iconImage.GetComponent<RectTransform>();
            iconRect.anchorMin = new Vector2(0, 0.2f);
            iconRect.anchorMax = new Vector2(0.4f, 0.8f);
            iconRect.offsetMin = Vector2.zero;
            iconRect.offsetMax = Vector2.zero;
            
            // Text
            GameObject textGO = new GameObject("Text");
            textGO.transform.SetParent(statGO.transform, false);
            Text statText = textGO.AddComponent<Text>();
            
            RectTransform textRect = statText.GetComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0.4f, 0);
            textRect.anchorMax = new Vector2(1, 1);
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;
            
            statText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            statText.fontSize = 18;
            statText.alignment = TextAnchor.MiddleCenter;
            statText.color = Color.white;
            statText.text = "50%";
            
            return statGO;
        }
    }
} 