using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    [SerializeField] private float moveSpeed = 7f; //Velocidad de movimiento
    [SerializeField] private GameInput gameInput;

    private bool isWalking;

    private void Update() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); //Creamos un vector3 y sustituimos los ejes x y z por las cordenadas del vector2

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            //No podemos movernos hacia moveDir

            //Se intenta mover solo en X
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                //Solo se puede mover en X
                moveDir = moveDirX;
            } else {
                //No se puede mover solamente en X

                //Se intenta mover solo en Z
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    //Solo se puede mover en Z
                    moveDir = moveDirZ;
                } else {
                    //No se puede mover en ninguna dirección
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero; //Si la velocidad de movimiento es diferente de 0, la variable "estáCaminando" cambiará a true

        float rotateSpeed = 10f; //La velocidad de rotacion del personaje
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); //Actualizamos el "forward" para que el personaje gire en la dirección a la que está caminando
       
    }

    public bool IsWalking() {
        return isWalking;
    }

}
