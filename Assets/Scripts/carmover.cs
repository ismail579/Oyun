using UnityEngine;

public class CarMover : MonoBehaviour
{
    [SerializeField] private float carSpeed = 0.01f;

    void Update()
    {
        // Eğer carSpeed 0 ise araba hareket etmez
        transform.Translate(0, 0, carSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Arabanın hızını sıfırla
            carSpeed = 0f;

            // 2. Karakterin hareket scriptini kapat
            var playerMovement = other.GetComponent<MonoBehaviour>(); // Scriptin adını doğru bulması için
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            // 3. EN ÖNEMLİSİ: İkisinin de collider'ını kapat ki birbirlerinin içinden geçemesinler
            // Çarpışma anında fiziksel etkileşimi tamamen kesiyoruz
            this.GetComponent<Collider>().enabled = false;
            other.GetComponent<Collider>().enabled = false;
        }
    }
}