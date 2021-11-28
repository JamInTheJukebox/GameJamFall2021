using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_State_Machine : MonoBehaviour
{
    public enum boss_state
    {
        idle = 0,
        ShootOrbs = 1,
        MoveToDifferentSideOfScreen = 2,
        RemoveTerrain = 3,
        SwoopIn = 4,
        CaughtKitchi = 5,
    }
    private boss_state m_BossState;
    public boss_state BossState
    {
        get { return m_BossState; }
        set
        {
            if(value != m_BossState)
            {
                if(PreviousState == m_BossState)
                {
                    statesRepeated++;
                }
                else
                {
                    statesRepeated = 0;
                }
                PreviousState = m_BossState;
                m_BossState = value;
                StateComplete = false;

                switch (m_BossState)
                {
                    case boss_state.MoveToDifferentSideOfScreen:
                        nextPositionString = (nextPositionString == "Left") ? "Right" : "Left";
                        if (bossPositions.Contains(nextPositionString))
                        {
                            nextBossPosition = bossPositions[nextPositionString];
                        }
                        break;
                }
            }
        }
    }

    private int m_BossPhase = 1;
    public int BossPhase
    {
        get { return m_BossPhase; }
        set
        {
            if (value != m_BossPhase)
            {
                m_BossPhase = value;
            }
        }
    }

    private boss_state PreviousState;

    #region Components
    private SpriteRenderer BossSpriteRenderer;
    private Animator BossAnimator;
    #endregion

    [SerializeField] float MaxSecondsUntilNewState = 6f;
    [SerializeField] float MinSecondsUntilNewState = 3f;

    [SerializeField] float SecondsToDropKitchi;

    int statesRepeated;
    // Start is called before the first frame update
    public GameObject projectile;
    private Transform playerTransform;
    public List<BossPositions> bossPositions = new List<BossPositions>();
    private Dictionary<String, Transform> bossPositionDictionary = new Dictionary<string, Transform>();
    private Transform nextBossPosition;
    private String nextPositionString;

    private bool StateComplete;

    void Awake()
    {
        playerTransform = FindObjectOfType<CharacterMovement>().transform;
        StartCoroutine(BossStateCoroutine());
        BossSpriteRenderer = GetComponent<SpriteRenderer>();
        BossAnimator = GetComponent<Animator>();
        foreach(BossPositions pos in bossPositions)
        {

        }
    }

    private void Update()
    {
        if(playerTransform.position.x > transform.position.x)
        {
            BossSpriteRenderer.flipX = true;
        }
        else
        {
            BossSpriteRenderer.flipX = false;
        }
        if (!StateComplete)
        {
            BossBehavior();
        }
    }

    private void BossBehavior()
    {
        switch (BossState)
        {
            case boss_state.idle:
                Idle();
                break;
            case boss_state.ShootOrbs:
                ShootProjectile();
                break;
            case boss_state.MoveToDifferentSideOfScreen:

                break;
            case boss_state.RemoveTerrain:
                break;
            case boss_state.SwoopIn:
                break;
            case boss_state.CaughtKitchi:
                break;
        }
    }
    private IEnumerator BossStateCoroutine()
    {
        var waitTime = UnityEngine.Random.Range(MinSecondsUntilNewState, MaxSecondsUntilNewState);
        yield return new WaitForSeconds(MaxSecondsUntilNewState);
        BossState = ChooseNextState();
        print(BossState);
        StartCoroutine(BossStateCoroutine());
    }

    private boss_state ChooseNextState()
    {
        int MaxState = (BossPhase == 1) ? 3 : 6;
        boss_state newBossState = (boss_state)UnityEngine.Random.Range(0, MaxState);
        
        while((PreviousState == boss_state.RemoveTerrain && newBossState == boss_state.RemoveTerrain) || (statesRepeated > 3 && newBossState == BossState))
        {
            newBossState = (boss_state)UnityEngine.Random.Range(0, MaxState);
        }

        return newBossState;
    }

    private void Idle()
    {
        // play idle animation here.
        BossAnimator.Play("BossHover");
        StateComplete = true;
    }

    private void ShootProjectile()
    {

        Projectile projPrefab = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projPrefab.MoveProjectile(playerTransform.position);
        StateComplete = true;
    }

    private void MoveToOppositeSide()
    {

    }
    private void RemoveTerrain()
    {

    }

    private void SwoopIn()
    {

    }

    private void CaughtKitchi()
    {

    }
}

[Serializable]
public class BossPositions
{
    public string BossPositionName;
    public Transform BossPosition;
}