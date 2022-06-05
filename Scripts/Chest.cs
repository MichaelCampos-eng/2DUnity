using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{   
    public Sprite emptyChest;
    public int dolAmount = 15;

    protected override void OnCollect() {
        if (!collected) {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.dolorado += dolAmount;
            GameManager.instance.ShowText("+" + dolAmount + " Dolorados!", 30, Color.white, transform.position, Vector3.up * 30, 1.0f);
        }    
    }
}
