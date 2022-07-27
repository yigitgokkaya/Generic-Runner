using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSegment : MonoBehaviour
{

    public Transform[] spawnpoints;
    public SpawnSegment Instance {set;get;}
    private int continiousSegments;
     public List<Piece> ramps= new List<Piece>();
    public List<Piece> longbois= new List<Piece>();
    public List<Piece> jumpbois= new List<Piece>();
    public List<Piece> slidebois= new List<Piece>();
    
   [HideInInspector] public List<Piece> allbois= new List<Piece>(); // all the pieces in the pool




    public List<Segment> availableSegments = new List<Segment>();
    public List<Segment> availableTransition = new List<Segment>();
    [HideInInspector] public List<Segment> segments = new List<Segment>();


    private void Awake(){
        Instance = this;
    }
    public void SpawnPoints(){
        for(int i =0;i<spawnpoints.Length;i++){
             //GenerateSegment
             GenerateSegment(spawnpoints[i]);
         }
         //transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
     }
    public void DeSpawnPoints(){
        for(int i =0;i<spawnpoints.Length;i++){
             //GenerateSegment
              DestroySegment(spawnpoints[i]);
         }
    }
    
    // Start is called before the first frame update
     private void Start(){
         Debug.Log(spawnpoints.Length);
        // SpawnPoints();
     }
     /*
     for(int i =0;i<15;i++){
             //GenerateSegment
             GenerateSegment(spawnpoints[i]);
         }
         transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
     } 
     */
     public void DestroySegment(Transform t){
        int child=t.childCount;
        if(child>1){
            for(int i =0;i<child-1;i++){
            t.GetChild(i).gameObject.SetActive(false);
            }
        }
        
   }
     private void GenerateSegment(Transform t)
    {
        if(UnityEngine.Random.Range(0f,1f)<(continiousSegments*0.25f)){
            // Spawn Transition Segment
            continiousSegments=0;
            SpawnTransition(t);
            return;
        }
        else{
            continiousSegments++;
            SpawnSegmentt(t);
        }
        

    }

    private void SpawnSegmentt(Transform t)
    {
        
        int id = UnityEngine.Random.Range(0,availableSegments.Count);
        Segment s = GetSegment(id,false);
        s.transform.SetParent(t);
        s.transform.position=t.position;
       
        s.Spawn();
    }




    private void SpawnTransition(Transform t)
    {
        int id = UnityEngine.Random.Range(0,availableTransition.Count);
        Segment s = GetSegment(id,true);
        s.transform.SetParent(t);
        s.transform.position=t.position;
        s.Spawn();
    }
    public Segment GetSegment(int id,bool transition){
        Segment r = null;
        r=segments.Find(x=> x.SegId==id && x.transition==transition&& !x.gameObject.activeSelf); //(x=> x.SegId==id && x.transition==transition && !x.gameObject.activeSelf)
        if(r==null){
            GameObject go = Instantiate((transition)?availableTransition[id].gameObject : availableSegments[id].gameObject)as GameObject;
            r = go.GetComponent<Segment>();
            r.SegId=id;
            r.transition=transition;
            segments.Insert(0,r);
        }else{
            segments.Remove(r);
            segments.Insert(0,r);
        }
        return r;
    }
    public Piece GetPiece(PieceType type,int visualIndex){
        Piece p = allbois.Find(x=>x.type == type && x.visualIndex==visualIndex && !x.gameObject.activeSelf);


        if (p==null)
        {
            GameObject gameObject =null;
            if(type==PieceType.ramp){
                gameObject=ramps[visualIndex].gameObject;
            }else if(type==PieceType.longboi){
                gameObject=longbois[visualIndex].gameObject;
            }else if(type==PieceType.jumpboi){
                gameObject=jumpbois[visualIndex].gameObject;
            }else if(type==PieceType.slideboi){
                gameObject=slidebois[visualIndex].gameObject;
            }

            gameObject=Instantiate(gameObject);
            p=gameObject.GetComponent<Piece>();
            allbois.Add(p);


        }

        return p;

    }
    void Update(){
        
    }
}

