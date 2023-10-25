using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Animator OnOffAnim;

    bool powerOn;

    [SerializeField] Slider energyBar;
    readonly float maxEnergy = 200, energyUsage = 3;
    float actualEnergy;

    [SerializeField] AudioSource aud;

    private void Awake()
    {
        energyBar.value = actualEnergy = energyBar.maxValue = maxEnergy;
    }

    private void OnEnable()
    {
        inventory.UseItemReleased += Use;
        inventory.ReloadItemReleased += ChargeBattery;
    }

    private void Update()
    {
        if (powerOn)
        {
            actualEnergy -= energyUsage * Time.deltaTime;
            energyBar.value = actualEnergy;
            if (actualEnergy <= 0)
            {
                powerOn = false;
                OnOffAnim.SetBool("On", powerOn);
                OnOffAnim.SetBool("Power", powerOn);
                inventory.UseItemReleased -= Use;
            }
        }
    }

    private void OnDisable()
    {
        inventory.UseItemReleased -= Use;
        inventory.ReloadItemReleased -= ChargeBattery;
    }

    public void Use()
    {
        powerOn = !powerOn;
        OnOffAnim.SetBool("On", powerOn);
        aud.Play();
    }

    public void ChargeBattery()
    {
        if (actualEnergy < maxEnergy)
        {
            if (OnOffAnim.GetBool("Power"))
            {
                actualEnergy += energyUsage * Time.deltaTime * 2;
            }
            else
            {
                actualEnergy += energyUsage * Time.deltaTime;
            }

            energyBar.value = actualEnergy;
        }
        else
        {
            if (!OnOffAnim.GetBool("Power"))
            {
                Debug.Log("hi");
                inventory.UseItemReleased += Use;
                OnOffAnim.SetBool("Power", true);
            }
        }
    }
}
