using UnityEngine;

public class ChargeScript : Enemy
{
    [Header("Charge")]
    [SerializeField] float startChargeTime;
    [SerializeField] float currentChargeTime;
    [SerializeField] Vector2 positionTargetWasLastSeen;
    [SerializeField] float startCalmDownTime;
    [SerializeField] float calmDownTime;
    [SerializeField] float chargeSpeed = 2000;
    bool currentlyAttacking;
    void Charge()
    {
        if (Vector2.Distance(transform.position, target.position) > targetDistanceStop)
        {
            // then move quickly towards that point
            vecTarget = positionTargetWasLastSeen;
        }
        else
        {
            // it then calms down for a second or two
            calmDownTime = startCalmDownTime;
        }
    }
    protected override void UpdatePath() // man kan tro att denna inte har några referenser, men det har den, unity ljuger
    {
        if (willHunt == true)
        {
            if (seeker.IsDone() && Vector2.Distance(rbody.position, target.position) > targetDistanceStop)
            {
                seeker.StartPath(rbody.position, vecTarget, OnPathComplete);
            }
        }
        else if (willHunt == false)
        {
            if (seeker.IsDone() && Vector2.Distance(rbody.position, PositionsToWalkBetween[currentWalkBetween]) <= targetDistanceStop)
            {
                if (currentWalkBetween == PositionsToWalkBetween.Length - 1)
                    currentWalkBetween = 0;
                else
                    currentWalkBetween++;
                seeker.StartPath(rbody.position, PositionsToWalkBetween[currentWalkBetween], OnPathComplete);
            }
        }
    }
    public override void FixedUpdate() // denna är public för annars funkar inte override
    {
        //if the player is in range
        if (Vector2.Distance(gameObject.transform.position, target.position) < aggroRange && calmDownTime <= 0 && willHunt == true)
        {
            // charge for a certain amount of time while choosing a location where the player were
            currentChargeTime = startChargeTime;
        }
        if (currentlyAttacking == false)
        {
            if (path == null)
            {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndPath = true;
                return;
            }
            else
            {
                reachedEndPath = false;
            }
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rbody.position).normalized;
            Vector2 force;
            if (currentlyAttacking == true)
                force = direction * chargeSpeed * Time.deltaTime;
            else
                force = direction * speed * Time.deltaTime;

            rbody.AddForce(force);

            float distance = Vector2.Distance(rbody.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            if (force.x > 0.01f)
                gfxObj.transform.localScale = new Vector3(1f, 1f, 1f);
            else
                gfxObj.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        while (currentChargeTime > 0 || calmDownTime > 0)
        {
            currentlyAttacking = true;
        }
        if (currentChargeTime > 0)
        {
            currentChargeTime -= Time.deltaTime;
            positionTargetWasLastSeen = target.position;
        }
        else
        {
            Charge();
        }
    }
}