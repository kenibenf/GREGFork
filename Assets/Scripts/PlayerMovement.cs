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
    private InputAction moveAction;
    private InputAction shiftAction;
    private float shiftt = 1; // forse c'è un modo più efficiente per farlo, ma cosi funziona

    Vector2 oldPos;

    private void Start()
    {
        moveAction = GetComponent<PlayerInput>().actions["Move"];
        shiftAction = GetComponent<PlayerInput>().actions["Shift"];
        // GetComponent "preleva" il componente attaccato all'oggetto, in questo caso preleva il componente BoxCollider2D dalla sprite del player_0
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        this.transform.position = PlayerState.getPosition();
    }
    
    private void Update()
    {

        //PER MUOVERSI
        moveDelta = moveAction.ReadValue<Vector2>(); // prende il valore dall'azione Move, che è passato tramite l'input system di unity
        x = moveDelta.x; 
        y = moveDelta.y;

        //PER SHIFTARE
        shiftAction.performed += OnShift; // se e' stato premuto shift, allora esegui OnShift()
        shiftAction.canceled += OnShiftReleased; // se e' stato rilasciato shift, allora esegui OnShiftReleased()


        speed = PlayerState.speed * shiftt;
        rb.velocity = moveDelta * speed;
        this.Animate();
    }
    // sinceramente il parametro Callbackcontext non so cosa sia, ma serviva affinche funzionasse.
    private void OnShift(InputAction.CallbackContext contex){ 
        shiftt = shiftDownSpeed; //setto la velocità a 0.5
    }

    private void OnShiftReleased(InputAction.CallbackContext contex){
        shiftt = 1; // una volta rilasciato shift, risetto la velocità a 1
    }
    
    /* 
        funzione chiamata ogni volta che l'utente preme wasd/freccette 
        (se si vuole cambiare combinazione di tasti o aggiungerne altri si fa dal componente Player Input del Player)
    */

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