using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class E_MrHu : MonoBehaviour {


    [SerializeField]
    private GameObject eyePoint;//眼点，用于作旋转轴
    [SerializeField]
    private float getUpTime;//用于起床，作为缓冲
    [SerializeField]
    private float turningTime;//转头时间
    [SerializeField]
    private float turningPauseTime;//转头间歇时间
    [SerializeField]
    private int checkTimes;//扫视次数
    [SerializeField]
    private float maxAngle;//转头角度


    public enum state { SLEEP, AWAKE, ATTACK };
    private state currentState;

    private Sequence checkSequence;

    // Use this for initialization
    void Start () {
        currentState = state.SLEEP;
   
        checkSequence = DOTween.Sequence();
        checkSequence.Append(eyePoint.transform.DOLocalRotate(new Vector3(0, maxAngle, 0), turningTime));
        checkSequence.AppendInterval(turningPauseTime);
        checkSequence.Append(eyePoint.transform.DOLocalRotate(new Vector3(0, -maxAngle, 0), turningTime*2));
        checkSequence.AppendInterval(turningPauseTime);
        checkSequence.Append(eyePoint.transform.DOLocalRotate(new Vector3(0, maxAngle, 0), turningTime * 2));
        checkSequence.AppendInterval(turningPauseTime);
        checkSequence.Append(eyePoint.transform.DOLocalRotate(new Vector3(0, -maxAngle, 0), turningTime * 2));
        checkSequence.AppendInterval(turningPauseTime);
        checkSequence.Append(eyePoint.transform.DOLocalRotate(new Vector3(0, maxAngle, 0), turningTime * 2));
        checkSequence.AppendInterval(turningPauseTime);
        checkSequence.Append(eyePoint.transform.DOLocalRotate(new Vector3(0, -maxAngle, 0), turningTime * 2));
        checkSequence.AppendInterval(turningPauseTime);
        checkSequence.Append(eyePoint.transform.DOLocalRotate(new Vector3(0, 0, 0), turningTime));
        checkSequence.OnComplete(SleepAgain);
        //checkSequence.SetLoops(checkTimes, LoopType.Restart);

        checkSequence.SetAutoKill(false);
        checkSequence.Pause();

    }
	
    public void Activate()
    {
        if (currentState==state.SLEEP)
        {//如果在睡觉，就起来查看
            Managers.Data.ShowCaption("E_MrHu");
            currentState = state.AWAKE;
            StartCoroutine(GetUP());
        }
    }

    private IEnumerator GetUP() {
        yield return new WaitForSeconds(getUpTime);
        eyePoint.SetActive(true);
        checkSequence.Restart();
    }

    public void Seen() {

        
        RaycastHit hit;
        if(Physics.Raycast(eyePoint.transform.position,(Managers.Player.GetHeadPos()-eyePoint.transform.position).normalized,out hit)) {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("PLAYER_BODY"))
            {
                currentState = state.ATTACK;
                checkSequence.Kill();
                Messenger.Broadcast(GameEvent.LEVEL_FAILED);
            }
        }
    }
    

    private void SleepAgain() {
        currentState = state.SLEEP;
        eyePoint.transform.localRotation = Quaternion.identity;
        eyePoint.SetActive(false);
    }
}
