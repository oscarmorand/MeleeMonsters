using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerScript : MonoBehaviour, IPunObservable
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

    public string nickName;
    private int playerNumber;

    public Monsters monster;

    private int lives;
    public int percentage;

    internal bool isAlive = true;
    internal bool canStillPlay = true;

    internal bool isWrath = false;
    public float maxWrathTime = 15;
    public float wrathTime = 0;
    public float maxLoadingWrath = 500;
    public float loadingWrath = 495;
    internal float wrathPercentage = 0;

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

        pV = GetComponent<PhotonView>();

        rb = GetComponent<Rigidbody2D>();

        playerNumber = (PhotonNetwork.LocalPlayer.ActorNumber) - 1;

        if (gameObject.tag == "IA")
            nickName = "IA";
        else
            nickName = PhotonNetwork.LocalPlayer.NickName;

        
        gameManager = GameObject.Find("GameManagerPrefab").GetComponent<GameManager>();
        if(gameManager.GetGameState() == GameManager.States.Playing)
        {
            rb.simulated = true;
            levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            spawnList = levelManager.spawnList;
            levelManager.players.Add(this);
            currentState = States.Playing;
        }
        lives = gameManager.nbrLives;

        AddObservable();
    }

    // Update is called once per frame
    void Update()
    {
        //print(wrathPercentage);
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

    public void WrathState()
    {
        if(wrathTime <= 0)
        {
            isWrath = false;
            loadingWrath = 0;
            //bodySprite.color = Color.white;
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
        //if(pV.IsMine)
        //{
        //    percentage += damage;
        //    print("aie " + nickName + " a pris une attaque d'une puissance de " + damage + " pourcents et monte maintenant à " + percentage + " pourcents!");
        //    if(!isWrath)
        //    {
        //        float bonus = damage * 2.5f;
        //        loadingWrath += bonus;
        //        print(nickName+" gagne " + bonus + " sec dans la barre de wrath");
        //    }
        //}
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
            Vector3 tempVector = new Vector3(actualColor.r, actualColor.g, actualColor.b);
            stream.SendNext(tempVector);
        }
        else
        {
            Vector3 newVector = (Vector3)stream.ReceiveNext();
            Color newColor = new Color(newVector.x, newVector.y, newVector.z);
            if (newColor != actualColor)
            {
                WrathColor(newColor);
            }
        }
    }
}
