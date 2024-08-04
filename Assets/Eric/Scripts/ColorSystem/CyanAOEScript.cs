using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyanAOEScript : MonoBehaviour
{

    public float cyanHealthRegen;
    public float aoeLifespan;
    [SerializeField] private GameObject playerRef;
    [SerializeField] private PlayerCombatScript playerCombatScriptRef;
    [SerializeField] private PlayerHealthBarScript playerHealthBarScriptRef;
    [SerializeField] private PlayerColorSystem playerColorSystemRef;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerRef = other.gameObject;
            playerCombatScriptRef = playerRef.GetComponent<PlayerCombatScript>();
            playerHealthBarScriptRef = playerCombatScriptRef.PlayerHealthBarScriptRef;
            playerColorSystemRef = playerRef.GetComponent<PlayerColorSystem>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCombatScriptRef.HealPlayer(cyanHealthRegen * Time.deltaTime);
            playerHealthBarScriptRef.ChangeHealthBarColor(Color.cyan);
            playerHealthBarScriptRef.TurnOnIndicator("cyan");
            playerColorSystemRef.cyanControlTimer = 0.1f;
            playerColorSystemRef.isWithCyanIndicatorsOn = true;
        }
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyCombatScript>().HealthBarScriptRef.TurnOnEffectIndicator("cyan");
            other.gameObject.GetComponent<EnemyColorSystem>().isSlowedByCyan = true;
            other.gameObject.GetComponent<EnemyColorSystem>().cyanControlTimer = 0.1f;
            other.gameObject.GetComponent<EnemyColorSystem>().isWithCyanIndicatorsOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealthBarScriptRef.ChangeHealthBarColor(Color.white);
            playerHealthBarScriptRef.TurnOffIndicator("Cyan");
            playerColorSystemRef.isWithCyanIndicatorsOn = false;
            playerColorSystemRef.cyanControlTimer = 0f;
        }
        else if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyCombatScript>().HealthBarScriptRef.TurnOffEffectIndicator("cyan");
            other.gameObject.GetComponent<EnemyColorSystem>().isSlowedByCyan = true;
            other.gameObject.GetComponent<EnemyColorSystem>().cyanControlTimer = 0f;
            other.gameObject.GetComponent<EnemyColorSystem>().isWithCyanIndicatorsOn = false;
        }
    }

    public void StartLifespanDecay()
    {

        Destroy(gameObject.transform.parent.gameObject, aoeLifespan);
    }
}
