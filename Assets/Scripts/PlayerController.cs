using Cinemachine;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 5f;
    public float gravity = -9.81f;
    public float attackCooltime = 3f;

    float healingManaScale = 5f;

    public CinemachineVirtualCamera virtualCam;
    public float rotationSpeed = 10f;
    private CinemachinePOV pov;
    private CharacterController characterController;
    private Vector3 velocity;
    public bool isGrounded;
    public CinemachineSwitcher cs;

    public float maxHP = 100;
    public float currentHP;

    public float maxMana = 100f;
    public float currentMana;

    public Slider hpSlider;
    public Slider manaSlider;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        pov = virtualCam.GetCinemachineComponent<CinemachinePOV>();

        currentHP = maxHP;
        hpSlider.value = 1f;

        currentMana = maxMana;
        hpSlider.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            pov.m_HorizontalAxis.Value = transform.eulerAngles.y;
            pov.m_VerticalAxis.Value = 0f;
        }

        isGrounded = characterController.isGrounded;
        if (isGrounded)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 camForward = virtualCam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = virtualCam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = (camForward * z + camRight * x).normalized;
        if (!cs.usingFreelook)
        characterController.Move(move * speed * Time.deltaTime);

        float cameraYaw = pov.m_HorizontalAxis.Value;
        Quaternion targetRot = Quaternion.Euler(0f, cameraYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpPower;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if (isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 10f;
            virtualCam.m_Lens.FieldOfView = 80f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
            virtualCam.m_Lens.FieldOfView = 60f;
        }

        HealMana();
        Manaconsume();
    }

   
        public void PlayerTakeDamage(int damage)
    {
        currentHP -= damage;
        hpSlider.value = (float)currentHP / maxHP;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Manaconsume()
    {
        manaSlider.value = (float)currentMana / maxMana;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void HealMana()
    {
       if(currentMana < maxMana)
        {
            healingManaScale = 5f;
            currentMana += healingManaScale * Time.deltaTime;
        }
       else if(currentMana == maxMana)
        {
          healingManaScale = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MagicBook>() != null)
        {
            string info = other.GetComponent<MagicBook>().objInfo;
            if (info.StartsWith("Scene"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(info);
            }
        }
    }
}

