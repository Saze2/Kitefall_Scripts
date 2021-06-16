using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }       
    }
    #endregion

    public GameObject player;
    public PlayerCombat combat;
    public PlayerStats stats;
    public PlayerController controller;
    public PlayerMotor motor;
    public PlayerAnimator animator;
    public Image[] secondaryIcon = new Image[5];

    public GameObject deathUI;

    public Vector3 lastCheckPoint = Vector3.zero;
    public Transform checkPoint;

    /*
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        combat = player.GetComponent<PlayerCombat>();
        controller = player.GetComponent<PlayerController>();
        motor = GetComponent<PlayerMotor>();
        stats = GetComponent<PlayerStats>();
        animator = GetComponent<PlayerAnimator>();
    }
    */

    public void KillPlayer()
    {
        motor.SpeedZero();
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        deathUI.SetActive(true);

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);     
    }

}
