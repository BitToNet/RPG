using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;
 
 
    GameObject currentWeaponInHand;
    GameObject currentWeaponInSheath;
    private Character character;

    void Start()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        character = transform.GetComponent<Character>();
    }

    public void DrawWeapon()
    {
        Debug.Log("DrawWeapon");
        StartCoroutine(ChangeWeaponToHand());
        character.isCombatStateChanging = true;
    }
    
    public void EndDrawWeapon()
    {
        character.isCombatStateChanging = false;
    }

    IEnumerator ChangeWeaponToHand()
    {
        yield return new WaitForSeconds(0.5f);
        currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
        Destroy(currentWeaponInSheath);
    }

    public void SheathWeapon()
    {
        Debug.Log("SheathWeapon");
        StartCoroutine(ChangeWeaponToWaist());
        character.isCombatStateChanging = true;
    }
    
    public void EndSheathWeapon()
    {
        character.isCombatStateChanging = false;
    }

    IEnumerator ChangeWeaponToWaist()
    {
        yield return new WaitForSeconds(1.26f);
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        Destroy(currentWeaponInHand);
    }

    public void StartDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }
}