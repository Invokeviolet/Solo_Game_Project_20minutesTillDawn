using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

// UI 매니저 클래스
public class UIManager : MonoBehaviour
{


    //---------------------------------------------------------------------------------------------
    //싱글톤
    #region 싱글턴

    static UIManager instance = null;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    instance = new GameObject("UIManager").AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    //
    //---------------------------------------------------------------------------------------------


    // 게임오버 이후 총 점수 계산을 위한 UI    
    [SerializeField] GameObject GameOverWindow;
    //[SerializeField] GameObject HomeWindow;
    //[SerializeField] GameObject MenuWindow;


    /* [SerializeField] Text TimeScore; // (00:00) -> 시간정보
     [SerializeField] Text TimeScore_Add; // (초)단위 점수 
     [SerializeField] Text MonsterScore; // (몬스터 잡은 수) -> 몬스터 정보
     [SerializeField] Text MonsterScore_Add; // 몬스터 잡은 수
     [SerializeField] Text AllScore; // Earned
     [SerializeField] Text AllScore_Add; // 총 점수*/
    [SerializeField] Text LevelScore; // (레벨) -> 플레이어 정보
    [SerializeField] Text LevelAdd_text; // 레벨 * 10    

    //Exp
    [SerializeField] Slider ExpSlider; // Exp 슬라이더
    [SerializeField] ExpItem expitem;

    //체력바
    [SerializeField] Image[] Heartimage; // 체력 이미지 배열    

    //타이머
    [SerializeField] Text TimeText; // 타이머 텍스트

    //총알
    public float ReloadTime; // 재장전 시간
    [SerializeField] public Slider ReloadSlider; // Reload 슬라이더
    [SerializeField] Text BulletCountText; // 총알 갯수
    [SerializeField] public Text MouseBulletCountText; // 총알 갯수

    private BulletSpawner bulletspawner;
    private BulletObject bulletObject;
    private PlayerController playerInfo;
    private Monster monster;

    float Time_S; // 초 계산
    float Time_M; // 분 계산

    float maxExpValue; // 슬라이더 최댓값

    // 하트 계산을 위한 값
    int curHeart = 0;
    int maxHeart = 4;

    bool isGameOver = false;

    //[SerializeField] Animator reloadBullet; //재장전 애니메이션 가져오기    
    bool isReload = false;
    int LevelUp; // 레벨 계산할 값

    private void Awake()
    {
        //reloadBullet = FindObjectOfType<Animator>();
        //ExpSlider = GetComponent<Slider>();
        playerInfo = FindObjectOfType<PlayerController>();
        bulletObject = FindObjectOfType<BulletObject>();
        bulletspawner = FindObjectOfType<BulletSpawner>();
        //expitem = FindObjectOfType<ExpItem>();
        curHeart = maxHeart;

        Time_M = 20; // 분
        Time_S = 0; // 초

        GameOverWindow.SetActive(false);
        ReloadSlider.gameObject.SetActive(false); // 슬라이더 비활성화
        maxExpValue = 100f; // 경험치 최댓값

        ExpSlider.value = 0f;
        ReloadSlider.value = 0f;

        ReloadTime = 1.0f;

        LevelUp = 1;
    }

    private void Start()
    {
        ReloadSlider.value = 1f; // 재장전 시간은 1부터 0으로 내려감

    }


    public void Update()
    {
        CheckHeart();

        LevelUpdate();

        if (!isGameOver) { BackTime(); }

        else { RestoreTime(); }
    }


    public void bulletCheck() // 불렛갯수가 0이하일때 재장전을 체크하는 곳
    {

        ReloadTime -= Time.deltaTime; // 재장전시간을 점차 줄여나감
        

        ReloadSlider.gameObject.SetActive(true); // 슬라이더 활성화
        ReloadSlider.value = ReloadTime; // 총알의 갯수 체크하는 좌측 상단 UI -> 슬라이더의 시간값도 같이 계산


        if (ReloadTime <= 0f) // 재장전 시간이 0이 되면
        {
            
            ReloadTime = 1; // 리로드 타임을 1로 다시 초기화 
            ReloadSlider.gameObject.SetActive(false); // 리로드 값이 초기화 되었으므로 슬라이더를 다시 비활성화

            if (bulletspawner.Curbullet <= 0) // 남은 총알이 0이거나 음수일때 재장전시간 이후에 총알 생성
            {
                bulletspawner.Curbullet = bulletspawner.Maxbullet; // 총알 스포너의 총알 갯수를 최대 총알 갯수로 초기화
            }
            
        }

    }

    public void BulletCount(int curCount) // 총알의 갯수 체크하는 좌측 상단 UI
    {

        BulletCountText.text = ("00" + curCount + "/" + "00" + bulletspawner.Maxbullet); // 현재 총알 불렛 갯수 / 최대 총알 갯수를 텍스트로 표시

    }
    public void MouseBulletCount(int curCount) // 총알의 갯수 체크하는 좌측 상단 UI
    {

        MouseBulletCountText.text = "" + curCount; //불렛 갯수가 텍스트로 표시

    }

    public void ExpUpdate(float ExpValue) // 경험치 + 해주는 함수
    {
        ExpSlider.value += ExpValue; // 슬라이더의 값을 점차적으로 더해준다.
    }


    public void LevelUpdate() // 레벨업 받기 위한 값
    {
        LevelAdd_text.text = "" + LevelUp; // 레벨 텍스트 = 레벨 1
        if (ExpSlider.value >= maxExpValue) // 레벨 슬라이더 값이 최대 경험치 값보다 커지면 레벨 업
        {            
            //Debug.Log("Level : " + LevelAdd_text.text);
            ++LevelUp; //레벨업
            ExpSlider.value = 0f;
        }
    }

    
    public void CheckHeart() // 플레이어가 데미지 입었을때 계산되는 하트 갯수
    {
        curHeart = playerInfo.curHp;

        if (curHeart <= 3)
        {
            Heartimage[curHeart].fillAmount = 0;
        }

    }

    public void BackTime() // 오른쪽 상단 UI 타이머 
    {
        if (Time_S <= 0 && Time_M <= 0)
        {

            TimeText.text = "00:00";
            isGameOver = true;
        }
        if (!isGameOver)
        {
            Time_S -= 1f * Time.deltaTime;

            if (Time_S * Time.deltaTime <= 0f)
            {
                Time_S += 60f;
                Time_M -= 1f;
            }

            if ((Time_M < 10f) && (Time_S < 10f))
            {
                TimeText.text = "0" + (int)Time_M + ":" + "0" + (int)Time_S;
            }

            if ((Time_M < 10f) && (Time_S > 10f))
            {
                TimeText.text = "0" + (int)Time_M + ":" + (int)Time_S;
            }
            if ((Time_M > 10f) && (Time_S > 10f))
            {
                TimeText.text = (int)Time_M + ":" + (int)Time_S;
            }
            if ((Time_M > 10f) && (Time_S < 10f))
            {
                TimeText.text = (int)Time_M + ":" + "0" + (int)Time_S;
            }
        }

    }
    void RestoreTime()
    {

        TimeText.text = "00" + ":" + "00";

    }

    public void GameOver()
    {
        GameOverWindow.SetActive(true);

        Time.timeScale = 0;


        //점수 계산 

        //최종 마지막에 기록된 시간(초)
        //최종 몬스터 잡은 수
        //최종 레벨 *10
        //--------------------------
        //총 점수
    }


}
