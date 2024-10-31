using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorSystem : MonoBehaviour
{
    public List<GameObject> colorburstTargets;

    [Header(" - Colorburst parameteres")]
    [SerializeField] private float cooldownTimer;
    [SerializeField] private bool canColorburst;
    public float maxCooldown = 5.0f;

    [Header(" - Active color")]
    public bool red;
    public bool green;
    public bool blue;

    [Header(" - Rotatation Values")]
    [SerializeField] int rotationController;
    [SerializeField] float rotationTarget;

    [Header(" - Cyan Control Variables")]
    public float cyanControlTimer;
    public bool isWithCyanIndicatorsOn;

    [Header(" - Refs")]
    public GameObject cyanColorburstAOEPrefab;

    [Header(" - DebugStuff")]
    [SerializeField] private PlayerCombatScript playerCombatScriptRef;
    [SerializeField] private PlayerCharacterControlerMovement playerMovementScriptRef;
    [SerializeField] private NewPlayerAnimationScript playerAnimationScriptRef;
    [SerializeField] private BrushTipScript BrushTipScriptRef;
    [SerializeField] private GameObject PlayerRef;
    [SerializeField] private SpriteRenderer greenColorburstVFXSpriteRef;
    [SerializeField] private PlayerUIColorBurstCooldownScripts playerUIColorBurstCooldownScriptRef;
    [SerializeField] private Image playerUIColorBurstCooldownImage1;
    [SerializeField] private Image playerUIColorBurstCooldownImage2;
    [SerializeField] private RectTransform UIColorwheelRef;
    public PlayerHealthBarScript playerHealthBarScriptRef;

    private void Start()
    {
        playerCombatScriptRef = gameObject.GetComponent<PlayerCombatScript>();
        playerMovementScriptRef = gameObject.GetComponent<PlayerCharacterControlerMovement>();
        playerAnimationScriptRef = gameObject.transform.GetChild(2).GetComponent<NewPlayerAnimationScript>();
        greenColorburstVFXSpriteRef = gameObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        playerUIColorBurstCooldownScriptRef = playerCombatScriptRef.PlayerUIRef.transform.GetChild(0).GetChild(0).GetComponent<PlayerUIColorBurstCooldownScripts>();
        playerUIColorBurstCooldownImage1 = playerCombatScriptRef.PlayerUIRef.transform.GetChild(0).GetComponent<Image>();
        playerUIColorBurstCooldownImage2 = playerCombatScriptRef.PlayerUIRef.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        UIColorwheelRef = playerCombatScriptRef.PlayerUIRef.transform.GetChild(1).GetComponent<RectTransform>();
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        BrushTipScriptRef = PlayerRef.transform.GetChild(3).GetComponent<BrushTipScript>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3") && Time.timeScale != 0.0f)
        {
            SwitchColor();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1) && Time.timeScale != 0.0f)
        {
            SwitchColorWithAlphaNumbers(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && Time.timeScale != 0.0f)
        {
            SwitchColorWithAlphaNumbers(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && Time.timeScale != 0.0f)
        {
            SwitchColorWithAlphaNumbers(3);
        }
        if (Input.GetButtonDown("Fire2") || InputUtility.LTriggerPulled)
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

        if (isWithCyanIndicatorsOn)
        {
            if(cyanControlTimer > 0.0f)
            {
                cyanControlTimer -= Time.deltaTime;
            }
            if(cyanControlTimer <= 0)
            {
                cyanControlTimer = 0.0f;
                isWithCyanIndicatorsOn = false;
                playerHealthBarScriptRef.TurnOffIndicator("Cyan");
                playerHealthBarScriptRef.ChangeHealthBarColor(Color.white);
            }
        }
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
    private void SwitchColorWithAlphaNumbers(int input)
    {
        if (input == 1)
        {
            red = true;
            green = false;
            blue = false;
            rotationTarget = 120f;
        }
        else if (input == 2)
        {
            green = true;
            blue = false;
            red = false;
            rotationTarget = 240f;
        }
        else if (input == 3)
        {
            blue = true;
            red = false;
            green = false;
            rotationTarget = 0f;
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
            //.SetTriggerInBrushTip("ColorBurst");
            yield return new WaitForSeconds(0.35f);

            foreach (GameObject enemy in colorburstTargets)
            {
                if(enemy != null)
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
