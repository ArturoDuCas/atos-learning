using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition; 

public class PlayerMovement_Exam : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private Vector2 scale;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;


    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 40f;

    public string scene; 
    public TransitionSettings transitionResult;
    public TransitionSettings transitionGame;
    public float loadDelay;
    
    private float screenBottom;
    private float screenTop;  
    private float screenMiddleX; 

    CountDown countDownComponent;

    private Collider2D lastTouched; 
    Color lastTouchedColor; 

    private bool justFinished = true;
    private bool isCorrect; 

    [SerializeField]
    private GameObject confetti; 

    void Awake() {
        lastTouched = null; 
        lastTouchedColor = new Color(.165f, 1f, 0f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(lastTouched == collision.collider) { // Si esta tocando el mismo sulo 
            return; 
        } else {
            if (lastTouched != null) {
                lastTouched.GetComponent<Renderer>().material.color = Color.white;
            }
            lastTouched = collision.collider;
            lastTouched.GetComponent<Renderer>().material.color = lastTouchedColor;
            GameObject answerObject = lastTouched.gameObject.transform.parent.gameObject;
            isCorrect = answerObject.GetComponent<Answer>().isCorrect;
            // isCorrect = lastTouched.GetComponent<Answer>().isCorrect;
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        scale = transform.localScale;
        countDownComponent = GameObject.Find("CountDown").GetComponent<CountDown>(); 

        screenBottom = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
        screenTop = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        screenMiddleX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 0)).x;

    }

    IEnumerator evaluateAnswer() {
        yield return new WaitForSeconds(3f);
        if (isCorrect) {
            GameObject confettiInstance = Instantiate(confetti);
            Vector2 confettiPos = new Vector2(screenMiddleX, screenTop + 1f);
            confettiInstance.transform.position = confettiPos;
            Store.player_correctAnswers++; 
        } else {
            anim.SetBool("Incorrect", true);       
        }

        yield return new WaitForSeconds(3f);
        if (Store.examQuestions.Count == 0) { // Si ya contesto todas las preguntas
            TransitionManager.Instance().Transition("ResultsScene", transitionResult, loadDelay); 
        } else {
            TransitionManager.Instance().Transition("TestsScene", transitionGame, loadDelay); // Carga la siguiente escena
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(countDownComponent.currentTime   == 0f && justFinished) {
            anim.SetBool("Running", false);
            justFinished = false;
            Store.player_answersHistory.Add(isCorrect);
            StartCoroutine(evaluateAnswer()); 
            return; 
        } else if (countDownComponent.currentTime == 0f && !justFinished) {
            return; 
        }


        Vector2 pos = transform.position;
        if (pos.y < screenBottom - 4f) // Si el jugador se cae de la pantalla
        {
            pos.y = screenTop + 2f; 
            pos.x = -12.8f;
            transform.position = pos;
        }


        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationUpdate();
    }

    public void onIncorrectAnimationEnd() {
        anim.SetBool("Incorrect", false); 
    }

    private void UpdateAnimationUpdate()
    {
        
        if (dirX > 0f)
        {
            anim.SetBool("Running", true);
            transform.localScale = scale;
            
        }
        else if (dirX < 0)
        {
            anim.SetBool("Running", true);
            transform.localScale = new Vector2(-scale.x, scale.y);
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

}
