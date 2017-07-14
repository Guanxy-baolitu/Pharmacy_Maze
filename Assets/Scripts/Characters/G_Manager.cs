using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class G_Manager : MonoBehaviour {

    [SerializeField]
    private GameObject eyePoint;//眼点，用于作旋转轴
    [SerializeField]
    private float walkSpeed;//行走速度
    [SerializeField]
    private float turningPauseTime;//转角时间
    [SerializeField]
    private float pauseTime;//在架子及柜台停留时间
    [SerializeField]
    private Vector3 CounterCorner;
    [SerializeField]
    private Vector3 Countera;
    [SerializeField]
    private Vector3 Counterb;
    [SerializeField]
    private Vector3 CounterToShelfA;
    [SerializeField]
    private Vector3 ShelfAa;
    [SerializeField]
    private Vector3 ShelfAb;
    [SerializeField]
    private Vector3 ShelfAc;
    [SerializeField]
    private Vector3 ShelfBa;
    [SerializeField]
    private Vector3 ShelfBb;
    [SerializeField]
    private Vector3 ShelfBc;//楼梯口停留
    [SerializeField]
    private Vector3 StairToCounter;
    [SerializeField]
    private Vector3 Counterc;

    public enum state { WALK, ATTACK };//不需要加一个READ状态吧
    private state currentState;

    private Sequence checkSequence;

    private float[] time=new float[12];

    void Start()
    {
        currentState = state.WALK;
        CalculateTime();
        checkSequence = DOTween.Sequence();
        checkSequence.Append(transform.DOLocalMove(Countera, time[0]));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, 0), turningPauseTime));
        checkSequence.Append(transform.DOLocalMove(Counterb, time[1]));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, 180, 0), turningPauseTime));
        checkSequence.Append(transform.DOLocalMove(CounterToShelfA, time[2]));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, -90, 0), turningPauseTime));
        checkSequence.Append(transform.DOLocalMove(ShelfAa, time[3]));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, 180, 0), turningPauseTime));
        checkSequence.Append(transform.DOLocalMove(ShelfAb, time[4]));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, -90, 0), turningPauseTime));
        checkSequence.AppendInterval(pauseTime);//看架子
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, 180, 0), turningPauseTime));
        checkSequence.Append(transform.DOLocalMove(ShelfAc, time[5]));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, 90, 0), turningPauseTime));
        checkSequence.Append(transform.DOLocalMove(ShelfBa, time[6]));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, 0), turningPauseTime));
        checkSequence.Append(transform.DOLocalMove(ShelfBb, time[7]));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, 90, 0), turningPauseTime));
        checkSequence.AppendInterval(pauseTime);//看架子
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, 0), turningPauseTime));
        checkSequence.Append(transform.DOLocalMove(ShelfBc, time[8]));
        checkSequence.AppendInterval(pauseTime);//看楼梯
        checkSequence.Append(transform.DOLocalMove(StairToCounter, time[9]));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, -90, 0), turningPauseTime));
        checkSequence.Append(transform.DOLocalMove(Counterc, time[10]));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, 0), turningPauseTime));
        checkSequence.Append(transform.DOLocalMove(Counterb, time[11]));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, -90, 0), turningPauseTime));
        checkSequence.Append(transform.DOLocalMove(Countera, time[1]));
        checkSequence.Append(transform.DOLocalRotate(new Vector3(0, 180, 0), turningPauseTime));
        checkSequence.Append(transform.DOLocalMove(CounterCorner, time[0]));
        checkSequence.AppendInterval(pauseTime);//看柜台
        checkSequence.SetLoops(-1, LoopType.Restart);

        checkSequence.SetAutoKill(false);

    }

    void CalculateTime(){
        time[0] = Vector3.Distance(Countera, CounterCorner) / walkSpeed;
        time[1] = Vector3.Distance(Counterb, Countera) / walkSpeed;
        time[2] = Vector3.Distance(CounterToShelfA, Counterb) / walkSpeed;
        time[3] = Vector3.Distance(ShelfAa, CounterToShelfA) / walkSpeed;
        time[4] = Vector3.Distance(ShelfAb, ShelfAa) / walkSpeed;
        time[5] = Vector3.Distance(ShelfAc, ShelfAb) / walkSpeed;
        time[6] = Vector3.Distance(ShelfBa, ShelfAc) / walkSpeed;
        time[7] = Vector3.Distance(ShelfBb, ShelfBa) / walkSpeed;
        time[8] = Vector3.Distance(ShelfBc, ShelfBb) / walkSpeed;
        time[9] = Vector3.Distance(StairToCounter,ShelfBc) / walkSpeed;
        time[10] = Vector3.Distance(Counterc, StairToCounter) / walkSpeed;
        time[11] = Vector3.Distance(Counterb, Counterc) / walkSpeed;
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
