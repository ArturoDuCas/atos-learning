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
    public TransitionSettings transition;
    public float loadDelay;
    
    private float screenBottom; 

    CountDown countDownComponent;



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        scale = transform.localScale;
        countDownComponent = GameObject.Find("CountDown").GetComponent<CountDown>(); 

        screenBottom = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;

    }

    // Update is called once per frame
    private void Update()
    {
        if(countDownComponent.currentTime   == 0f) {
            return; 
        }
        Vector2 pos = transform.position;
        if (pos.y < screenBottom)
        {
            TransitionManager.Instance().Transition(scene, transition, loadDelay); // Carga la siguiente escena        
        }


        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationUpdate();
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
