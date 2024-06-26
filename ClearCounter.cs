using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {


    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // No hay ning�n KitchenObject aqu�
            if (player.HasKitchenObject()) {
                // El jugador est� sujetando algo
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // El jugador no est� sujetando nada
            }
        } else {
            // Hay un KitchenObject aqu�
            if (player.HasKitchenObject()) {
                // El jugador est� sujetando algo
            } else {
                // El jugador no est� sujetando nada
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
