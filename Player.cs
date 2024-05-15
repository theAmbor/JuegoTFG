using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {


    public static Player Instance { get; private set; }

    //Evento que se dispara cuando se selecciona una estanter�a
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {

        public ClearCounter selectedCounter;

    }

    [SerializeField] private float moveSpeed = 7f; //Velocidad de movimiento
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;


    private bool isWalking;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;
    private KitchenObject kitchenObject;


    private void Awake() {

        if (Instance != null) {

            Debug.LogError("Hay m�s de una instancia de Jugador");

        }

        Instance = this;

    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedCounter != null) {

            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {

                //Tiene el componente ClearCounter
                if (clearCounter != selectedCounter) {
                    SetSelectedCounter(clearCounter);
                }

            } else {
                SetSelectedCounter(null);
            }

        } else {
            SetSelectedCounter(null);
        }
    }
         

    private void HandleMovement() {
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
                    //No se puede mover en ninguna direcci�n
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero; //Si la velocidad de movimiento es diferente de 0, la variable "est�Caminando" cambiar� a true

        float rotateSpeed = 10f; //La velocidad de rotacion del personaje
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); //Actualizamos el "forward" para que el personaje gire en la direcci�n a la que est� caminando

    }

    private void SetSelectedCounter(ClearCounter selectedCounter) {

        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {

            selectedCounter = selectedCounter

        });

    }

    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
