using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {


    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;


    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // No hay ningún KitchenObject aquí
            if (player.HasKitchenObject()) {
                // El jugador está sujetando algo
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // El jugador no está sujetando nada
            }
        } else {
            // Hay un KitchenObject aquí
            if (player.HasKitchenObject()) {
                // El jugador está sujetando algo
            } else {
                // El jugador no está sujetando nada
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            // No hay ningún KitchenObject aquí
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }

}
