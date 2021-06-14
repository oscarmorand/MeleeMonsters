using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerScript : MonoBehaviour, IPunObservable, IPunInstantiateMagicCallback 
{
    public enum Monsters
    {
        Avocado,
        Ghost,
        Yeti,
        Kraken,
    }

    public enum States
    {
        Menu,
        Frozen,
        Playing,
        Dead,
    }

    public States currentState = States.Frozen;

    public string nickName = "Player";
    private int playerNumber;

    //public Monsters monster;
    public int monsterIndex = -1;

    private int lives;
    public int percentage;

    internal bool isAlive = true;
    internal bool canStillPlay = true;

    public bool isWrath = false;
    public float maxWrathTime = 15;
    public float wrathTime = 0;
    public float maxLoadingWrath = 500;
    public float loadingWrath = 495;
    internal float wrathPercentage = 0;

    private bool initForPlayingDone = false;

    private PlayerMovement pMovement;
    private PlayerCollisions pCollisions;
    private PlayerAnimation pAnimation;
    private PlayerInputs pInputs;

    private PhotonView pV;

    private Rigidbody2D rb;

    private List<Transform> spawnList;

    private LevelManager levelManager;
    private GameManager gameManager;

    public float reappearitionTime;

    private ExitGames.Client.Photon.Hashtable _myCustomPropreties = new ExitGames.Client.Photon.Hashtable();

    public List<SpriteRenderer> sprites;
    internal Color actualColor;

    private GameObject aMGameObject;
    private AudioManager aM;
    //private PlayerSoundManager pSM;
    private int localInt;

    public bool isHitStun;

    // Start is called before the first frame update
    void Start()
    {
        actualColor = Color.white;
        pMovement = GetComponent<PlayerMovement>();
        pCollisions = GetComponent<PlayerCollisions>();
        pAnimation = GetComponent<PlayerAnimation>();
        pInputs = GetComponent<PlayerInputs>();

        aMGameObject = GameObject.Find("AudioManager");
        aM = aMGameObject.GetComponent<AudioManager>();
        //pSM = GameObject.Find("PlayerSoundManager").GetComponent<PlayerSoundManager>();

        pV = GetComponent<PhotonView>();

        rb = GetComponent<Rigidbody2D>();

        playerNumber = (PhotonNetwork.LocalPlayer.ActorNumber) - 1;

        if (gameObject.tag == "IA")
            nickName = "IA";
        else
        {
            nickName = pV.Owner.NickName;
            if (nickName == "")
            { 
                nickName = "Player";
            }
        }
            

        
        gameManager = GameObject.Find("GameManagerPrefab").GetComponent<GameManager>();
        
        lives = gameManager.nbrLives;

        AddObservable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!initForPlayingDone)
        {
            if (gameManager.GetGameState() == GameManager.States.Playing)
            {
                rb.simulated = true;
                levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
                spawnList = levelManager.spawnList;
                levelManager.playersScripts.Add(this);
                currentState = States.Playing;
                initForPlayingDone = true;
            }
        }
        
        //print(wrathPercentage);
        if (pV.IsMine)
        {
            if (isWrath)
            {
                WrathState();
                wrathPercentage = (wrathTime / maxWrathTime) * 100;
            }
            else
            {
                NormalState();
                wrathPercentage = (loadingWrath / maxLoadingWrath) * 100;
            }
        }
        else
            isWrath = localInt == 1;
    }

    public void WrathState()
    {
        if(wrathTime <= 0)
        {
            isWrath = false;
            loadingWrath = 0;
            WrathColor(Color.white);
        }
        else
        {
            wrathTime -= Time.deltaTime;
        }
    }

    public void NormalState()
    {
        if(loadingWrath < maxLoadingWrath)
            loadingWrath += Time.deltaTime;
    }


    public void WrathModeState()
    {
        if (loadingWrath >= maxLoadingWrath)
        {
            wrathTime = maxWrathTime;
            isWrath = true;
            loadingWrath = 0;
            WrathColor(Color.red);
        }
    }


    public void WrathColor(Color color)
    {
        actualColor = color;
        foreach(SpriteRenderer sprite in sprites)
        {
            sprite.color = color;
        }
    }

    public void OnEnterReappear()
    {
        isAlive = false;
        lives--;
        CheckStillAlive();
        if (canStillPlay)
            Invoke("Reappear", reappearitionTime);
        else
        {
            Destroy(gameObject);
            if (levelManager.inSolo)
            {
                Destroy(levelManager.gameObjectIA);
            }
        }
    }

    private void Reappear()
    {
        rb.velocity = new Vector2(0, 0);
        int randomSpawnPoint = Random.Range(0, spawnList.Count);
        transform.position = spawnList[randomSpawnPoint].position;
        //transform.position = new Vector3(0, 1, 0);
        isAlive = true;
        percentage = 0;
    }

    void CheckStillAlive()
    {
        if(lives <= 0)
        {
            canStillPlay = false;
            _myCustomPropreties["StillInGame"] = false;
            PhotonNetwork.LocalPlayer.CustomProperties = _myCustomPropreties;
            levelManager.SearchForWinner();
        }
    }

    public void TakeDamage(int damage)
    {
        percentage += damage;
        print("aie " + nickName + " a pris une attaque d'une puissance de " + damage + " pourcents et monte maintenant à " + percentage + " pourcents!");
        if (!isWrath)
        {
            float bonus = damage * 2.5f;
            loadingWrath += bonus;
            print(nickName + " gagne " + bonus + " sec dans la barre de wrath");
        }

        aM.Play("oof");
    }

    public IEnumerator HitStunState(float hitStunTime)
    {
        isHitStun = true;

        yield return new WaitForSeconds(hitStunTime);

        isHitStun = false;
    }

    public void WrathSustain(int damage)
    {
        float sustain = (float)damage / 20;
        print("sustain de " + sustain + " secondes!");
        wrathTime += sustain;
    }



    private void AddObservable()
    {
        if (!pV.ObservedComponents.Contains(this))
        {
            pV.ObservedComponents.Add(this);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            localInt = 0;
            if (isWrath)
                localInt = 1;

            Vector3 tempVector = new Vector3(actualColor.r, actualColor.g, actualColor.b);
            stream.SendNext(tempVector);
            stream.SendNext(localInt);
            stream.SendNext(wrathPercentage);
        }
        else
        {
            Vector3 tempVector = (Vector3)stream.ReceiveNext();
            Color newColor = new Color(tempVector.x, tempVector.y, tempVector.z);
            if (newColor != actualColor)
            {
                WrathColor(newColor);
            }
            localInt = (int)stream.ReceiveNext();
            wrathPercentage = (float)stream.ReceiveNext();
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        // e.g. store this gameobject as this player's charater in Player.TagObject
        info.Sender.TagObject = this.gameObject;
        object[] instantiationData = info.photonView.InstantiationData;
        monsterIndex = (int)instantiationData[0]; 
    }
}
