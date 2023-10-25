using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Animator pCamAnim;
    [SerializeField] Image damageIndicator;
    [SerializeField] EntityStatsScriptable pStats;
    Rigidbody rb;

    
    [SerializeField] float hp = 3;
    Color damageColor;

    public bool damaged = false;
    float healDelay = 5, healTimer = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 1;
        damageColor = damageIndicator.color;
    }

    private void Update()
    {
        if (damaged)
        {
            if (healTimer < healDelay)
            {
                healTimer += Time.deltaTime;
            }
            else
            {
                if (hp < pStats.hp)
                {
                    ReceiveDamage(-1);
                    damageIndicator.color = damageColor;
                }
                else
                {
                    damaged = false;
                }
                healTimer = 0;
            }
        }
    }

    public void ReceiveDamage(float damage)
    {
        //Debug.Log(hp + " " + damage);
        hp-=damage;
        damaged = true;
        healTimer = 0;
        if (hp <= 0)
        {
            PlayerDie();
            damageColor.a = 1;
            StartCoroutine(WaitForGameOver());
        }
        else
        {
            //Debug.Log(damageColor.a);
            damageColor.a = damage/pStats.hp;
        }
        damageIndicator.color = damageColor;
    }

    IEnumerator WaitForGameOver()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        rb.freezeRotation = true;
        yield return new WaitForSecondsRealtime(1f);
        Cursor.lockState = CursorLockMode.None;
        ScoreManager.scoreInstance.SetWin(false);
        MenuController.MenuCtrlInstance.EnableMenu(MenuType.GameOver);
    }

    void PlayerDie()
    {
        rb.freezeRotation = false;
        gameObject.layer = 0;

        MonoBehaviour[] playerScripts = gameObject.GetComponents<MonoBehaviour>();

        for (int i = 0; i < playerScripts.Length; i++)
        {
            playerScripts[i].enabled = false;
        }

        int aux = Random.Range(0, 2);
        Vector3 dieDir = Vector3.up * 10;
        if (aux == 0)
        {
            dieDir += Vector3.right * 2;
        }
        else
        {
            dieDir += Vector3.left * 2;
        }
        rb.AddForce(dieDir);
    }

}
