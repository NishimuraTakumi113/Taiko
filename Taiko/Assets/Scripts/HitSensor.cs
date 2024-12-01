using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSensor : MonoBehaviour
{
    // 最短の音符を格納する変数
    public string hitTag = "OMP";
    private GameObject hitNote = null;

    private float position;
    public float offset = 0f;


    public GameObject DonImage;
    public GameObject KaImage;


    [SerializeField] private float FindDistance = 1.0f;
    [SerializeField] private float SensorDistance = 0.5f;
    
    void Start()
    {
        position = transform.position.x + offset;
    }

    // Update is called once per frame
    void Update()
    {
        hitNote = FindNearestNote();
        if(Input.GetKeyDown(KeyCode.F)||Input.GetKeyDown(KeyCode.J)){
            DonImage.SetActive(true);
            HitNote(0, hitNote);
        }else if(Input.GetKeyUp(KeyCode.F)||Input.GetKeyUp(KeyCode.J)){
            DonImage.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.K)){
            KaImage.SetActive(true);
            HitNote(1, hitNote);
        }else if(Input.GetKeyUp(KeyCode.D)||Input.GetKeyUp(KeyCode.K)){
            KaImage.SetActive(false);
        }
    }

    GameObject FindNearestNote()
    {
        GameObject[] notes = GameObject.FindGameObjectsWithTag(hitTag);
        //対象なし
        if(notes.Length == 0){return null;}
        float minDistance = FindDistance;

        GameObject nearestNote = null;
        foreach(GameObject note in notes){
            float distance = Mathf.Abs(transform.position.x - note.transform.position.x);
            //最短の音符を更新
            if(distance < minDistance){
                minDistance = distance;
                nearestNote = note;
            }
        }
        
        return nearestNote;
    }

    private void HitNote(int ButtonType, GameObject hitNote){
        if (hitNote != null){
            int type = hitNote.GetComponent<NotesController>().type;
            bool isCorrrct = (ButtonType == type);//正しい譜面が叩かれたか
            if(isCorrrct){
                //正しい音符が叩かれた場合
                if(Mathf.Abs(hitNote.transform.position.x - position) <= SensorDistance &&Mathf.Abs(hitNote.transform.position.x - position) > SensorDistance/4){
                    hitNote.GetComponent<NotesController>().GoodHit();

                }else if(Mathf.Abs(hitNote.transform.position.x - position) <= SensorDistance/4){
                    hitNote.GetComponent<NotesController>().PerfectHit();
                    
                }else{
                    hitNote.GetComponent<NotesController>().MissHit();
                }
            }else{
                //間違った音符が叩かれた場合
                hitNote.GetComponent<NotesController>().MissHit();
            }
            // if(Mathf.Abs(hitNote.transform.position.x - position) <= SensorDistance &&Mathf.Abs(hitNote.transform.position.x - position) > SensorDistance/4){
            //     hitNote.GetComponent<NotesController>().GoodHit();

            // }else if(Mathf.Abs(hitNote.transform.position.x - position) <= SensorDistance/4){
            //     hitNote.GetComponent<NotesController>().PerfectHit();
                
            // }else{
            //     hitNote.GetComponent<NotesController>().MissHit();
            // }
        }
    }
}
