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
                if (PreviousState == m_BossState)
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
                StateChangeCallback();

                timeElapsed = 0;
            }
        }
    }

    private void StateChangeCallback()
    {
        switch (m_BossState)
        {
            case boss_state.idle:
                break;
            case boss_state.ShootOrbs:
                BossAnimator.Play("BossFireProjectile");
                break;
            case boss_state.MoveToDifferentSideOfScreen:
                nextPositionString = (nextPositionString == "Left") ? "Right" : "Left";
                PrepareMove();
                break;
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
    [Header("Shooting Projectile")]
    public GameObject projectile;
    private Transform playerTransform;
    public ParticleSystem ProjectileParticle;
    public float TimeToShoot = 2;
    [Header("Boss Movement")]
    public List<BossPositions> bossPositions = new List<BossPositions>();
    private Dictionary<string, Transform> bossPositionDictionary = new Dictionary<string, Transform>();
    public Transform Parent;
    private Vector2 initialBossPosition;
    private Vector2 nextBossPosition;
    private float timeElapsed;
    public float TimeToMoveSides = 1f;

    private string nextPositionString;
    private string nextAnimationToPlay;
    public BoxCollider2D TileRemoverCollider;
    private bool StateComplete;
    
    void Awake()
    {
        playerTransform = FindObjectOfType<CharacterMovement>().transform;
        StartCoroutine(BossStateCoroutine());
        BossSpriteRenderer = GetComponent<SpriteRenderer>();
        BossAnimator = GetComponent<Animator>();
        foreach(BossPositions pos in bossPositions)
        {
            bossPositionDictionary.Add(pos.BossPositionName, pos.BossPosition);
        }
    }

    private void Update()
    {
        if(playerTransform.position.x > Parent.position.x)
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
                timeElapsed += Time.deltaTime;  
                ShootProjectile();
                break;
            case boss_state.MoveToDifferentSideOfScreen:
                timeElapsed += Time.deltaTime;
                MoveToOppositeSide();
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

    public void PrepareToShootProjectile()
    {
        ProjectileParticle.Play();
    }

    private void ShootProjectile()
    {
        if(timeElapsed > TimeToShoot)
        {
            StateComplete = true;
            Projectile projPrefab = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
            projPrefab.MoveProjectile(playerTransform.position);
            nextAnimationToPlay = "FireBall";
            Invoke("StopProjectileEffect", 0.2f);
            Invoke("MoveToIdle", 1f);
        }
    }

    private void StopProjectileEffect()
    {
        ProjectileParticle.Stop();
    }

    private void PrepareMove()
    {
        BossAnimator.SetTrigger("InIdleState");
        timeElapsed = 0;
        initialBossPosition = Parent.position;
        nextBossPosition = bossPositionDictionary[nextPositionString].position;
    }

    private void MoveToOppositeSide()
    {
        var currentRatio = timeElapsed / TimeToMoveSides;
        Parent.position = Vector2.Lerp(initialBossPosition, nextBossPosition, currentRatio);
        if(currentRatio >= 1)
        {
            Parent.position = nextBossPosition;
            timeElapsed = 0;
            BossState = boss_state.idle;
        }
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

    private void MoveToIdle()
    {
        BossState = boss_state.idle;
    }
}

[Serializable]
public class BossPositions
{
    public string BossPositionName;
    public Transform BossPosition;
}