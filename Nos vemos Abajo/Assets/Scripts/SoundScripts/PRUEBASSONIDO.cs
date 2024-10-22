using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRUEBASSONIDO : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    public float distance = 5f; // Distancia máxima de desplazamiento

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        // Movimiento continuo de un lado a otro usando Mathf.PingPong
        //float movement = Mathf.PingPong(Time.time * speed, distance);
        //transform.position = startPosition + new Vector3(0, 0, movement);

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SoundManager.instance.PlaySound("vientoFuerte", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SoundManager.instance.PlaySound("susto", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SoundManager.instance.PlaySound("cogerCuchillo", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SoundManager.instance.PlaySound("risaIncomoda", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SoundManager.instance.PlaySound("hablar", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SoundManager.instance.PlaySound("gritoMujer", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SoundManager.instance.PlaySound("golpeIntro", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            SoundManager.instance.PlaySound("cogerFarol", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SoundManager.instance.PlaySound("cortarCuerda", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            SoundManager.instance.PlaySound("botonFarol", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SoundManager.instance.PlaySound("valvulaFarol", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SoundManager.instance.PlaySound("cogerBoli", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SoundManager.instance.PlaySound("firmar", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SoundManager.instance.PlaySound("vientoFuerte", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SoundManager.instance.PlaySound("sonidomiedo1", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SoundManager.instance.PlaySound("sonidomiedo2", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SoundManager.instance.PlaySound("cogerBloqueado", transform.position, gameObject);
        }
    }
}
