using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    private float movementX;
    private float movementY;
    private Rigidbody rb;
    private int count;
    private int lives;
    private bool playerAdvanced = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        rb = GetComponent<Rigidbody>();
        lives = 3;
        SetCountText();
        SetLivesText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }

        if (count == 8 && playerAdvanced)
        {
            transform.position = new Vector3(40.0f, 0.5f, 0.0f);
            SetCountText();
            playerAdvanced = false;
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        movementX = v.x;
        movementY = v.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 18)
        {
            winTextObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

            if (lives == 0) {
                loseTextObject.SetActive(true);
                Destroy(gameObject);
            }
    }
}