using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class G_Worker : MonoBehaviour {

    [SerializeField]
    private GameObject eyePoint;//眼点，用于作旋转轴
    [SerializeField]
    private float walkTime;//单程时间
    [SerializeField]
    private float turningPauseTime;//转头时间
    [SerializeField]
    private Vector3 pointA;
    [SerializeField]
    private Vector3 pointB;

    public enum state { WALK, ATTACK };
    private state currentState;

    private Sequence checkSequence;

    void Start()
    {
        currentState = state.WALK;
        checkSequence = DOTween.Sequence();
        checkSequence.Append(transform.DOLocalMove(pointA, walkTime));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, 0), turningPauseTime));
        checkSequence.Append(transform.DOLocalMove(pointB, walkTime));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, 180, 0), turningPauseTime));
        checkSequence.SetLoops(-1, LoopType.Restart);

        checkSequence.SetAutoKill(false);

    }


    public void Seen()
    {
        RaycastHit hit;
        if (Physics.Raycast(eyePoint.transform.position, (Managers.Player.GetHeadPos() - eyePoint.transform.position).normalized, out hit))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("PLAYER_BODY"))
            {
                currentState = state.ATTACK;
                checkSequence.Kill();
                Messenger.Broadcast(GameEvent.LEVEL_FAILED);
            }
        }
    }

}
