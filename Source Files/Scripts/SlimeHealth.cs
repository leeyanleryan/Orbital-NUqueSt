using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeHealth : EnemyHealth
{
    private bool hasLooped = false;
    
    public override void SlimeDeath()
    {
        if (!hasLooped)
        {
            for (int i = 0; i < 6; i++)
            {
                //if there is an active quest in the slot
                if (player.questList.questSlots[i].count == 1)
                {
                    player.questList.questSlots[i].slimesRequired--;
                }
                if (i == 5)
                {
                    hasLooped = true;
                }
            }
        }
        Destroy(this.gameObject);
    }
}
