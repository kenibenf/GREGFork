using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveDelta;
    public Animator animator;
    private float x, y;
    public float speed;
    private bool isMove;
    public float shiftDownSpeed;

    Vector2 oldPos;

    private void Start()
    {
        // GetComponent "preleva" il componente attaccato all'oggetto, in questo caso preleva il componente BoxCollider2D dalla sprite del player_0
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        this.transform.position = PlayerState.getPosition();
    }
    
    private void Update()
    {
        speed = PlayerState.speed;
        rb.velocity = moveDelta * speed;
        this.Animate();
    }
    
    public void OnShift(){
        speed = speed * shiftDownSpeed;

    }
    /* 
        funzione chiamata ogni volta che l'utente preme wasd/freccette 
        (se si vuole cambiare combinazione di tasti o aggiungerne altri si fa dal componente Player Input del Player)
    */
    public void OnMove(InputValue value)
    {
        moveDelta = value.Get<Vector2>(); // prende il valore da inputValue, che è passato tramite l'input system di unity
        x = moveDelta.x;
        y = moveDelta.y;
    }

    private void Animate()
    {
        //    se le vecchia posizione è diversa da quella nuova isMove viene impostata su true
        //    e viene salvata la posizione corrente come vecchia posizione
        //    in caso contrario isMove viene impostata su false

        if (isMove = (Vector3)oldPos != transform.position)
        {
            oldPos = transform.position;
        }

        if ((x != 0 || y != 0) && speed != 0)
        {
            animator.SetFloat("X", x);
            animator.SetFloat("Y", y);
        }
        animator.SetBool("isMove", isMove);
    }
}