using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public PlayerMovement player;
    public float delayBeforeStart;
    private float actDelay;
    private List<PositionInfo> playerPositions = new List<PositionInfo>();

    public GameObject shadowObj;
    //public Animator shadowAnim;

    private bool created;
    //private string savedAnim;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //create information for position
        PositionInfo posNew = new PositionInfo
        {
            position = player.transform.position,
            rotation = player.transform.rotation,
            scale = player.transform.localScale,
            //anim = player.Anim.GetCurrentAnimatorClipInfo(0)[0].clip,
        };

        //add info to list
        playerPositions.Add(posNew);
        //wait for delay
        if (actDelay < delayBeforeStart)
        {
            actDelay += Time.deltaTime;
            if (actDelay > delayBeforeStart)
            {
                //turn on shadow
                shadowObj.SetActive(true);
                //place obj
                SetShadowPos(playerPositions[0]);
            }
            return;
        }

        //set info
        PositionInfo setInfo = playerPositions[0];
        SetShadowPos(setInfo);
        //set animation values
        //SetShadowAnimations(setInfo);
        //remove first position from list
        playerPositions.RemoveAt(0);
    }

    void SetShadowPos(PositionInfo setInfo)
    {
        //move object
        shadowObj.transform.position = setInfo.position;
        shadowObj.transform.rotation = setInfo.rotation;
        shadowObj.transform.localScale = setInfo.scale;
    }
    
    //void SetShadowAnimations(PositionInfo setInfo) {}

    [System.Serializable]
    public class PositionInfo
    {
        //position
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        //animation
        //public AnimationClip anim;
    }
}