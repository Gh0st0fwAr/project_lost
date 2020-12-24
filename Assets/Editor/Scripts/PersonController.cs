using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PersonController : MonoBehaviour //класс, отвечающий за управлениеи работу персонажа
{
    private int _score = 0; //счатчик для кристаллов
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            PlayerPrefs.SetInt("score", _score);
            scoreText.text = Score.ToString();
            if (_score == 200)
            {
                on200CoinsCollected.Invoke();
            }
        }
    }
    
    public static UnityEvent on200CoinsCollected = new UnityEvent(); //запуск события при 200 кристаллах
    public static UnityEvent onShubaCollected = new UnityEvent(); 

    public Text scoreText;
    public GameObject person;
    public Rigidbody2D rb;
    public float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    public bool isGrounded;
    public bool alive = true;
    public GameObject deathObject;
    public Animator animator;
    public float stairSpeed;
    public bool blocked;
    public Text questText;

    public Sprite kyleNormalSprite;
    public Sprite kylePanicSprite;
    
    public Sprite fateNormalSprite;
    public Sprite fatePanicSprite;

    public Sprite robotSprite;
    

    private QuestController _questController;


    private bool _nearStair;
    private DataObject _dataObject;

    // Start is called before the first frame update
    void Start() // запуск сцены
    {
        var valueFromPrefs = PlayerPrefs.GetInt("score");
        if (valueFromPrefs != 0)
        {
            Score = valueFromPrefs;
        }

        _questController = FindObjectOfType<QuestController>();
        _dataObject = FindObjectOfType<DataObject>();
        if (_dataObject.currentQuest != null)
            SetQuestText(_dataObject.currentQuest);
        else
        {
            questText.text = "";
        }
    }

    // Update is called once per frame
    void Update() // проверка взаимодействия персонажа и триггеров, задание условий анимаций
    {
        if (alive)
        {
            if (blocked)
                return;
            float moveX = Input.GetAxis("Horizontal");
            if (Input.GetAxis("Horizontal") != 0)
            {
                if (moveX < 0)
                {
                    person.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
                else if (moveX > 0)
                {
                    person.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                
                rb.velocity = new Vector2(moveX * speed * Time.deltaTime, rb.velocity.y);

                if (isGrounded)
                {
                    animator.Play("Walk");
                    
                }
            }

            float moveY = Input.GetAxis("Vertical");
            if (moveY != 0 && _nearStair)
            {
                animator.Play("LadderUp");
                var pos = transform.position;
                float speed;
                if (moveY > 0)
                {
                    speed = stairSpeed * Time.deltaTime;
                }
                else
                {
                    speed = stairSpeed * Time.deltaTime;
                    speed *= -1;
                }
                
                Debug.Log($"moveY = {moveY} speed = {speed}");
                rb.MovePosition(new Vector2(pos.x, pos.y + speed));
            }

            float jump = Input.GetAxis("Jump");
            if (jump != 0 && isGrounded)
            {
                Jump();
            }

            if (rb.velocity == Vector2.zero)
            {
                animator.Play("Stay");
            }

            if (Input.GetKeyDown(KeyCode.E) && _questController.CollideWithCharacter)
            {
                _questController.CreateDialog(kyleNormalSprite, fateNormalSprite);
            }
            if (Input.GetKeyDown(KeyCode.E) && _questController.CollideWithJump)
            {
                Destroy(GameObject.FindWithTag("Jump"));
                _dataObject.currentQuest.completed = true;
                _dataObject.currentQuest = _dataObject.questList.GetQuest(QuestType.ComeBack);
                SetQuestText(_dataObject.currentQuest);
            }
        }
        else
        {
            deathObject.SetActive(true);
            Destroy(person);
        }
    }

    private void Jump() // функция для прыжка
    {
        rb.AddForce(Vector2.up * jumpForce);
        isGrounded = false;
        animator.Play("Jump");
    }

    private void OnCollisionEnter2D(Collision2D other) // функция состояний
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Death"))
        {
            alive = false;
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            Score += 50;
        }
        
        if (other.gameObject.CompareTag("Shuba"))
        {
            Destroy(other.gameObject);
            Score -= 200;
            onShubaCollected.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // функция состояний для триггеров
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            Score += 50;
        }
        
        if (other.gameObject.CompareTag("Stair"))
        {
            Debug.Log("Stay near stair");
            _nearStair = true;
            rb.isKinematic = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)  // функция состояний для дебага лестниц
    {
        if (other.gameObject.CompareTag("Stair"))
        {
            Debug.Log("Dont stay near stair");
            _nearStair = false;
            rb.isKinematic = false;
        }
    }

    public void SetQuestText(Quest quest) // функция состояний для квестов
    {
        questText.text = quest.description;
        _dataObject.currentQuest = quest;
    }
}
