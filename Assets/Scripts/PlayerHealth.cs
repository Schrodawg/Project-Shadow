using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public float health, maxHealth;
    public float spikeCost;
    public float shadowCost;
    public float chargeRate;
    public float chargeTime = 1f;
    private Coroutine recharge;

    public float transitionTime = 0.5f;

    private void Update()
    {
        if (health == 0)
        {
            StartCoroutine(LoadLevel());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(LoadLevel());
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Damage(bool spike, bool shadow)
    {
        if (spike)
        {
            health -= spikeCost;
            if (health < 0) health = 0;
            healthBar.fillAmount = health / maxHealth;
            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeHealth());
        }

        if (shadow)
        {
            health -= shadowCost * Time.deltaTime;
            if (health < 0) health = 0;
            healthBar.fillAmount = health / maxHealth;
            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeHealth());
        }
    }

    private IEnumerator RechargeHealth()
    {
        yield return new WaitForSeconds(chargeTime);

        while (health < maxHealth)
        {
            health += chargeRate / 10f;
            if (health > maxHealth) health = maxHealth;
            healthBar.fillAmount = health / maxHealth;
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(transitionTime);

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
