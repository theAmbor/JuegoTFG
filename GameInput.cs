using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;


    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty); //Si OnInteractAction no devuelve null, se invoca el EventHandler
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); //Aqui guardamos en qu� direcci�n se est� moviendo el jugador. Por ejemplo, hacia delante (0, 1)

        inputVector = inputVector.normalized;

        return inputVector;
    }

}
