using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeroJaguar : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    private float movimientoHorizontal;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("Salto")]
    public float fuerzaSalto = 12f;
    public Transform chequeoSuelo;
    public LayerMask suelo;
    private bool enSuelo;
    public Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void LateUpdate()
    {
        // Entrada de movimiento
        movimientoHorizontal = Input.GetAxisRaw("Horizontal");

        // Verifica si está en el suelo
        enSuelo = Physics2D.OverlapCircle(chequeoSuelo.position, 0.2f, suelo);

        // Salto
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
            anim.SetFloat("ySpeed", fuerzaSalto);
        }

        //Animations.
        anim.SetFloat("MoveSpeed",Mathf.Abs(rb.velocity.x));
        anim.SetBool("isGrounded", enSuelo);
        // Cambia la dirección del sprite según el movimiento
        if (movimientoHorizontal > 0)
            spriteRenderer.flipX = false;
        else if (movimientoHorizontal < 0)
            spriteRenderer.flipX = true;
    }

    private void FixedUpdate()
    {
        // Movimiento en el eje X
        rb.velocity = new Vector2(movimientoHorizontal * velocidad, rb.velocity.y);
    }
}
