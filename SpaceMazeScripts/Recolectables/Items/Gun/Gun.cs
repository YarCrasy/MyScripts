using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Transform gunHole;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Image ammoDisplay;

    public const int MAX_AMMO = 4;

    int ammo = MAX_AMMO;

    const float MAX_TIMER = 1;
    float reloadTimer = 0;

    private void OnEnable()
    {
        inventory.UseItemReleased += Shoot;
        inventory.ReloadItemReleased += Reload;
    }

    private void OnDisable()
    {
        inventory.UseItemReleased -= Shoot;
        inventory.ReloadItemReleased -= Reload;
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            GameObject bullet = Instantiate(bulletPrefab, gunHole.position, transform.parent.parent.rotation);
            ammo--;
            ammoDisplay.fillAmount = (float)ammo / MAX_AMMO;
        }
    }

    bool CanShoot()
    {
        if (!MenuController.MenuCtrlInstance.IsMenuEnabled() && ammo > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Reload()
    {
        if (ammo < MAX_AMMO)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= MAX_TIMER)
            {
                ammo++;
                ammoDisplay.fillAmount = (float)ammo / MAX_AMMO;
                reloadTimer = 0;
            }
        }
    }

}
