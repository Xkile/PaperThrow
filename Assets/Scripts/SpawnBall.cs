using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Experimental.Playables;
using System.Threading;
//using System.Numerics;

public class SpawnBall : MonoBehaviour
{
    [SerializeField]
	GameObject ball;
    [SerializeField]
	Text ScoreText;
    [SerializeField]
    Collider TargetColl;
    [SerializeField]
    GameObject ParticleShow;
    //[SerializeField]
   // GameObject ballParent;

   public bool SwipeOn;
   public Button SpawnButton;
   public int score;
   public static SpawnBall instance;
   public GameObject pointsAdd;
   public Text highScore;
   public Text timer;
   public float seconds = 20;
    public float miliseconds = 0;
    public bool startGame;
    public GameObject TimerImage;
    public bool oneTime;

   private void Awake() {
       highScore.text = PlayerPrefs.GetInt("HighScore",0).ToString();
       instance = this;
       score = 0;
       SwipeOn = true;
   }

   void Update(){
       print(pointsAdd.transform.position);
      /* if(startGame){
           if (oneTime){
               oneTime = false;
               TimerImage.transform.DOScale(new Vector3(0.026f,0.026f,0.026f),1f);
           }
        if(miliseconds <= 0){
           
           
        if(seconds <= (0)){
            startGame = false;
            GameOver();
        }
        else if(seconds >= 0){
            seconds--;
        }
        miliseconds = 100;
        }
    miliseconds -= Time.deltaTime * 100;
    timer.text = string.Format("{0}:{1}", seconds, (int)miliseconds);
    }*/
    }
   

   public void SwipeEnable(){
       SwipeOn = true;
   }

     public void SwipeDisable(){
       SwipeOn = false;
   }
    private GameObject newBall;

	public void Spawn()
	{
        print("Spawning ball");
        TargetColl.enabled = true;
        SwipeOn = true;
		newBall  = Instantiate (ball, new Vector3(-0.83f, 0.885f, 3.01f), Quaternion.identity);
        newBall.transform.rotation = Quaternion.Euler(-90,0,0);
       // newBall.transform.SetParent(ballParent.transform);
        //newBall.transform.localScale = new Vector3 (0.05f,0.05f,0.05f);

	}

    public void InceaseScore(){
        ParticleShow.SetActive(true);
        TargetColl.enabled = false;
        pointsAdd.transform.localScale = new Vector3(1,1,1);
        pointsAdd.transform.DOMove(new Vector3(900, 700, 0),2f);
        pointsAdd.transform.DOScale(new Vector3(0, 0, 0),2f).OnComplete(resetPos);
        //pointsAdd.transform.position = new Vector3(971, 400, 0);
        score++;
        ScoreText.text = ""+score;
        if (score> PlayerPrefs.GetInt("HighScore",0)){
            PlayerPrefs.SetInt("HighScore", score);
        }
        
    }

    void resetPos(){
        pointsAdd.transform.position = new Vector3(971, 400, 0);
        ParticleShow.SetActive(false);
    }

    void GameOver(){

    }

    public void Quit(){
        #if !UNITY_EDITOR
        Application.Quit();
        #else
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
