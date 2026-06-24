using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    [Header("Hareket Ayarları")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float LateralSmoothSpeed = 15f;

    [Header("Şerit Koordinatları")]
    [SerializeField] private float[] xPosition = { -0.4f, 1f, 0f };
    private int currentXpositonIndex = 1;
    public bool isAlive = true;

    [Header("UI (Arayüz) Ayarları")]
    [SerializeField] private GameObject gameOverScreen;

    [Header("Ses Efekti Ayarları")]
    [SerializeField] private AudioClip runSound;     
    [SerializeField] private AudioClip deathSound;   
    private AudioSource audioSource;                 

    // YENİ EKLENEN BÖLÜM: Arka Plan Müziği Hoparlörü
    [Header("Müzik Ayarları")]
    [SerializeField] private AudioSource backgroundMusicSource; 

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Karakterin üstündeki hoparlör (Efektler için)

        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        Time.timeScale = 1f; 

        // KOŞMA SESİNİ BAŞLAT
        if (runSound != null && audioSource != null)
        {
            audioSource.clip = runSound;
            audioSource.loop = true; 
            audioSource.Play();
        }

        // YENİ: ARKA PLAN MÜZİĞİNİ BAŞLAT
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Play();
        }
    }

    void Update()
    {
        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.A) && currentXpositonIndex > 0)
                currentXpositonIndex--;
            else if (Input.GetKeyDown(KeyCode.D) && currentXpositonIndex < 2)
                currentXpositonIndex++;
        }
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            float moveZ = speed * Time.fixedDeltaTime;
            float targetX = xPosition[currentXpositonIndex];
            float nextX = Mathf.Lerp(rb.position.x, targetX, Time.fixedDeltaTime * LateralSmoothSpeed);

            Vector3 finalPosition = new Vector3(nextX, rb.position.y, rb.position.z + moveZ);
            rb.MovePosition(finalPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cars"))
        {
            isAlive = false;
            animator.SetBool("Die", true);
            
            // 1. SES EFEKTLERİNİ AYARLA (Koşmayı durdur, çarpma sesi çal)
            if (audioSource != null)
            {
                audioSource.Stop(); 
                audioSource.PlayOneShot(deathSound); 
            }

            // YENİ: 2. ARKA PLAN MÜZİĞİNİ DURDUR
            if (backgroundMusicSource != null)
            {
                backgroundMusicSource.Stop();
            }

            // 3. GAME OVER EKRANINI AÇ
            if (gameOverScreen != null)
            {
                gameOverScreen.SetActive(true);
            }

            // 4. OYUNU DONDUR
            Time.timeScale = 0f; 
        }
    }
}