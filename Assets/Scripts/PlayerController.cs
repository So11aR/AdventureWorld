using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Image hpBar;
    public int maxHp = 50;
    public int currentHp;

    Rigidbody2D rb;
    float horizontal;
    float vertical;

    public float timeProtect = 2.0f;
    bool isProtect;
    float protectTimer;

    Animator anim;
    Vector2 lookDirection = new Vector2(1, 0);

    public int maxBulletCount;
    public int currentBulletCount;
    public Text currentBulletAmoText;

    public GameObject bulletPrefab;
    public GameObject restartMenu;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHp = maxHp;
        anim = GetComponent<Animator>();

        currentBulletCount = maxBulletCount;
        currentBulletAmoText.text = "Снаряды: " + currentBulletCount.ToString();
        print("Моё кол-во снарядов равно: " + currentBulletCount);
    }
    

    void Launch()
    {
        if(currentBulletCount >= 1)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);

            Bullet bul = bulletObject.GetComponent<Bullet>();
            bul.Launch(lookDirection, 300);

            anim.SetTrigger("Launch");
        }
        else {
            print("У вас закончились патроны");
        }
        
    }

    public void ChangeAmo(int amount)
    {
        currentBulletCount = Mathf.Clamp(currentBulletCount + amount, 0, maxBulletCount);
        currentBulletAmoText.text = "Снаряды: " + currentBulletCount.ToString();
    }

    public void ChangeHp(int amount)
    {
        if(amount < 0)
        {
            if(isProtect)
            {
                return;
            }
            isProtect = true;
            protectTimer = timeProtect;
        }
        currentHp = Mathf.Clamp(currentHp + amount, 0, maxHp);
        hpBar.fillAmount = 1.0f * currentHp / maxHp;
        print(currentHp + " / " + maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHp < 1)
        {
            Destroy(gameObject);
            restartMenu.gameObject.SetActive(true);
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);
        anim.SetFloat("Speed", move.magnitude);

        if(isProtect)
        {
            protectTimer -= Time.deltaTime;
            if(protectTimer < 0)
            {
                isProtect = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
            currentBulletCount--;
            if(currentBulletCount < 1)
            {
                currentBulletCount = 0;
            }
            currentBulletAmoText.text = "Снаряды: " + currentBulletCount.ToString();
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rb.position;
        position.x = position.x + 3.0f * horizontal * Time.deltaTime;
        position.y = position.y + 3.0f * vertical * Time.deltaTime;

        rb.MovePosition(position);
    }
}
