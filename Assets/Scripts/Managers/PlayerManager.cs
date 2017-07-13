using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}

    //这里关于health的部分是源代码自带的，暂时没用，万一以后有用呢先不改
	public int health {get; private set;}
	public int maxHealth {get; private set;}

    /// <summary>
    /// 时间处理代码
    /// </summary>
    public float MaxTime=5f;
	public float CountDown { get; private set; }
    private int previousSec = 0;
	public Text TimeLeftText=null;
    
	public void Startup() {
		Debug.Log("Player manager starting...");

		UpdateData(50, 100);

		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		status = ManagerStatus.Started;
		//时间处理代码
		CountDown=MaxTime;
        //人物死亡处理代码
        Messenger.AddListener(GameEvent.LEVEL_FAILED, Respawn);
    }

    /// <summary>
    /// 时间处理代码
    /// </summary>
    public void Update(){
		CountDown-=Time.deltaTime;
		if(CountDown<=0){
			Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        }
        if (TimeLeftText != null)
        {
            int sec = Mathf.RoundToInt(CountDown);
            if(sec==previousSec) { //稍微减少计算量，只有不同的秒数才更新
                return;
            }
            else{
                previousSec = sec;
                int remain = sec % 60;
                TimeLeftText.text = "" + (sec / 60).ToString("D2") + ":" + remain.ToString("D2");
                if(sec<=5) {
                    TimeLeftText.color = Color.red;
                }
                else {
                    TimeLeftText.color = Color.white;
                }
            }
        }
    }

    /// <summary>
    /// 暴力加血(读档用)
    /// </summary>
	public void UpdateData(int health, int maxHealth) {
		this.health = health;
		this.maxHealth = maxHealth;
	}
    /// <summary>
    /// 暴力改时间（读档用）
    /// </summary>
	public void UpdateTime(float t)
    {
        CountDown = t;
    }
    /// <summary>
    /// 非暴力加血
    /// </summary>
	public void ChangeHealth(int value) {
		health += value;
		if (health > maxHealth) {
			health = maxHealth;
		} else if (health < 0) {
			health = 0;
		}

		if (health == 0) {
			Messenger.Broadcast(GameEvent.LEVEL_FAILED);
		}
		Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
	}
    /// <summary>
    /// 重置
    /// </summary>
	public void Respawn() {
		UpdateData(50, 100);
        CountDown = MaxTime;
	}
}
