using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpController : MonoBehaviour
{
    public float speed = 1.0f;//テンポバーのスピード
    public Vector3 pastPosition;//移動前のポジション
    void Update()
    {
        if(GameMode.isReset){
            Destroy(this.gameObject);
        }
        
        if(GameMode.isPlay){
            if(GameMode.isEdit){
                this.transform.position -= new Vector3(speed * Time.deltaTime, 0 , 0);
            }else{
                this.transform.position -= new Vector3(speed * Time.deltaTime, 0 , 0);
                if(this.transform.position.x < -9){
                    Destroy(this.gameObject);
                }
            }
        }else{
            transform.position = pastPosition + new Vector3(-EditorMelody.scrollPoint, 0, 0);
        }
    }
}
