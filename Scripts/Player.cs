using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;

    protected override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdatedMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int skinID) {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinID];
    }
}
