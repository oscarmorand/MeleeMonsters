using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SyncChara : MonoBehaviour, IPunObservable
{

    public Text _text;
    public List<GameObject> sprites;
    public int spriteInt;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(_text.text);
            stream.SendNext(spriteInt);
        }
        else
        {
            _text.text = (string)stream.ReceiveNext();
            spriteInt = (int)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            if (i == spriteInt)
                sprites[i].SetActive(true);
            else
                sprites[i].SetActive(false);
        }
    }
}
