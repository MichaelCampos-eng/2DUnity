using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Weapon : Collidable
{
    // Damage struct
    public int[] damagePoint = { 1, 2 };
    public float[] pushForce = { 2.0f, 2.5f };

    // Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    // Swing
    private Animator anim; 
    private float coolDown = 0.5f;
    private float lastSwing;
    private static Random rnd = new Random();


    protected override void Start() {
        base.Start();
        anim =GetComponent<Animator>();
    }

    protected override void Update() {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Time.time - lastSwing > coolDown) {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll) {
        if (coll.tag == "Fighter") {
            if (coll.name != "Player") {


                
                // Create a new damage object, then we'll send it to the fighter we've hit
                Damage dmg = new Damage 
                {
                    damageAmount = damagePoint[weaponLevel],
                    origin = transform.position,
                    pushForce = pushForce[weaponLevel]
                };


                coll.SendMessage("RecieveDamage", dmg);
            }
        }
    }

    private void Swing() {
        int trigger = rnd.Next(0, 2);
        if (trigger == 0) {
            anim.SetTrigger("Swing");
        } else if (trigger == 1) {
            anim.SetTrigger("Jab");
        }
        
    }

    public void UpgradeWeapon() {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        // Change stats

    }

    public void SetWeaponLevel(int level) {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

}
