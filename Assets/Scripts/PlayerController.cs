using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;   // Rigidbody component’ý tutacak deðiþken. we dont need it outside of this class so its private
    private Animator playerAnim;  //Animator component'ý tutacak deðiþken
    public ParticleSystem expPart;  //Particle'ý tutacak
    public ParticleSystem dirtPart;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool onGround = true;
    public bool gameOver = false;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    
    // Start is called once before 
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();  //Bu script hangi objenin üzerindeyse, onun Rigidbody component’ini al ve playerRb içine koy.
        playerAnim = GetComponent<Animator>();  //animator component'ýný bu deðiþkene koy
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && onGround && !gameOver)
        {
            //roket gibi sürekli deðil, bir anda itiyor.
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            onGround = false;

            playerAnim.SetTrigger("Jump_trig");  //Activate the trigger parameter named Jump_trig rn

            dirtPart.Stop();  //havadayken çýkmasýn

            playerAudio.PlayOneShot(jumpSound, 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            dirtPart.Play();
        }
        
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            dirtPart.Stop();
            playerAudio.PlayOneShot(crashSound, 1f);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            expPart.Play();


        }
    }
}
