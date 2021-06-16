using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    public LayerMask movementMask;
    public Interactable focus;
    public Transform target;
    [HideInInspector] public bool playerControllerActive = false;

    Camera cam;
    PlayerMotor motor;
    PlayerAnimator animator;
    PlayerCombat playerCombat;
    InventoryUI inventory;
    EquipmentInventoryUI equipmentInventory;
    FindClosestEnemy findEnemy;
    RespawnManager respawnManager;
    NavMeshAgent agent;

    private GameObject inventoryCanvas;
    private CurrentEquipmentSlot equipmentSlot1;
    private CurrentEquipmentSlot equipmentSlot2;
    private CurrentEquipmentSlot equipmentSlot3;
    private CurrentEquipmentSlot equipmentSlot4;

    public float[] abilityCooldown = new float[4] { 5, 5, 5, 5 };
    private float[] abilityTimer = new float[4] { 0, 0, 0, 0 };
    public List<CooldownBar> cdBar = new List<CooldownBar>(4);

    public GameObject clickVFX;

    private void Awake()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        animator = GetComponent<PlayerAnimator>();
        playerCombat = GetComponent<PlayerCombat>();
        findEnemy = GetComponent<FindClosestEnemy>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Start()
    {
        respawnManager = RespawnManager.instance;

        inventory = InventoryUserInterface.instance.inventory;
        equipmentInventory = InventoryUserInterface.instance.equipmentInventory;
        inventoryCanvas = InventoryUserInterface.instance.inventoryCanvas;

        equipmentSlot1 = InventoryUserInterface.instance.equipmentSlot1;
        equipmentSlot2 = InventoryUserInterface.instance.equipmentSlot2;
        equipmentSlot3 = InventoryUserInterface.instance.equipmentSlot3;
        equipmentSlot4 = InventoryUserInterface.instance.equipmentSlot4;


        for (int i = 0; i < 4; i++)
        {
            abilityTimer[i] = abilityCooldown[i];
        }


        for (int l = 0; l < 4; l++)
        {
            cdBar[l].SetMaxCooldown(abilityCooldown[l]);
        }


        if (RespawnManager.instance.lastRespawnPoint != Vector3.zero)
        {
            agent.Warp(RespawnManager.instance.lastRespawnPoint);
        }
    }


    void Update()
    {
        abilityTimer[0] += Time.deltaTime;
        cdBar[0].SetCooldown(abilityTimer[0]);

        abilityTimer[1] += Time.deltaTime;
        cdBar[1].SetCooldown(abilityTimer[1]);

        abilityTimer[2] += Time.deltaTime;
        cdBar[2].SetCooldown(abilityTimer[2]);

        abilityTimer[3] += Time.deltaTime;
        cdBar[3].SetCooldown(abilityTimer[3]);


        if (Input.GetButtonDown("Inventory"))
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
            inventory.UpdateUI();
            equipmentInventory.UpdateUI();
        }

        //disable player input during ui e.g. inventory
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(1)) //1 = right mouse
        {
            Movement();
            InvokeRepeating(nameof(Movement), 0.8f, 0.55f);
        }
        if (Input.GetMouseButtonUp(1))
        {
            CancelInvoke(nameof(Movement));
        }

        if (Input.GetMouseButtonDown(0)) //0 = left mouse 
        {
            AttackInteractionMovement();
        }

        //A
        if (Input.GetButtonDown("AttackClick"))
        {
            AttackClick();
        }

        if (Input.GetButtonDown("Ability1"))
        {
            AbilityQ();
        }

        if (Input.GetButtonDown("Ability2"))
        {
            AbilityW();
        }

        if (Input.GetButtonDown("Ability3"))
        {
            AbilityE();
        }

        if (Input.GetButtonDown("Ability4"))
        {
            AbilityR();
        }

        if (Input.GetButtonDown("StopMoving"))
        {
            playerControllerActive = true;
            motor.StopMoving();
            animator.cancelAttack();
        }
    }

    #region INPUT
    public void Movement()
    {
        playerControllerActive = true;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, movementMask))
        {
            motor.MoveToPoint(hit.point);
            RemoveFocus();

            //needs a better solution
            Vector3 clickSpawn = new Vector3(0f, 0.25f, 0f);
            clickSpawn = clickSpawn + hit.point;

            Instantiate(clickVFX, clickSpawn, transform.rotation);
            animator.cancelAttack();
        }
    }

    public void AttackInteractionMovement()
    {
        playerControllerActive = true;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            target = hit.collider.transform;

            if (interactable != null)
            {
                SetFocus(interactable);
            }
            else if (target.tag == "Enemy")
            {
                motor.StopMoving();
                playerCombat.Attack(target);
                motor.SetTarget(target);
            }
            else
            {
                motor.MoveToPoint(hit.point);
                motor.StopFollowingTarget();
                animator.cancelAttack();
            }
        }
    }

    public void AttackClick()
    {
        playerControllerActive = true;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            target = hit.collider.transform;

            if (target.tag == "Enemy")
            {
                motor.StopMoving();
                playerCombat.Attack(target);
                motor.SetTarget(target);
            }

            else
            {
                findEnemy.FindClosestEnemyToPlayer(hit.point);
                if (findEnemy.targetEnemy == null)
                {
                    motor.MoveToPoint(hit.point);
                    return;
                }

                target = findEnemy.targetEnemy;
                motor.StopMoving();
                playerCombat.Attack(target);
                motor.SetTarget(target);
            }
        }
    }

    public void AbilityQ()
    {
        CurrentEquipmentSlot slot = equipmentSlot1;
        if (slot.CheckSlotForItem() == false) return;
        if (abilityTimer[0] >= abilityCooldown[0])
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 hitPoint = hit.point;
                slot.UseItem(hitPoint);
            }
            abilityTimer[0] = 0;
        }
    }

    public void AbilityW()
    {
        CurrentEquipmentSlot slot = equipmentSlot2;
        if (slot.CheckSlotForItem() == false) return;
        if (abilityTimer[1] >= abilityCooldown[1])
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 hitPoint = hit.point;
                slot.UseItem(hitPoint);
            }

            abilityTimer[1] = 0;
        }
    }

    public void AbilityE()
    {
        CurrentEquipmentSlot slot = equipmentSlot3;
        if (slot.CheckSlotForItem() == false) return;
        if (abilityTimer[2] >= abilityCooldown[2])
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 hitPoint = hit.point;
                slot.UseItem(hitPoint);
            }

            abilityTimer[2] = 0;
        }
    }

    public void AbilityR()
    {
        CurrentEquipmentSlot slot = equipmentSlot4;
        if (slot.CheckSlotForItem() == false) return;
        if (abilityTimer[3] >= abilityCooldown[2])
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 hitPoint = hit.point;
                slot.UseItem(hitPoint);
            }

            abilityTimer[3] = 0;
        }
    }

    #endregion

    #region Skills

    public void StartTurningVolley()
    {
        StartCoroutine(SetIsTurningFalse());
    }

    IEnumerator SetIsTurningFalse()
    {
        yield return new WaitForSeconds(0.19f);
        motor.isTurning = false;
    }



    #endregion


    #region Focus

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDeFocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDeFocused();

        focus = null;
        motor.StopFollowingTarget();
    }

    #endregion
}
