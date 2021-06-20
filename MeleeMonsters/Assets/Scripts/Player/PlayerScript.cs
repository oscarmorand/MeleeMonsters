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

    public int lives;
    public int percentage;

    internal bool isAlive = true;
    internal bool canStillPlay = true;

    public bool isWrath = false;
    public float maxWrathTime = 15;
    public float wrathTime = 0;
    public float maxLoadingWrath = 500;
    public float loadingWrath = 495;
    internal float wrathPercentage = 0;

    private bool initForStart321Go = false;
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

    private List<Material> Materials = new List<Material>();

    public float reappearitionTime;

    private ExitGames.Client.Photon.Hashtable _myCustomPropreties = new ExitGames.Client.Photon.Hashtable();

    public List<SpriteRenderer> spritesToColor;
    public List<SpriteRenderer> spritesToShow;
    internal Color actualColor;
    public Color wrathColor;

    private GameObject aMGameObject;
    private AudioManager aM;
    //private PlayerSoundManager pSM;
    private int localInt;

    public bool isHitStun;

    public bool isAppearing;
    private float appearTime;

    public Shader wrath;
    public Shader appear;

    public GameObject deathParticles;

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

        foreach (var child in GetComponentsInChildren<SpriteRenderer>())
        {
            Materials.Add(child.material);
        }


        GameManager.States state = gameManager.GetGameState();
        if (state == GameManager.States.Playing || state == GameManager.States.Start321GO)
            AppearDissolveState();
    }

    // Update is called once per frame
    void Update()
    {
        if (!initForStart321Go)  // TODO : do this with events
        {
            if (gameManager.GetGameState() == GameManager.States.Start321GO)
            {
                levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
                spawnList = levelManager.spawnList;
                levelManager.playersScripts.Add(this);
                initForStart321Go = true;
            }
        }
        if (!initForPlayingDone)
        {
            if (gameManager.GetGameState() == GameManager.States.Playing)
            {
                rb.simulated = true;
                currentState = States.Playing;
                initForPlayingDone = true;
            }
        }
        
        //print(wrathPercentage);
        if (pV.IsMine)
        {
            if (isWrath)
            {
                InWrathState();
                wrathPercentage = (wrathTime / maxWrathTime) * 100;
            }
            else
            {
                InNormalState();
                wrathPercentage = (loadingWrath / maxLoadingWrath) * 100;
            }
        }
        else
            isWrath = localInt == 1;

        if(isAppearing)
        {
            appearTime += Time.deltaTime;
            UpdateFade(appearTime / 3);
            if(appearTime >= 3)
            {
                isAppearing = false;
                ChangeShader(wrath);
            }
        }
    }


    //WRATH MODE
    public void InWrathState()
    {
        if(wrathTime <= 0)
        {
            ReturnToNormalState();
        }
        else
        {
            wrathTime -= Time.deltaTime;
        }
    }

    public void InNormalState()
    {
        if(loadingWrath < maxLoadingWrath)
            loadingWrath += Time.deltaTime;
    }

    public void GoIntoWrathMode()
    {
        if (loadingWrath >= maxLoadingWrath)
        {
            wrathTime = maxWrathTime;
            isWrath = true;
            loadingWrath = 0;
            WrathColor(wrathColor); 
            pV.RPC("WrathSprites", RpcTarget.All, true);
            WrathSprites(true);
        }
    }

    public void ReturnToNormalState()
    {
        isWrath = false;
        loadingWrath = 0;
        WrathColor(Color.white);
        pV.RPC("WrathSprites", RpcTarget.All, false);
        WrathSprites(false);
    }

    public void WrathColor(Color color)
    {
        actualColor = color;
        foreach(SpriteRenderer sprite in spritesToColor)
        {
            sprite.color = color;
        }
    }

    [PunRPC]
    public void WrathSprites(bool enabled)
    {
        foreach(SpriteRenderer sprite in spritesToShow)
        {
            sprite.gameObject.SetActive(enabled);
        }

        foreach (var mat in Materials)
        {
            if(enabled)
                mat?.SetFloat("WrathCoef", 1f);
            else
                mat?.SetFloat("WrathCoef", 0f);
        }
    }



    //REAPPEAR

    public void OnEnterReappear()
    {
        DeathParticles();
        aM.Play("bloodbath");
        isAlive = false;
        lives--;
        CheckStillAlive();
        if (canStillPlay)
        {
            currentState = States.Frozen;
            AppearDissolveState();
            Reappear();
        }
        else
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            if (levelManager.inSolo)
            {
                levelManager.gameObjectIA.SetActive(false);
            }
        }
    }

    public void DeathParticles()
    {
        GameObject deathParticlesGO = PhotonNetwork.Instantiate(deathParticles.name, transform.position, new Quaternion());
        deathParticlesGO.GetComponent<ParticleSystem>().Play();
    }

    private void Reappear()
    {
        currentState = States.Playing;
        rb.velocity = new Vector2(0, 0);
        int randomSpawnPoint = Random.Range(0, spawnList.Count);
        transform.position = spawnList[randomSpawnPoint].position;
        isAlive = true;
        percentage = 0;

        if(isWrath)
        {
            ReturnToNormalState();
        }
        else
        {
            loadingWrath -= 30 / 100 * loadingWrath;    //on perd 30% de la barre de chargement de wrath 
        }

    }

    void CheckStillAlive()
    {
        if(lives <= 0)
        {
            canStillPlay = false;
            _myCustomPropreties["StillInGame"] = false;
            PhotonNetwork.LocalPlayer.CustomProperties = _myCustomPropreties;
        }
    }

    public void AppearDissolveState()
    {
        appearTime = 0;
        isAppearing = true;
        ChangeShader(appear);
    }

    public void ChangeShader(Shader shader)
    {
        foreach(Material mat in Materials)
        {
            mat.shader = shader;
        }
    }

    public void UpdateFade(float fade)
    {
        foreach (Material mat in Materials)
        {
            mat.SetFloat("_Fade", fade);
        }
    }


    //TAKE DAMAGE

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




    //PHOTON

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
