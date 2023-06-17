using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChangePosition : StateMachineBehaviour
{
    private int currentPositionIndex = 0;
    private Transform newPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var finalBoss = animator.GetComponent<FinalBoss>();
        var pickNewIndex = PickNewIndex(finalBoss.positions.Count);
        newPos = finalBoss.positions[pickNewIndex];
    }

    private int PickNewIndex(int posCount)
    {
        var index = currentPositionIndex;
        while (index == currentPositionIndex)
        {
            index = Random.Range(0, posCount);
        }

        currentPositionIndex = index;
        return index;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = newPos.position;
        animator.transform.eulerAngles = newPos.eulerAngles;
    }

}
