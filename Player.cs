using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    [SerializeField] private float moveSpeed = 7f; //Velocidad de movimiento

    private bool isWalking;

    private void Update() {
        Vector2 inputVector = new Vector2(0, 0);

        //Moverse hacia delante
        if (Input.GetKey(KeyCode.W)) { 
            inputVector.y = +1;
        }
        //Moverse hacia atrás
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y = -1;
        }
        //Moverse hacia la izquierda
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = -1;
        }
        //Moverse hacia la derecha
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); //Creamos un vector3 y sustituimos los ejes x y z por las cordenadas del vector2
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero; //Si la velocidad de movimiento es diferente de 0, la variable "estáCaminando" cambiará a true

        float rotateSpeed = 10f; //La velocidad de rotacion del personaje
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); //Actualizamos el "forward" para que el personaje gire en la dirección a la que está caminando
       
    }

    public bool IsWalking() {
        return isWalking;
    }

}
