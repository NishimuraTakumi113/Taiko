using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    public float speed = 1f;
    public int type;

    public bool isSelected = false;//編集モードでの選択フラグ
    public Vector3 pastPosition;//移動前のポジション(実際の位置)
    public int pastIndex = -1;//移動前のインデックス
    public int locateIndex;//配置されている位置のインデックス

    void Update()
    {
        if(GameMode.isReset){
            Destroy(this.gameObject);
        }
        if(GameMode.isPlay){
            this.transform.position -= new Vector3(speed * Time.deltaTime, 0 , 0);
            if(!GameMode.isEdit){
                if(this.transform.position.x < -9){
                MissHit();
                Destroy(this.gameObject);
                }
            }
        }else{
            if(isSelected){
                if (Input.GetMouseButton(0)){
                    // マウス位置を取得してワールド座標に変換
                    Vector3 mousePosition = Input.mousePosition;
                    mousePosition.z = Camera.main.WorldToScreenPoint(this.transform.position).z; // オブジェクトのZ座標を維持
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    
                    //配置可能な位置に座標を合わせる
                    if(worldPosition.y >= -1 && worldPosition.y <= 1){
                        worldPosition.y = 0;
                        (float location, int index) = FindLocate(worldPosition.x);
                        Debug.Log(index);
                        locateIndex = index;
                        worldPosition.x = location - EditorMelody.scrollPoint + EditorMelody.tmpOffset.x;
                    }
                    worldPosition.z = 1 + (float)locateIndex/10;

                    // オブジェクトをマウス位置に移動
                    this.transform.position = worldPosition;
                }

                if (Input.GetMouseButtonUp(0)){
                    if(this.transform.position.y < -1 || this.transform.position.y > 1){
                        this.transform.position = pastPosition + new Vector3(-EditorMelody.scrollPoint, 0, 0);
                        isSelected = false;
                    }else{
                        if(EditorMelody.notesList[locateIndex] != 0){
                            if(pastIndex == -1){
                                Destroy(this.gameObject);
                            }
                            this.transform.position = pastPosition + new Vector3(-EditorMelody.scrollPoint, 0, 0);//既に配置されている場合は元の位置に戻す
                        }else{
                            pastPosition = this.transform.position + new Vector3(EditorMelody.scrollPoint, 0, 0);
                            if(pastIndex != -1){
                                EditorMelody.notesList[pastIndex] = 0;
                            }
                            EditorMelody.notesList[locateIndex] = type;
                            pastIndex = locateIndex;
                        }
                        isSelected = false;
                    }
                }
            }else{
                this.transform.position = pastPosition + new Vector3(-EditorMelody.scrollPoint, 0, 0);
            }
        }

        
    }

    public void PerfectHit()
    {
        if(GameMode.isEdit){
            return;
        }else{
            Destroy(this.gameObject);
            GameScore.score +=  (int)(1000.0f*((float)GameScore.combo/10.0f + 1.0f)); 
            GameScore.combo++;
            GameScore.perfect++;
            if(GameScore.combo > GameScore.maxCombo){
                GameScore.maxCombo = GameScore.combo;
            }
        }
    }

    public void GoodHit(){
        if(GameMode.isEdit){
            return;
        }else{
            Destroy(this.gameObject);
            GameScore.score += (int)(500.0f*((float)GameScore.combo/10.0f + 1.0f));
            GameScore.combo++;
            GameScore.good++;
            if(GameScore.combo > GameScore.maxCombo){
                GameScore.maxCombo = GameScore.combo;
            }
        }
    }

    public void MissHit(){
        if(GameMode.isEdit){
            return;
        }else{
            Destroy(this.gameObject);
            GameScore.combo = 0;
            GameScore.miss++;
        }
    }

    void OnMouseDown()
    {
        isSelected = true;
        pastPosition = this.transform.position + new Vector3(EditorMelody.scrollPoint, 0, 0);
    }

    //最短の配置できる位置を探す
    private (float nowLocation, int locateIndex) FindLocate(float worldX){
        float minDistance = 1000.0f;
        float nowLocation = 0.0f;
        int locateIndex = 0;

        for(int i = 0; i < EditorMelody.notesLocate.Count; i++){
            float distance = Mathf.Abs((worldX  - (EditorMelody.notesLocate[i] + EditorMelody.tmpOffset.x -EditorMelody.scrollPoint)));
            if(distance <= minDistance){
                minDistance = distance;
                nowLocation = EditorMelody.notesLocate[i];
                locateIndex = i;
            }
        }
        return (nowLocation, locateIndex);
    }

    public void DestroyInEditMode(){
        if(pastIndex != -1){
            EditorMelody.notesList[pastIndex] = 0;
            Destroy(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }
}