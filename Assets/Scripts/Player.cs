using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed = 5;
    private Rigidbody2D rb2d;

    private float move;

    public float jumpForce = 4;

    private bool isGrounded;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    //variable de animacion
    private Animator animator;

    //vaiables para contar monedas
    //un contador clasico
    private int coins;
    //la varibale del texto
    public TMP_Text textCoins;

    //variable que recogera las coordenadas del techo donde estara el cerdo
    public Transform puntoTecho;


    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

          
        move = Input.GetAxisRaw("Horizontal");
        rb2d.linearVelocity = new Vector2(move*speed, rb2d.linearVelocity.y);

          
        
       
if (move != 0)
{
    // Usamos rotación en lugar de escala para girarlo
    if (move > 0) transform.eulerAngles = new Vector3(0, 0, 0); // Mira a la derecha
    else transform.eulerAngles = new Vector3(0, 180, 0);      // Mira a la izquierda
}   

        //maths.abs hace que siempre sea positivala velocidad
        animator.SetFloat("Speed", Mathf.Abs(move));
        //para ver y controlar la velocidad de y (osea hacia arriba , el salto)
        animator.SetFloat("VerticalVelocity", rb2d.linearVelocity.y);
        animator.SetBool("IsGrounded", isGrounded);

//saltara con la barra espaciadora y cuando este en el suelo
if(Input.GetButtonDown("Jump") && isGrounded)
{

    rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);

}

//que nuestro personaje pueda volar

if (Input.GetKey(KeyCode.V)) 
{

    // se anula la gravedad
    rb2d.gravityScale = 0; 

    //le decimos que la velocidad X (horizontal) sea la misma 
    // y que la de Y sea el la velocidad de la var speed
    rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, speed);
}
else
{
    // una vez soltamos la V el personaje deja de volar
    rb2d.gravityScale = 1; 
}



    }

private void FixedUpdate() {
    //se crea una esfera pegada al cerdo que comprueba si esta pegada al suelo
    isGrounded= Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
}


//metodo para la interacciond e player con la moneda
private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.transform.CompareTag("Coin"))
    {
        Destroy(collision.gameObject);
        coins++;
        textCoins.text = coins.ToString();

        // coge 5 monedas y lo teletrnasporta, al ser igual si coge seis o mas ya no
        if (coins == 5)
        {
            //le indicamos que debe ir a la posicion de puntoTecho
            transform.position = puntoTecho.position;
            
        }
    }

    if (collision.transform.CompareTag("Caida"))
    {     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

}
