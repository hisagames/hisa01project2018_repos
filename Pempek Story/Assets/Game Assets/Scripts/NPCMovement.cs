using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPCMovement : MonoBehaviour
{
    public enum NPCState
    {
        idle,
        walking,
        thinking,
        interestedToBuy,
        inQueueZone,
        buyingThings,
        alreadyBuyThings
    };

    [SerializeField]
    NPCState npcState;
    
    Animator animator;

    //---- Movement Parameter ---------------
    public Vector2 npcSpeed;
    public bool walkFromLeftToRight;
    bool alreadyChangeDirection;
    //---------------------------------------

    //---- Walk to Thinking Parameter -------
    float timeToThinking;
    public float maxTimeToThinking;
    public float thinkingPercentage;
    //---------------------------------------

    //---- Thinking to Walk Parameter -------
    float timeToWalk;
    public float maxTimeToWalk;
    //---------------------------------------

    //---- Thinking to Interested to Buy Parameter ---------
    public float interestedToBuyPercentage;
    //------------------------------------------------------

    //---- Interested to Buy -> in Queue Zone Parameter ----
    bool isCharacterInQueueZone;
    //------------------------------------------------------

    //---- In Queue Zone to Buying Things ------------------
    float timeToWaitInQueueZone = 5f; //waiting for seller to cooking
    [SerializeField]
    int queueNumber;
    //------------------------------------------------------

    //---- Buying Things to Already Buy Things -------------
    bool isCharacterInBuyingZone;
    //------------------------------------------------------

    //---- Emoticon Parameter ---------------
    public SpriteRenderer emoticon;
    //---------------------------------------
    
    [SerializeField]
    Transform BuyingZone;
    [SerializeField]
    NPCEmoticon npcEmoticon;
    [SerializeField]
    GameObject coinAnimated;

    void Awake()
    {
        BuyingZone = GameObject.Find("BuyingZone").transform;
    }

    void Start () {
        queueNumber = -1;
        emoticon.enabled = false;
        npcState = NPCState.walking;
        alreadyChangeDirection = false;
        animator = GetComponent<Animator>();
        animator.SetInteger("State", 0); //state 0 idle animation
    }
	
	void FixedUpdate ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        if (Player.instance.inSellerMode)
        {
            switch (npcState)
            {
                case NPCState.walking:
                    if (walkFromLeftToRight)
                        walking(npcSpeed);
                    else
                        walking(-npcSpeed);

                    if (timeToThinking >= maxTimeToThinking)
                    {
                        float tempThinkingValue = Random.Range(0, 100);
                        if (tempThinkingValue <= thinkingPercentage)
                        {
                            //Debug.Log("Switch to Thinking...");
                            npcState = NPCState.thinking;
                            animator.SetInteger("State", 0); //state 0 idle animation
                            npcEmoticon.changeEmoticon(NPCEmoticon.Type.thinking);
                            emoticon.enabled = true;
                        }
                        else
                        {
                            //Debug.Log("Continue Walking...");
                        }
                        timeToThinking = 0;
                    }
                    else
                    {
                        timeToThinking += Time.deltaTime;
                    }
                    break;
                case NPCState.thinking:
                    if (timeToWalk >= maxTimeToWalk)
                    {
                        float tempInterestedToBuyPercentage = Random.Range(0, 100);

                        int tempQueueId = QueueManager.instance.isThereEmptyQueue();
                        if (tempQueueId != -1 && tempInterestedToBuyPercentage <= interestedToBuyPercentage)
                        {
                            //Debug.Log("Switch to Interested To Buy...");
                            npcState = NPCState.interestedToBuy;
                            if (transform.position.x < -QueueManager.instance.queueZone[tempQueueId].transform.position.x) //-4.781 is seller table position
                                animator.SetInteger("State", 1); //state 1 walk to right animation
                            else
                                animator.SetInteger("State", 2); //state 2 walk to left animation

                            npcEmoticon.changeEmoticon(NPCEmoticon.Type.interested);
                            queueNumber = tempQueueId;
                            QueueManager.instance.queueCustomer[queueNumber] = gameObject;
                        }
                        else
                        {
                            //Debug.Log("Switch to Walking...");
                            npcState = NPCState.walking;
                            npcEmoticon.changeEmoticon(NPCEmoticon.Type.dislike);
                        }
                        timeToWalk = 0;
                    }
                    else
                    {
                        timeToWalk += Time.deltaTime;
                    }
                    break;
                case NPCState.interestedToBuy:
                    float npcDistanceToQueueZone = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                        new Vector2(QueueManager.instance.queueZone[queueNumber].transform.position.x, QueueManager.instance.queueZone[queueNumber].transform.position.y));
                    transform.DOMove(new Vector3(QueueManager.instance.queueZone[queueNumber].transform.position.x,
                        QueueManager.instance.queueZone[queueNumber].transform.position.y, transform.position.z), npcDistanceToQueueZone, false);

                    if (isCharacterInQueueZone)
                    {
                        if (queueNumber == 0)
                        {
                            //Debug.Log("Switch to In Queue Zone...");
                            npcState = NPCState.inQueueZone;
                            animator.SetInteger("State", 0); //state 0 idle animation
                            npcEmoticon.changeEmoticon(NPCEmoticon.Type.happy);
                        }
                        else
                        {
                            int tempNewQueueNumber = QueueManager.instance.getCustomerQueueNumber(gameObject);
                            if (tempNewQueueNumber != queueNumber)
                            {
                                animator.SetInteger("State", 2); //state 2 walk to left animation
                                queueNumber = tempNewQueueNumber;
                                isCharacterInQueueZone = false;
                            }
                            else
                            {
                                animator.SetInteger("State", 0); //state 0 idle animation
                            }
                        }
                    }

                    break;
                case NPCState.inQueueZone:
                    if (timeToWaitInQueueZone >= 0)
                    {
                        timeToWaitInQueueZone -= Time.deltaTime;
                    }
                    else
                    {
                        float npcDistanceToBuyingZone = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                            new Vector2(BuyingZone.position.x, BuyingZone.position.y));
                        transform.DOMove(new Vector3(BuyingZone.position.x, BuyingZone.position.y, transform.position.z), npcDistanceToBuyingZone, false);
                        animator.SetInteger("State", 2); //state 2 walk to left animation

                        if (isCharacterInBuyingZone)
                        {
                            //Debug.Log("Switch to Buying Things...");
                            npcState = NPCState.buyingThings;
                            //QueueManager.instance.queueCustomer[queueNumber] = null;
                            QueueManager.instance.queueTransition();
                            queueNumber = -1;
                        }
                    }
                    break;
                case NPCState.buyingThings:
                    if (true)
                    {
                        //Debug.Log("Switch to In Already Buy Things...");
                        npcState = NPCState.alreadyBuyThings;
                        npcEmoticon.changeEmoticon(NPCEmoticon.Type.love);
                    }
                    break;
                case NPCState.alreadyBuyThings:
                    if (walkFromLeftToRight)
                        walking(npcSpeed);
                    else
                        walking(-npcSpeed);
                    break;
            }
        }
        else
        {
            if (npcState == NPCState.inQueueZone)
            {
                queueNumber = -1;
                QueueManager.instance.queueTransition();
                npcEmoticon.changeEmoticon(NPCEmoticon.Type.dislike);
                npcState = NPCState.walking;
                alreadyChangeDirection = false;
                isCharacterInQueueZone = false;
                isCharacterInBuyingZone = false;
            }
            else if (npcState == NPCState.buyingThings)
            {
                queueNumber = -1;
                QueueManager.instance.queueTransition();
                npcEmoticon.changeEmoticon(NPCEmoticon.Type.love);
                npcState = NPCState.alreadyBuyThings;
                alreadyChangeDirection = false;
                isCharacterInQueueZone = false;
                isCharacterInBuyingZone = false;
            }
            else
            {
                queueNumber = -1;
                QueueManager.instance.queueTransition();
                emoticon.enabled = false;
                npcState = NPCState.walking;
                isCharacterInQueueZone = false;
                isCharacterInBuyingZone = false;
            }

            if (walkFromLeftToRight)
                walking(npcSpeed);
            else
                walking(-npcSpeed);
        }
    }

    void walking(Vector2 speed)
    {
        if(speed.x > 0)
            animator.SetInteger("State", 1); //state 1 walk to right animation
        else if(speed.x < 0)
            animator.SetInteger("State", 2); //state 2 walk to left animation


        transform.DOMoveX(transform.position.x + speed.x, Time.deltaTime, false);

        if(!isCharacterInBuyingZone)
            transform.DOMoveY(transform.position.y + speed.y, Time.deltaTime, false);

        if (!alreadyChangeDirection && characterInBorderLine())
            npcSpeed = new Vector2(npcSpeed.x, npcSpeed.y * -1f);
    }

    bool characterInBorderLine()
    {
        if (transform.position.y >= 0f || transform.position.y <= -4f)
        {
            alreadyChangeDirection = true;
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "QueueZone" + (queueNumber + 1))
        {
            if (npcState == NPCState.interestedToBuy)
                isCharacterInQueueZone = true;
        }
        if (col.name == BuyingZone.name)
        {
            if (npcState == NPCState.inQueueZone)
            {
                isCharacterInBuyingZone = true;
                CoinManager.instance.AddMoney(GameBaseManager.instance.pempekPrice * GameBaseManager.instance.pempekPerCustomer);
                coinAnimated.GetComponent<FlyCoinAnimated>().FlyingAnimated();
            }
        }
        if (col.tag == "Outzone")
        {
            Destroy(gameObject);
        }
    }
}
