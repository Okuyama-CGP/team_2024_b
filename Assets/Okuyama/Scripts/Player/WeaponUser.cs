using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUser : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weaponPrefab; //TODO:暫定措置

    PlayerCore playerCore;
    GameObject weaponInstance;
    IUseable holdingUseable;


    void Start()
    {
        playerCore = MainGameManager.instance.playerCore;

        weaponInstance = Instantiate(weaponPrefab, weaponHolder.transform);
        holdingUseable = weaponInstance.GetComponent<IUseable>();
    }

    
    void Update()
    {
        playerCore.attackTrigger = false;
        if(Input.GetMouseButtonDown(0)){
            bool isUsed = holdingUseable.TryUse(playerCore);
            playerCore.attackTrigger = isUsed;
        }
        //TODO: 長押し使用どうしよう
    }
}
