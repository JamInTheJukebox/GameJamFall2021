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
            if (value != m_BossState)
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

            if (value == boss_state.idle || value == boss_state.CaughtKitchi)
            {
                StartCoroutine(BossStateCoroutine());
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
                currentParticleEffect = ProjectileParticle;
                BossAnimator.Play("BossFireProjectile");
                if(BossPhase == 1)
                {
                    currentOrbsToShoot = 1;
                }
                else
                {
                    currentOrbsToShoot = UnityEngine.Random.Range(2, 4);
                }
                break;
            case boss_state.MoveToDifferentSideOfScreen:
                nextPositionString = (nextPositionString == "Left") ? "Right" : "Left";
                PrepareMove();
                break;
            case boss_state.RemoveTerrain:
                RemoveTerrain();
                break;
            case boss_state.SwoopIn:
                StartSwoop();
                break;
            case boss_state.CaughtKitchi:
                bossManager.ChangeKitchiState(true);
                break;
        }
    }

    [SerializeField] private int m_BossPhase = 1;
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

    int StatesSinceOffence;
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
    private int currentOrbsToShoot;
    [Header("RemoveTiles")]
    public ParticleSystem RemoveTilesEffect;
    public TileRemover tileRemover;

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
    private bool StateComplete;
    ParticleSystem currentParticleEffect;

    // CaughtKitchi
    BossManager bossManager;

    void Awake()
    {
        playerTransform = FindObjectOfType<CharacterMovement>().transform;
        StartCoroutine(BossStateCoroutine());
        BossSpriteRenderer = GetComponent<SpriteRenderer>();
        BossAnimator = GetComponent<Animator>();
        bossManager = FindObjectOfType<BossManager>();
        foreach (BossPositions pos in bossPositions)
        {
            bossPositionDictionary.Add(pos.BossPositionName, pos.BossPosition);
        }
    }

    private void Update()
    {
        /*
        if (playerTransform.position.x > transform.position.x)
        {
            BossSpriteRenderer.flipX = true;
        }
        else
        {
            BossSpriteRenderer.flipX = false;
        }*/
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
    }

    private boss_state ChooseNextState()
    {
        int MaxState = (BossPhase == 1) ? 3 : 6;
        boss_state newBossState = (boss_state)UnityEngine.Random.Range(0, MaxState);

        if (newBossState == boss_state.idle || newBossState == boss_state.MoveToDifferentSideOfScreen)
        {
            StatesSinceOffence++;
        }

        while((PreviousState == boss_state.RemoveTerrain && newBossState == boss_state.RemoveTerrain) || (statesRepeated > 3 && newBossState == BossState) || StatesSinceOffence > 3)
        {
            newBossState = (boss_state)UnityEngine.Random.Range(0, MaxState);

            if (newBossState == boss_state.idle || newBossState == boss_state.MoveToDifferentSideOfScreen)
            {
                StatesSinceOffence++;
            }
            else { StatesSinceOffence = 0; }
        }
        return newBossState;
    }
    #region Idle and Shooting
    private void Idle()
    {
        // play idle animation here.
        BossAnimator.Play("BossHover");
        StateComplete = true;
    }

    public void PrepareToShootProjectile()
    {
        currentParticleEffect.Play();
    }

    private void ShootProjectile()
    {
        if (timeElapsed > TimeToShoot)
        {
            currentOrbsToShoot -= 1;
            Projectile projPrefab = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
            projPrefab.MoveProjectile(playerTransform.position);
            if (currentOrbsToShoot > 0)
            {
                timeElapsed -= 0.4f;
                return;
            }
            StateComplete = true;
            nextAnimationToPlay = "FireBall";
            Invoke("StopProjectileEffect", 0.2f);
            Invoke("MoveToIdle", 1f);
        }
    }

    private void StopProjectileEffect()
    {
        currentParticleEffect?.Stop();
    }
    #endregion

    #region Move Boss  
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
    #endregion

    #region Remove Terrain  

    private void RemoveTerrain()
    {
        Vector2 L = bossPositionDictionary["Left"].position;
        Vector2 R = bossPositionDictionary["Right"].position;
        nextBossPosition = new Vector2(UnityEngine.Random.Range(L.x, R.x), L.y);
        StartTeleportAnimation();
        Invoke("StartRemoval",0.8f);
    }

    private void StartRemoval()
    {
        BossAnimator.Play("BossFireProjectile");
        currentParticleEffect = RemoveTilesEffect;
        Invoke("RemoveTiles", 0.5f);
    }

    private void RemoveTiles()
    {
        Invoke("StopProjectileEffect", 0.2f);
        tileRemover.RemoveTiles();
        Invoke("FinishRemoval", 0.3f);
    }

    private void FinishRemoval()
    {
        BossState = boss_state.ShootOrbs;
        BossAnimator.Play("BossFireProjectile",-1,0.3f);
    }

    private void StartSwoop()
    {
        StartTeleportAnimation();
        nextBossPosition = bossPositionDictionary["LowerLeft"].position;
        Invoke("swoopIn", 0.8f);
    }

    private void StartTeleportAnimation()
    {
        BossAnimator.Play("BossTeleport",-1,0f);
    }

    public void TeleportToSpot()
    {
        Parent.position = nextBossPosition;
    }

    public void swoopIn()
    {
        StartCoroutine(SwoopIn());
    }

    private IEnumerator SwoopIn()
    {
        nextBossPosition = bossPositionDictionary["LowerRight"].position;
        Vector3 current = Parent.position;

        float elapsedTime = 0;
        float waitTime =  1f;

        while (elapsedTime < waitTime)
        {
            Parent.position = Vector3.Lerp(current, nextBossPosition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        BossAnimator.Play("BossTeleport", -1, 0f);
        nextBossPosition = bossPositionDictionary["Right"].position;
        StartTeleportAnimation();
        Invoke("MoveToIdle", 0.8f);
        yield return null;
    }

    #endregion
    private void CaughtKitchi()
    {

    }

    private void MoveToIdle()
    {
        BossState = boss_state.idle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.PLAYER && !BossState.Equals(boss_state.CaughtKitchi))
        {
            // instantly transition to Boss State
            BossState = boss_state.CaughtKitchi;

        }
    }
    private IEnumerator caughtKitchi()
    {
        yield return null;
    }

    public void ReleaseKitchi()
    {
        print("Releasing Kitchi");
    }

}

[Serializable]
public class BossPositions
{
    public string BossPositionName;
    public Transform BossPosition;
}