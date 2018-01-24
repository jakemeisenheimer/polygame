using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Rigidbody rb;
    public float speed = 8f;
    public float rollTimer = 0;
    public bool rolling;
    public bool walking;
    public bool idle;
    public bool attack;
    public bool jumping;
    public bool jumped;
    public bool OnGround;
    public bool hit;
    public bool isHit;
    public bool AttackedAnimationPlayed;
    private bool paused;
    public Animator anim;
    public Camera camera;
    public GameObject[] weapons;
    public GameObject activeWeapon;
    public CapsuleCollider capColider;
    public float attackTimer = 0;
    public float jumpTimer = 0;
    public float hitTimer = 0;
    public int activeWeaponsNumbers = 0;
    public int attackChainNumber;
    public Transform hand;
    private Vector3 gravity = new Vector3(0, -9.8f, 0);
    private string[] rollanimNames = { "Unarmed-Roll-Forward", "Unarmed-Roll-Right", "Unarmed-Roll-Backward", "Unarmed-Roll-Left" };
    private float xVelocity;
    private float zVelocity;
    private Vector3 gravityadditions = new Vector3(0,0,0);
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.J))
        {
            hit = true;
        }
        Hit();
        Paused();
        Walking();
        Roll();
        Attack();
        Jumping();
    }

    void Paused() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Time.timeScale = 0f;
                paused = true;
            }
            else
            {
                paused = false;
                Time.timeScale = 1f;
            }
        }
    }

    void Hit() {
        hitTimer += Time.deltaTime;
        if(!rolling && !jumping && !walking &&! attack && hit && !isHit && hitTimer > .6)
        {
            hitTimer = 0;
            isHit = true;
            anim.Play("Unarmed-GetHit-R1");
        }else if (isHit && hitTimer > .6)
        {
            
            hit = false;
            isHit = false;
            anim.Play("Unarmed-Idle");
        }
    }
    void Walking() {
        float horizontal = Input.GetAxis("Horizontal");
        Debug.Log(horizontal);
        float vertical = Input.GetAxis("Vertical");
        if (!rolling && !jumping && (Mathf.Abs(horizontal) > .1 || Mathf.Abs(vertical) > .1)&& !attack)
        {
            walking = true;
            Vector3 movment = (vertical * camera.transform.TransformDirection(Vector3.forward)) + (horizontal * camera.transform.TransformDirection(Vector3.right));
            movment.y = 0;
            movment.Normalize();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.Play("Unarmed-Run");
                rb.velocity = (movment * 16) + gravity;
            }
            else
            {
                anim.Play("Unarmed-Walk-Slow");
                rb.velocity = (movment * 8)+ gravity;
            }
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(movment.x, 0, movment.z), Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(rb.rotation, targetRotation, 5 * Time.deltaTime);
            rb.MoveRotation(newRotation);
        }
        else if (walking && !rolling && !jumping) {
            walking = false;
            rb.velocity = Vector3.zero;
            anim.Play("Unarmed-Idle");
        }
    }

    void Roll() {
        rollTimer += Time.deltaTime;
        RollAnimation(KeyCode.W, 4,  camera.transform.TransformDirection(Vector3.forward));
        RollAnimation(KeyCode.S, 2, -camera.transform.TransformDirection(Vector3.forward));
        RollAnimation(KeyCode.A, 3, -camera.transform.TransformDirection(Vector3.right));
        RollAnimation(KeyCode.D, 1, camera.transform.TransformDirection(Vector3.right));
         if (rollTimer > .6f && rolling) {
            capColider.height = 2.5f;
            capColider.center = (new Vector3(0, 1.25f, 0));
            rolling = false;
            rb.velocity = Vector3.zero;
            idle = true;
            anim.Play("Unarmed-Idle");
        }
    }

    bool RollAnimation(KeyCode keypress2,int rollAnimNumber,Vector3 directioon)
    {
        if (!jumping && !attack && Input.GetKey(KeyCode.C) && Input.GetKey(keypress2) && rollTimer > .7f && rolling == false)
        {
            capColider.height = 1.5f;
            capColider.center = (new Vector3(0, .75f, 0));
            rolling = true;
            anim.Play(rollanimNames[FindRollDirectionAnimation(rollAnimNumber)]);
            rollTimer = 0;
            directioon.y = 0;
            Vector3 roll = directioon * speed*3;
            rb.velocity = roll;
            return true;
        }
        return false;
    }

    int FindRollDirectionAnimation(int number) {
        // figures out which direction the player is facing relitive to the camera and changes which roll animation will be played
        int Yangle = (int)(camera.transform.localRotation.eulerAngles.y);

        
        if (Yangle > 45 && Yangle < 135)
        {
            number += -3;
        }
        else if (Yangle > 225 && Yangle < 315)
        {
            number += -1;
        }
        else if (Yangle > 135 && Yangle < 225)
        {
            number += -2;
        }
        if (number < 0) {
            number += 4;
        }
        if (number > 3)
        {
            number -= 4;
        }

        return number;
    }

   public void equipWeapons(int itemSpacenumber) {
        Quaternion ro = hand.rotation;
        if (itemSpacenumber == 0)
        {
            activeWeaponsNumbers = 0;
            Destroy(activeWeapon);
        }
        else if (itemSpacenumber == 1) {
            Destroy(activeWeapon);
            activeWeapon = Instantiate(weapons[2], hand.position, ro, hand);
            activeWeapon.transform.Rotate(80, -30, 0);
            activeWeapon.transform.Translate(-.1f, 0, 0);
            activeWeaponsNumbers = 3;
        }
        else if (itemSpacenumber == 2)
        {
            Destroy(activeWeapon);
            activeWeapon = Instantiate(weapons[0], hand.position, ro, hand);
            activeWeapon.transform.Rotate(0, -30, 0);
            activeWeapon.transform.Translate(-.1f, 0, 0);
            activeWeaponsNumbers = 1;
        }
        else if (itemSpacenumber == 3)
        {
            Destroy(activeWeapon);
            activeWeapon = Instantiate(weapons[7], hand.position, ro, hand);
            activeWeapon.transform.Rotate(50, 180, 100);
            activeWeapon.transform.transform.localPosition = new Vector3(-0.294f, -0.297f, 0.471f);
            activeWeaponsNumbers = 8;
        }
        else if (itemSpacenumber == 4)
        {
           
            Destroy(activeWeapon);
            activeWeapon = Instantiate(weapons[4], hand.position, ro, hand);
            activeWeapon.transform.Rotate(0, -30, 0);
            activeWeapon.transform.Translate(-.1f, 0, 0);
            activeWeaponsNumbers = 5;
        }
        else if (itemSpacenumber == 5)
        {
            Destroy(activeWeapon);
            activeWeapon = Instantiate(weapons[1], hand.position, ro, hand);
            activeWeapon.transform.Rotate(0, -30, 0);
            activeWeaponsNumbers = 2;
        }
        else if (itemSpacenumber == 6)
        {
            Destroy(activeWeapon);
            activeWeapon = Instantiate(weapons[3], hand.position, ro, hand);
            activeWeapon.transform.Rotate(0, -30, 255);
            activeWeaponsNumbers = 4;
        }
        else if (itemSpacenumber == 7)
        {
            Destroy(activeWeapon);
            activeWeapon = Instantiate(weapons[6], hand.position, ro, hand);
            activeWeapon.transform.Rotate(80, -30, 0);
            activeWeapon.transform.Translate(-.1f, 0, 0);
            activeWeaponsNumbers = 7;
        }
        else if (itemSpacenumber == 8)
        {
            Destroy(activeWeapon);
            activeWeapon = Instantiate(weapons[5], hand.position, ro, hand);
            activeWeapon.transform.Rotate(50, 180, 0);
            activeWeaponsNumbers = 6;
        }
    }
    
    
    
 
    void Attack() {
        attackTimer += Time.deltaTime;
        if (activeWeaponsNumbers == 6)
        {
            GetComponentInChildren<LineRenderer>().SetPosition(0, transform.position + Vector3.up * 1.5f);
            GetComponentInChildren<LineRenderer>().SetPosition(1, transform.position + transform.forward * 20 + Vector3.up * 1.5f);
            Debug.Log(transform.forward);
        }
        if (!rolling && !jumping && Input.GetMouseButton(0) && attackTimer > .8)
        {
            attack = true;
            AttackedAnimationPlayed = false;
            attackTimer = 0;

        } else if (!AttackedAnimationPlayed) {
            AttackedAnimationPlayed = true;
            if (activeWeaponsNumbers == 0)
            {
                if (attackChainNumber == 3)
                {
                    attackChainNumber = 0;
                }
                if (attackChainNumber == 0)
                {
                    anim.Play("Unarmed-Attack-R3");
                }
                else if (attackChainNumber == 1)
                {
                    anim.Play("Unarmed-Attack-L3");
                }
                else if (attackChainNumber == 2)
                {
                    anim.Play("Unarmed-Attack-Kick-R1");
                }
                attackChainNumber++;
            }
            else if (activeWeaponsNumbers == 6) {   
                anim.Play("Unarmed-Attack-L3");
                GameObject arrow = Instantiate(weapons[8], transform.position+transform.forward + Vector3.up, Quaternion.identity);
                arrow.GetComponent<Rigidbody>().velocity = transform.forward * 20 + Vector3.up * 5;
                arrow.transform.Rotate(0, transform.eulerAngles.y+90, 0);
                Destroy(arrow, 4);
            }
            else if(activeWeaponsNumbers == 1 || activeWeaponsNumbers == 3 || activeWeaponsNumbers == 8)
            {
                if (attackChainNumber == 3)
                {
                    attackChainNumber = 0;
                }
                if (attackChainNumber == 0)
                {
                    anim.Play("attack1left");
                }
                else if (attackChainNumber == 1)
                {
                    anim.Play("attack2right");
                }
                else if (attackChainNumber == 2)
                {
                    anim.Play("attack3front");
                }
                attackChainNumber++;
            }
            else if (activeWeaponsNumbers == 2 || activeWeaponsNumbers == 5)
            {
                if (attackChainNumber == 3)
                {
                    attackChainNumber = 0;
                }
                if (attackChainNumber == 0)
                {
                    anim.Play("attack1left slow");
                }
                else if (attackChainNumber == 1)
                {
                    anim.Play("attack2right slow");
                }
                else if (attackChainNumber == 2)
                {
                    anim.Play("attack3front slow");
                }
                attackChainNumber++;
            }
            else if (activeWeaponsNumbers == 7)
            {
                anim.Play("attack3front slow");
            }
            else if (activeWeaponsNumbers == 4)
            {
                anim.Play("attack1left fast");
            }
        }
        else if (attackTimer > .7f && attack) {
            attack = false;
            anim.Play("Unarmed-Idle");
        }
    }
    void Jumping() {
        jumpTimer += Time.deltaTime;
        if (OnGround && Input.GetKeyDown(KeyCode.Space))
        {
            jumped = true;
            jumping = true;
            jumpTimer = 0;
            OnGround = false;
            xVelocity = rb.velocity.x;
            zVelocity = rb.velocity.z;
        }
        else if (jumped && !OnGround)
        {
            jumped = false;
            rb.velocity = (new Vector3(0, 13, 0));
            anim.Play("Unarmed-Jump");
            rb.velocity = new Vector3( xVelocity, rb.velocity.y, zVelocity);
        }
        else if (!OnGround && jumpTimer < .5f)
        {
            gravityadditions.y += -1f * Time.deltaTime;
            rb.velocity = new Vector3(xVelocity, rb.velocity.y + gravityadditions.y, zVelocity);
        }
        else if(jumpTimer > .5f && !OnGround)
        {
            gravityadditions.y += -1f * Time.deltaTime;
            Vector3 movment = (Input.GetAxis("Vertical") * camera.transform.TransformDirection(Vector3.forward)) + (Input.GetAxis("Horizontal") * camera.transform.TransformDirection(Vector3.right));
            movment.Normalize();
            anim.Play("Unarmed-Fall");
            rb.velocity = new Vector3(xVelocity,rb.velocity.y + gravityadditions.y, zVelocity);
        }
        else if (OnGround && jumping) {
            anim.Play("Unarmed-Idle");
            jumping = false;
            rb.velocity = Vector3.zero;
            gravityadditions.y = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "chunk") {
            OnGround = true;
        }
        if (collision.gameObject.tag == "falling block")
        {
            OnGround = true;
            collision.gameObject.GetComponent<FallingBlock>().active = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "chunk")
        {
            other.GetComponentInParent<HexGridChunk>().loadTrigger.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "chunk")
        {
            other.GetComponentInParent<HexGridChunk>().loadTrigger.SetActive(false);
        }
    }
}
