using UnityEngine;

public class Instantiator : MonoBehaviour
{ public GameObject SFX;

    public void playSFX(){
        Instantiate(SFX);
    }



}
