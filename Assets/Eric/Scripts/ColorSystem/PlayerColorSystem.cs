using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorSystem : MonoBehaviour
{
    public List<GameObject> colorburstTargets;

    [Header(" - Colorburst parameteres")]
    [SerializeField] private float maxCooldown = 5.0f;
    [SerializeField] private float cooldownTimer;
    [SerializeField] private bool canColorburst;

    [Header(" - Active color")]
    public bool red;
    public bool green;
    public bool blue;

    [Header(" - References")]
    [SerializeField] private RectTransform UIColorwheelRef;

    [Header(" - Rotatation Values")]
    [SerializeField] int rotationController;
    [SerializeField] float rotationTarget;

    [Header(" - DebugStuff")]
    [SerializeField] private PlayerCombatScript playerCombatScriptRef;
    [SerializeField] private PlayerCharacterControlerMovement playerMovementScriptRef;
    [SerializeField] private NewPlayerAnimationScript playerAnimationScriptRef;
    [SerializeField] private SpriteRenderer greenColorburstVFXSpriteRef;
    [SerializeField] private PlayerUIColorBurstCooldownScripts playerUIColorBurstCooldownScriptRef;
    [SerializeField] private Image playerUIColorBurstCooldownImage1;
    [SerializeField] private Image playerUIColorBurstCooldownImage2;

    private void Start()
    {
        playerCombatScriptRef = gameObject.GetComponent<PlayerCombatScript>();
        playerMovementScriptRef = gameObject.GetComponent<PlayerCharacterControlerMovement>();
        playerAnimationScriptRef = gameObject.transform.GetChild(0).GetComponent<NewPlayerAnimationScript>();
        greenColorburstVFXSpriteRef = gameObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        playerUIColorBurstCooldownScriptRef = playerCombatScriptRef.PlayerUIRef.transform.GetChild(0).GetChild(0).GetComponent<PlayerUIColorBurstCooldownScripts>();
        playerUIColorBurstCooldownImage1 = playerCombatScriptRef.PlayerUIRef.transform.GetChild(0).GetComponent<Image>();
        playerUIColorBurstCooldownImage2 = playerCombatScriptRef.PlayerUIRef.transform.GetChild(0).GetChild(0).GetComponent<Image>();

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            SwitchColor();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(Colorburst());
        }

        if (cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else if (cooldownTimer < 0.0f)
        {
            cooldownTimer = 0.0f;
        }
        else
        {
            canColorburst = true;
            playerUIColorBurstCooldownImage1.enabled = false;
            playerUIColorBurstCooldownImage2.enabled = false;
        }

        playerUIColorBurstCooldownScriptRef.SetCooldownDisplay(cooldownTimer / maxCooldown);
    }


    private void SwitchColor()
    {
        if (red)
        {
            red = false;
            green = true;
            rotationTarget = 240f;
        }
        else if (green)
        {
            green = false;
            blue = true;
            rotationTarget = 0f;
        }
        else if (blue)
        {
            blue = false;
            red = true;
            rotationTarget = 120f;
        }
        else
        {
            red = true;
            rotationTarget = 120f;
        }
        StartRotationCoroutine();
    }

    private void StartRotationCoroutine()
    {
        float i = 0;
        StartCoroutine(RotationCoroutine(i));
    }

    private IEnumerator RotationCoroutine(float i)
    {
        i += 0.1f;

        if (rotationTarget == 0.0f)
        {
            UIColorwheelRef.Rotate(0, 0, Mathf.Lerp(UIColorwheelRef.rotation.eulerAngles.z, 360f, i) - UIColorwheelRef.rotation.eulerAngles.z);
        }
        else
        {
            UIColorwheelRef.Rotate(0, 0, Mathf.Lerp(UIColorwheelRef.rotation.eulerAngles.z, rotationTarget, i) - UIColorwheelRef.rotation.eulerAngles.z);
        }



        yield return new WaitForSeconds(0.01f);
        if (i < 1)
        {
            StartCoroutine(RotationCoroutine(i));   
        }
    }





    private IEnumerator Colorburst()
    {
        if (canColorburst && colorburstTargets.Count > 0)
        {
            playerMovementScriptRef.canMove = false;
            cooldownTimer = maxCooldown;
            canColorburst = false;
            playerUIColorBurstCooldownImage1.enabled = true;
            playerUIColorBurstCooldownImage2.enabled = true;
            playerAnimationScriptRef.SetTriggerInPlayerAnimator("ColorBurst");
            yield return new WaitForSeconds(0.35f);

            foreach (GameObject enemy in colorburstTargets)
            {
                EnemyColorSystem enemyColorSystemRef = enemy.GetComponent<EnemyColorSystem>();

                enemyColorSystemRef.OnColorburst();
                enemy.GetComponent<EnemyCombatScript>().StaggerEnemy(1.0f);

                playerCombatScriptRef.HealPlayer(enemyColorSystemRef.greenStacks * enemyColorSystemRef.greenHealingBurst);
                if (!greenColorburstVFXSpriteRef.enabled && enemyColorSystemRef.greenStacks > 0)
                {
                    StartCoroutine(GreenColorburstVFX());
                }

                enemyColorSystemRef.ResetStacks();
            }

            colorburstTargets.Clear();

            yield return new WaitForSeconds(0.35f);

            playerMovementScriptRef.canMove = true;
        }
    }

    private IEnumerator GreenColorburstVFX()
    {
        greenColorburstVFXSpriteRef.enabled = true;
        yield return new WaitForSeconds(0.75f);
        greenColorburstVFXSpriteRef.enabled = false;
    }
}
