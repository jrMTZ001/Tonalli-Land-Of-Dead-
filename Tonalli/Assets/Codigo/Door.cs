using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    /*
    public Animator anim;
    public float distanceToOpen;
    private Jugador thePlayer;
    public Transform exitPoint;
    public float moveSpeedPlayer;
    private bool playerExiting;
    void Start()
    {
        thePlayer = PlayerHealthController.Instance.GetComponent<Jugador>();
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, thePlayer.transform.position) < distanceToOpen)
        {
            anim.SetBool("doorOpen", true);
        }
        else
        {
            anim.SetBool("doorOpen", false);
        }
        if(playerExiting)
        {
            thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, exitPoint.position, moveSpeedPlayer * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!playerExiting)
            {
                thePlayer.canMove = false;
                StartCoroutine(UseDoorCo());
            }
        }
    }

    IEnumerator UseDoorCo()
    {   
        playerExiting = true;
        thePlayer.anim.enabled = false;
        yield return new WaitForSeconds(1.5f);
        
    }
    */
    /*
    public string sceneToLoad; // La escena a la que se va a cargar cuando el jugador pase por la puerta

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Carga la siguiente escena
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    */
    /*
    public string sceneToLoad;
    public Transform portalPosition;  // Asignar la posición del portal de entrada

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Guardar la posición de la puerta antes de cambiar de escena
            PlayerPositionManager.SavePlayerPosition(portalPosition.position);

            // Cargar la nueva escena
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    */
    public string sceneToLoad; // La escena a la que se va a cargar cuando el jugador pase por la puerta

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Carga la siguiente escena
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}