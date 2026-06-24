using UnityEngine;

public class CarMover : MonoBehaviour
{
    [SerializeField] private float carSpeed = 0.01f;

    void Update()
    {
        // Ufak bir tavsiye: carSpeed değerini Time.deltaTime ile çarpmak 
        // arabanın her bilgisayarda aynı hızda gitmesini sağlar. (Örn: carSpeed * Time.deltaTime)
        transform.Translate(0, 0, carSpeed);
    }

    // OnTriggerEnter yerine OnCollisionEnter kullanıyoruz (katı çarpışma için)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Arabanın hızını sıfırla (Çarpınca araba da dursun)
            carSpeed = 0f;
        }
    }
}