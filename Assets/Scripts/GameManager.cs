using UnityEngine;
using TMPro; // UI için TextMeshPro kullanacağız

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Cheese System")]
    [SerializeField] private TextMeshProUGUI cheeseText; // Ekrandaki yazı bileşeni
    private int currentCheeseCount = 0;

    private void Awake()
    {
        // Singleton Pattern: Sahnede sadece tek bir GameManager olmasını sağlar
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Eğer sahneler arası taşınsın istersen açabilirsin
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCheeseUI();
    }

    // Peynir toplandığında çağrılacak fonksiyon
    public void AddCheese(int amount)
    {
        currentCheeseCount += amount;
        UpdateCheeseUI();
    }

    // Arayüzü güncelleyen fonksiyon
    private void UpdateCheeseUI()
    {
        if (cheeseText != null)
        {
            cheeseText.text = "Peynir: " + currentCheeseCount.ToString();
        }
    }
}