using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    [SerializeField] private float moveSpeed = 7f; //Velocidad de movimiento


    private void Update() {
        Vector2 inputVector = new Vector2(0, 0);

        //Moverse hacia delante
        if (Input.GetKey(KeyCode.W)) { 
            inputVector.y = +1;
        }
        //Moverse hacia atr�s
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

        Debug.Log(Time.deltaTime);
    }

}
