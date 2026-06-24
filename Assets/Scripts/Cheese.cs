using UnityEngine;

public class Cheese : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f; // Peynirin kendi etrafında dönme hızı
    [SerializeField] private int scoreValue = 1; // Bu peynir kaç puan veriyor?

    void Update()
    {
        // Peynirin havada havalı durması için kendi ekseninde döndürüyoruz
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Çarpan objenin tag'i "Player" mı diye kontrol ediyoruz
        if (other.CompareTag("Player"))
        {
            // İleride oluşturacağımız GameManager üzerinden skoru artıracağız
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddCheese(scoreValue);
            }

            // Toplama ses efekti veya partikül eklemek istersen tam buraya yazabilirsin

            // Peyniri sahneden yok et
            Destroy(gameObject);
        }
    }
}