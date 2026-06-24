using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçişleri için gerekli kütüphane

public class MenuManager : MonoBehaviour
{
    // Play butonuna tıklandığında çalışacak fonksiyon
    public void StartGame()
    {
        // 1 numaralı sahneyi (GameScene) yükler
        SceneManager.LoadScene(1); 
    }
}