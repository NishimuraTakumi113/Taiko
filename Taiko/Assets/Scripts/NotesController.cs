using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    public float speed = 5f;
    public int type;
    void Update()
    {
        this.transform.position -= new Vector3(speed * Time.deltaTime, 0 , 0);

        if(this.transform.position.x < -9){
            MissHit();
            Destroy(this.gameObject);
        }
    }

    public void PerfectHit()
    {
        Destroy(this.gameObject);
        GameScore.score +=  (int)(1000.0f*((float)GameScore.combo/10.0f + 1.0f)); 
        GameScore.combo++;
        GameScore.perfect++;
        if(GameScore.combo > GameScore.maxCombo){
            GameScore.maxCombo = GameScore.combo;
        }
    }

    public void GoodHit(){
        Destroy(this.gameObject);
        GameScore.score += (int)(500.0f*((float)GameScore.combo/10.0f + 1.0f));
        GameScore.combo++;
        GameScore.good++;
        if(GameScore.combo > GameScore.maxCombo){
            GameScore.maxCombo = GameScore.combo;
        }
    }

    public void MissHit(){
        Destroy(this.gameObject);
        GameScore.combo = 0;
        GameScore.miss++;
    }
}
