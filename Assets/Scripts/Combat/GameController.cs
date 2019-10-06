using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum WaitMode { None, WaitForFinishTool, WaitForFinishPlayerAttack, WaitForFinishWithdrawingTool, WaitForEnemyAttack, WaitForFinishScan }

public class GameController : MonoBehaviour
{
    public static GameController Game;
    public BattleStats Player;
    public BattleStats Enemy;
    public BattleStats HumanEnemy;
    public GameObject Boom;
    public float DeathDelay = 1;
    [HideInInspector]
    public List<Attack> PlayerAttacks;
    [HideInInspector]
    public List<Attack> EnemyAttacks;
    public List<Attack> PlayerAvailableAttacks
    {
        get
        {
            return PlayerAttacks.FindAll(a => a.EnergyCost <= Player.Energy);
        }
    }
    public List<Attack> EnemyAvailableAttacks
    {
        get
        {
            return EnemyAttacks.FindAll(a => a.EnergyCost <= Enemy.Energy);
        }
    }
    [HideInInspector]
    public Attack PlayerAttack;
    [HideInInspector]
    public Attack EnemyAttack;
    [HideInInspector]
    public WaitMode TheWaitMode = WaitMode.None;
    public bool IsHuman;
    private float count = Mathf.NegativeInfinity;
    private void Awake()
    {
        Game = this;
        //Check if enemy is human
        if (PlayerPrefs.GetInt("IsHuman", 1) == 1)
        {
            Enemy.gameObject.SetActive(false);
            HumanEnemy.FromString(PlayerPrefs.GetString("EnemyStats"));
            Enemy = HumanEnemy;
            IsHuman = true;
            PlayerPrefs.SetInt("Music", 3);
        }
        else
        {
            HumanEnemy.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Music", 1);
        }
        //Load enemy and player from prefs
        Enemy.FromString(PlayerPrefs.GetString("EnemyStats"));
        if (!PlayerPrefs.HasKey("PlayerStats"))
        {
            //Init stats
            Player.Name = "M43 (you)";
            Player.MaxHealth = 10;
            Player.MaxEnergy = 10;
            Player.Power = 2;
            Player.Defense = 2;
            PlayerPrefs.SetString("PlayerStats", Player.ToString());
        }
        Player.FromString(PlayerPrefs.GetString("PlayerStats"));
    }
    private void Start()
    {
        //TODO: Load enemy and player attacks
        PlayerAttacks = AllAttacks.Instance.Attacks.FindAll(a => Player.Attacks.Contains(a.Name));
        EnemyAttacks = AllAttacks.Instance.Attacks.FindAll(a => Enemy.Attacks.Contains(a.Name));
        EnemyAttack = null;
    }
    private void Update()
    {
        switch (TheWaitMode)
        {
            case WaitMode.None:
                if (count != Mathf.NegativeInfinity)
                {
                    count -= Time.deltaTime;
                    if (count <= 0)
                    {
                        SceneManager.LoadScene("Overworld");
                    }
                }
                break;
            case WaitMode.WaitForFinishTool:
                if (!PlayerAttack.ToolAnimation.Active)
                {
                    PlayerAttack.Launch(Player, Enemy);
                    TheWaitMode = WaitMode.WaitForFinishPlayerAttack;
                }
                break;
            case WaitMode.WaitForFinishPlayerAttack:
                if (!PlayerAttack.AttackAnimation.Active)
                {
                    PlayerAttack.ToolAnimation.Reverse();
                    PlayerAttack.ToolAnimation.StartAnimations();
                    TheWaitMode = WaitMode.WaitForFinishWithdrawingTool;
                }
                break;
            case WaitMode.WaitForFinishWithdrawingTool:
                if (!PlayerAttack.ToolAnimation.Active)
                {
                    EndPlayerTurn();
                }
                break;
            case WaitMode.WaitForEnemyAttack:
                if ((!IsHuman && !EnemyAttack.AttackAnimation.Active) || (IsHuman && !EnemyAttack.HumanAnimation.Active))
                {
                    EndEnemyTurn();
                }
                break;
            case WaitMode.WaitForFinishScan:
                if (!Player.ScanAnimation.Active)
                {
                    foreach (AttackButton item in InteractableUI.Instance.AttackUI.GetComponentsInChildren<AttackButton>())
                    {
                        Destroy(item.gameObject);
                    }
                    DisplayAttacks.Instance.Display();
                    EndPlayerTurn();
                }
                break;
            default:
                break;
        }
    }
    public void StartPlayerTurn()
    {
        InteractableUI.Instance.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
    public void EndPlayerTurn()
    {
        if (TheWaitMode == WaitMode.WaitForFinishWithdrawingTool)
        { 
            PlayerAttack.ToolAnimation.Reverse();
        }
        Player.IdleAnimation.StartAnimations(true);
        //Enemy do stuff
        GetEnemyAttack();
        if (EnemyAttack != null)
        {
            EnemyAttack.Launch(Enemy, Player);
            TheWaitMode = WaitMode.WaitForEnemyAttack;
        }
        else
        {
            Enemy.Energy += 5;
            Game.Enemy.Missed.Show("Waited");
            EndEnemyTurn();
        }
    }
    private void EndEnemyTurn()
    {
        Enemy.IdleAnimation.StartAnimations(true);
        InteractableUI.Instance.GetComponent<RectTransform>().localScale = Vector3.one;
        TheWaitMode = WaitMode.None;
        InteractableUI.Instance.EnableButtons();
    }
    private void GetEnemyAttack()
    {
        //A very complicated and smart enemy AI
        if (EnemyAvailableAttacks.Count > 0)
        {
            //EnemyAttack = EnemyAvailableAttacks[Random.Range(0, Enemy.Attacks.Count)];
            List<Attack> attacks = EnemyAvailableAttacks;
            attacks.Sort(delegate (Attack x, Attack y)
            {
                return (int)Mathf.Sign(y.EnergyCost - x.EnergyCost);
            });
            Debug.Log(attacks[0].Name);
            List<Attack> bestAttacks = attacks.FindAll(a => a.EnergyCost >= attacks[0].EnergyCost);
            EnemyAttack = bestAttacks[Random.Range(0, bestAttacks.Count)];
        }
        else
        {
            EnemyAttack = null;
        }
    }
    public void Explode(BattleStats target)
    {
        if (target == Player)
        {
            PlayerPrefs.SetFloat("PlayerXPos", -4);
            PlayerPrefs.SetFloat("PlayerZPos", 4);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("EnemyID"), 1);
            PlayerPrefs.SetInt("Kills", PlayerPrefs.GetInt("Kills", 0) + 1);
            if (PlayerPrefs.GetInt("Kills") >= 3)
            {
                PlayerPrefs.SetInt("Kills", 0);
                Player.Missed.Show("Level up");
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 0) + 1);
                Player.MaxHealth += 2;
                Player.MaxEnergy += 2;
                Player.Power += 1;
                Player.Defense += 1;
            }
            PlayerPrefs.SetString("PlayerStats", Player.ToString());
        }
        Instantiate(Boom, target.transform.position, Quaternion.identity).SetActive(true);
        Destroy(target.gameObject);
        count = DeathDelay;
    }
}
