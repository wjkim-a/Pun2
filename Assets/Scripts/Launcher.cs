using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;

//네트워크에 적합하게끔 뜯어고침

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI text;
    // string gameVersion = "1";//게임 킬 때 , 버전 안맞으면 못키게 해야하니깐 지정. 버전 같아야 통신 가능하게 하려고 정의

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;//방장이 씬을 바꾸면 다른 모든 클라들도 씬이 같이 바뀌는 옵션
        //이 파일은 모든 파일에 싹 다 들어있다.
    }


    public void ConnectedToServer()
    { 
        PhotonNetwork.ConnectUsingSettings();   //우리가 스크립터블 오브젝트에 적어둔 앱아이디 정보들이 담긴 내용으로 서버 연결 시도
    }
    public override void OnConnectedToMaster()
    {
        text.text += "\nConnect Success";
        Debug.Log("펀 마스터 연결 성공");
        PhotonNetwork.JoinLobby();
    }

    //로비 참여 완료시 알아서 호출되는 기능, 룸 들어가기 전레 로비(룸) 거치는 건 필수가 아님
    public override void OnJoinedLobby()
    {
        Debug.Log("로비에 들어왔습니다.");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinRandomOrCreateRoom(null, 0, MatchmakingMode.FillRoom, null, null, "test", roomOptions);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //랜덤 방 진입 실패
        Debug.Log(returnCode + message);
        //혹은 팝업 띄운다던지
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
        //크리에이트 룸 하게 되면, 방 만들고 방장이 되어버림
    }

    public override void OnJoinedRoom()
    {
        text.text += "\nName: " + PhotonNetwork.NickName;
        Debug.Log("방에 들어왔습니다.");
        Debug.Log("닉네임 : " + PhotonNetwork.NickName);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        text.text += newPlayer.NickName + "is Entered Room.";
        Debug.Log(newPlayer.NickName + "님이 방에 들어왔습니다.");
    }
}
