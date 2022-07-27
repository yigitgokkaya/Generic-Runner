using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLevel : MonoBehaviour
{


    public const bool SHOW_COLLIDER =true;

    public static ManagerLevel Instance {set;get;}

    // Level Spawning

    private const float DISTANCE_BEFORE_SPAWN =100.0f;
    private const int INITIAL_SEGMENTS =16;
    private const int MAX_SEGMENTS_ON_SCREEN=25;
    private Transform cameraContainer;
    private int amountofActiveSegments;
    private int continiousSegments;
    private int currentLevel;
    private int currentSpawnZ;
    private int y1,y2,y3;


    // List of Pieces

    public List<Piece> ramps= new List<Piece>();
    public List<Piece> longbois= new List<Piece>();
    public List<Piece> jumpbois= new List<Piece>();
    public List<Piece> slidebois= new List<Piece>();
    
   [HideInInspector] public List<Piece> allbois= new List<Piece>(); // all the pieces in the pool




    // List of Segments
    public List<Segment> availableSegments = new List<Segment>();
    public List<Segment> availableTransition = new List<Segment>();
    [HideInInspector] public List<Segment> segments = new List<Segment>();


    // Gameplay
     private bool isMoving=false;


     private void Awake(){
         Instance = this;
         //cameraContainer=Camera.main.transform;
         currentSpawnZ =10;
         currentLevel=0;  
     }
     private void Start(){
         for(int i =0;i<INITIAL_SEGMENTS;i++){
             //GenerateSegment
             GenerateSegment();
         }
         transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z-5);
     }

    private void GenerateSegment()
    {
        if(UnityEngine.Random.Range(0f,1f)<(continiousSegments*0.25f)){
            // Spawn Transition Segment
            continiousSegments=0;
            SpawnTransition();
            return;
        }
        else{
            continiousSegments++;
            SpawnSegment();
        }
        

    }

    private void SpawnTransition()
    {
         List<Segment> possibleTrans = availableTransition.FindAll(x=> x.beginY1==y1 || x.beginY2==y2 || x.beginY3==y3);
        int id = UnityEngine.Random.Range(0,possibleTrans.Count);
        Segment s = GetSegment(id,true);
        y1=s.endY1;
        y2=s.endY2;
        y3=s.endY3;
        s.transform.SetParent(transform);
        s.transform.localPosition=Vector3.forward*currentSpawnZ;
        currentSpawnZ+=s.lenght;
        amountofActiveSegments++;
        s.Spawn();
    }

    private void SpawnSegment()
    {
        List<Segment> possibleSegments = availableSegments.FindAll(x=> x.beginY1==y1 || x.beginY2==y2 || x.beginY3==y3);
        int id = UnityEngine.Random.Range(0,possibleSegments.Count);
        Segment s = GetSegment(id,false);
        y1=s.endY1;
        y2=s.endY2;
        y3=s.endY3;
        s.transform.SetParent(transform);
        s.transform.localPosition=Vector3.forward*currentSpawnZ;
        currentSpawnZ+=s.lenght;
        amountofActiveSegments++;
        s.Spawn();
    }

    public Segment GetSegment(int id,bool transition){
        Segment r = null;
        r=segments.Find(x=> x.SegId==id && x.transition==transition && !x.gameObject.activeSelf);
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

}
