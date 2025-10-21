using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState { Idle, Attackpreparing, Attack }
    public PlayerState state = PlayerState.Idle;

    public float speed = 5f;
    public float jumpPower = 5f;
    public float gravity = -9.81f;
    public float attackCooltime = 3f;

    public CinemachineVirtualCamera virtualCam;
    public float rotationSpeed = 10f;
    private CinemachinePOV pov;
    private CharacterController characterController;
    private Vector3 velocity;
    public bool isGrounded;
    public CinemachineSwitcher cs;
    public PlayerShooting Shooter;

    public int maxHP = 100;
    private int currentHP;

    public Slider hpSlider;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        pov = virtualCam.GetCinemachineComponent<CinemachinePOV>();

        currentHP = maxHP;
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

        switch (state)
        {
            case PlayerState.Idle:
                if (Input.GetMouseButtonDown(0))
                    state = PlayerState.Attackpreparing;
                break;

            case PlayerState.Attackpreparing:
               if()
                break;

            case PlayerState.Attack:
              
               
                break;
        }
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

  void Die()
    {
        Destroy(gameObject);
    }
}

