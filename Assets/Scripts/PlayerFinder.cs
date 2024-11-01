using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    private CircleCollider2D circleCollider;

    public List<IngameCharacterMover> targets = new List<IngameCharacterMover>();

    public void SetKillRange(float range)
    {
        circleCollider.radius = range;
    }

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<IngameCharacterMover>();
        if(player && player.playerType == EPlayerType.Crew)
        {
            if(!targets.Contains(player))
            {
                targets.Add(player);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<IngameCharacterMover>();
        if (player && player.playerType == EPlayerType.Crew)
        {
            if (targets.Contains(player))
            {
                targets.Remove(player);
            }
        }
    }

    public IngameCharacterMover GetFirstTarget()
    {
        float dist = float.MaxValue;
        IngameCharacterMover closeTarget = null;
        foreach(var target in targets)
        {
            float newDist = Vector3.Distance(transform.position, target.transform.position);
            if(newDist < dist)
            {
                dist = newDist;
                closeTarget = target;
            }
        }

        targets.Remove(closeTarget);
        return closeTarget;
    }
}
