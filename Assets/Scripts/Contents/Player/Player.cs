using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Player : Mob_Base
{
    private static Player _instance = null;
    public static Player Instance => _instance;

    [SerializeField] private AudioClip moveSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private TextMeshProUGUI hpbar;

    public int HP;
    public List<Item_Base> statsItem = new();
    public List<Item_Base> attackItem = new();
    public List<Item_Base> moveItem = new();
    public int isKey;
    public bool isLobby;
    public bool isDead;
    public bool isEasy;

    TimingManager timingManager;

    public void Init()
    {
        _instance = this;
    }

    protected override void Start()
    {
        base.Start();
        isDead = false;
        timingManager = FindObjectOfType<TimingManager>();
    }

    public void Setting(int hp){
        isEasy = RoundData.Instance.isEasy;

        if(!isEasy) hpbar.text = HP.ToString();
        else hpbar.text = new("∞");
    }

    public void Damage()
    {
        if (!isEasy)
        {
            HP -= 1;
            hpbar.text = HP.ToString();
        }else return;

        if (HP <= 0)
        {
            HP = 0;
            DieDestroy();
        }
    }

    public void Move(string type)
    {
        if (isDead) return;

        switch (type)
        {
            case "Up": CheckMove(Vector3.forward); break;
            case "Down": CheckMove(Vector3.back); break;
            case "Right": CheckMove(Vector3.right); break;
            case "Left": CheckMove(Vector3.left); break;
        }
    }

    private void CheckMove(Vector3 movePos)
    {
        if (!timingManager.CheckTiming()) { Debug.Log("Miss"); return; } //멈추는 이펙트 추가하면 좋을 듯

        Vector2Int plusPos = Vector2Int.zero;

        if (movePos == Vector3.forward) plusPos = Vector2Int.up;
        if (movePos == Vector3.back) plusPos = Vector2Int.down;
        if (movePos == Vector3.right) plusPos = Vector2Int.right;
        if (movePos == Vector3.left) plusPos = Vector2Int.left;

        RookPlayer(movePos);

        int action = MoveManager.Instance.MoveCheck(curPos, plusPos, true);

        switch (action)
        {
            case 0: curPos = curPos + plusPos; StartCoroutine(MovePlayer(movePos, 0.2f)); break;
            case 1: break;
            case 2: Attack(); break;
        }
    }

    void RookPlayer(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), 1f);
    }

    void Attack()
    {
        Managers.Sound.Play(attackSound);
        anim.SetTrigger("doAttack");
    }

    private IEnumerator MovePlayer(Vector3 direction, float sec)
    {
        Managers.Sound.Play(moveSound);

        float elapsedTime = 0;
        Vector3 origPos = transform.position;
        Vector3 targetPos = origPos + direction;
        anim.SetTrigger("doDash");

        while (elapsedTime < sec)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / sec));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //move item trigger
        transform.position = targetPos;
    }

    protected override void DieDestroy()
    {
        Managers.Sound.Play(dieSound);
        anim.SetTrigger("doDeath");
        isDead = true;
        StartCoroutine(DieDelay());
    }

    private IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(2f);

        FadeLobby.Instance.fadeInGameAnim.SetTrigger("OnFade");
        Managers.Sound.Play(FadeLobby.Instance.fadeSound);

        yield return null;
    }
}
