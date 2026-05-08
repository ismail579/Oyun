using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator animator;

    [Header("Hareket Ayarları")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float LateralSmoothSpeed = 15f;

    [Header("Şerit Koordinatları")]
    [SerializeField] private float[] xPosition = { -0.4f, -0.2f, 0f };
    private int currentXpositonIndex = 1;
    public bool isAlive = true;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.A) && currentXpositonIndex > 0)
            {
                currentXpositonIndex--;
            }
            else if (Input.GetKeyDown(KeyCode.D) && currentXpositonIndex < 2)
            {
                currentXpositonIndex++;
            }
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cars"))
        {
            isAlive = false;
            animator.SetBool("Die", true);
        }
    }
}