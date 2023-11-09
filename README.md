# dotnetconfmini2209
닷넷콘 미니 2209 준비 (그레이프시티 GCExcel, 라즈베리파이 x Avalonia UI 11)
'백-엔드 | Back-end Excel 서비스 만들기(GcExcel)' 영상: https://youtu.be/Z6Z3qgHYaOg?t=12959
'AvaloniaUI + Raspberry Pi => 전광판 완성!' 영상: https://youtu.be/Z6Z3qgHYaOg?t=14459

1. 라즈베리파이4 + Avalonia UI
- 라즈베리파이4가 필요함.
- 엘레파츠에서 스타트 패키지로 구매했음 (17만원 + 세금 별도)
- 동봉된 SD 카드에 Windows 에서 Raspbian (라즈베리파이4 OS) 의 64bit 운영체제 설치
- 설치 시 톱니바퀴의 설정에서 아래의 것들을 설정할 것
  1. hostname
  2. SSH 사용체크 (비밀번호 인증 사용)
  3. 사용자 이름 및 비밀번호 설정 (사용자 이름 : pi / 비밀번호 : raspberry) 아무거나 해도 상관없지만 국룰은 따른다.
  4. wifi 설정 (나중에 해도 되긴하는데 지금하는 게 편함, SSID는 wifi의 이름이고, 비밀번호는 맞는거 치면 됨. 보통 공유기에 붙어있음)
  5. Locale 한국으로 설정
- 라즈베리파이4를 부팅하고 처음 할 것
  1. 내장된 블루투스를 켜고 무선 마우스와 무선 키보드 등록(물론 유선 기기로 입출력하거나 전용 Receiver를 사용해도 됨)
  2. wifi 기능이 내장되어 있으므로 라즈비언을 업데이트 (매우 오래걸림)
  3. wifi를 통해 [한글 폰트](https://dbjina.tistory.com/60) 및 [한글 입력기 설치](https://fishpoint.tistory.com/7249) (2번의 업데이트 중일 때는 apt-get이 동작하지 않음, 재부팅 필요)
- https://docs.microsoft.com/ko-kr/dotnet/iot/deployment 문서를 통해 라즈베리안에 .NET 6 설치 (정말 MSDN에 있는 그대로 따라하면 됨. 어디에 칠지 모르겠는 명령어는 모두 터미널에 입력하면 알아서 설치 다 해줌.)
- 아마 지금까지 부팅/재부팅을 하면서 계속 경고 창이 떴을 텐데, 비밀번호를 재설정해주면 없어진다.
- 그리고 windows에서 라즈베리파이로 ssh 붙는 키를 까먹었기 때문에 라즈비언에서 SSH를 열어줘도 접속하지 못한다. [이거](https://elbruno.com/2020/01/27/raspberrypi-how-to-solve-the-ssh-warning-warning-remote-host-identification-has-changed/)보고 해결한다. powershell에 고대로 치면 된다.
- 왜 인지 모르겠지만 Windows 10의 Powershell로 raspbian에 hostname으로 연결이 안된다. ssh pi@raspberry 가 안된다는 말... 결국 ssh pi@192.168.219.105 IP로 접속해서 raspbian에 설정한 비밀번호를 입력하고 접속 성공했다.
- Windows 에서 Avalonia UI 11 Helloworld App을 빌드하여 게시한 뒤, 해당 폴더에 가서 Powershell을 열고 SCP 명령어를 통해 raspbian으로 .NET App을 복사한다.
```scp -r ./* pi@192.168.219.105:/home/pi/{내가만든 폴더}/{닷넷앱배포경로}```

---
20220906 13:56:40

위의 방법으로 진행했지만, UI가 있는 .NET APP을 실행할 수 없었다.
찾아보니 역시 [Raspbian에서 Avalonia UI App을 실행하는 공식문서](https://docs.avaloniaui.net/guides/deep-dives/running-your-app-on-a-raspberry-pi)가 존재했다.
이대로 진행하는 과정에서도 이 문서에서는 OS를 Raspbian Lite를 설치하라고 했는데 나는 64bit OS를 설치했고, [이 오류](https://damedame.tistory.com/entry/libzso1-%ED%8C%8C%EC%9D%BC%EC%9D%84-%EC%B0%BE%EC%9D%84%EC%88%98-%EC%97%86%EC%9D%84%EB%95%8C) 와 [이 오류](https://stackoverflow.com/questions/11471722/libstdc-so-6-cannot-open-shared-object-file-no-such-file-or-directory) 를 지속적으로 겪었다.
결국 실행에 실패했고 다시 문서가 시키는대로 32bit Lite Raspbian OS를 설치하러 Rollback 했다.

---
20220908

Raspbian 에서 Avalonia UI로 만든 전광판 프로그램 완성
Raspbian은 64bit로 재설치했고, 한글폰트만 설치해서 한글이 출력되게만 한다음, 한글 입력기는 설치 안했고, Locale도 영문으로 유지했다.
그랬더니 더블클릭해서는 역시 실행이 안되지만 터미널에서 실행하면 문제없이 실행된다.

GrapeCity에서 샘플로 수령한 GcExcel vs Aspose vs SpreadsheetGear 성능 측정 프로그램이 .NET Core 2.1 이라 .NET 6로 변경하고 Nuget 패키지들도 업데이트했다.
Windows와 Raspbian에서 모두 테스트했고 GcExcel이 파일을 읽어 들이는데에 있어 압도적으로 빨랐다. 또한 메모리 효율이 매우 높아서 연산속도는 조금 더뎠지만 메모리 할당을 거의 하지 않고 Excel 연산을 수행했다.
그렇게 Raspbian에서 계산한 Excel 성능 측청 문서는 scp를 통해 문서로 Windows 10으로 전송해서 테스트했다.
우선 Windows 10 에서 SSH를 이용해본 적이 없었는데 OpenSSH가 기본적으로 설치되어 있었고, [이 영상](https://www.youtube.com/watch?v=OwrBOAKXa8c)을 보고 서버만 활성화 시켰다.
이후 ssh를 접속할 때 PC 이름으로 접속하는 것으로 착각하여 장기간 삽질 후, [검색을 통해](https://superuser.com/questions/1661724/how-do-i-find-my-username-and-servername-for-windows-ssh-server) PC이름이 아닌 powershell에서 ```$env:USERNAME``` 으로 확인가능한 것이 내 ssh ID라고 알게되었다.
그 ID를 보니까 비밀번호가 유추가 되어서 잘 때려맞췄다...(내 시간 ㅠㅠ)


[참고문서]
- https://www.raspberrypi.com/documentation/computers/remote-access.html#using-secure-copy
- https://www.raspberrypi.com/documentation/computers/remote-access.html#ip-address
- https://www.raspberrypi.com/documentation/computers/remote-access.html#ssh
- https://docs.microsoft.com/ko-kr/dotnet/iot/debugging?source=recommendations&tabs=self-contained&pivots=visualstudio
- https://docs.microsoft.com/ko-kr/visualstudio/debugger/remote-debugging-dotnet-core-linux-with-ssh?view=vs-2022
- https://docs.microsoft.com/ko-kr/dotnet/iot/deployment

여담)
https://tutorials-raspberrypi.com/raspberry-pi-default-login-password/
위 문서에 따르면 라즈비언 OS 이미지를 SD 카드에 구울 때 기본 SSH 아이디는 pi, 비밀번호는 raspberry 라고 한다.
물론 Imager를 통해 설정해서 OS를 만들 수도 있다.
그런데 ssh를 raspberry로 연결하면 access denied 가 발생한다.
https://stackoverflow.com/questions/71804429/raspberry-pi-ssh-access-denied
위 링크를 읽어보면 더 이상 사용되지 않는다고 하며 반드시 OS 이미지를 만들 때 설정해서 이미지를 만들어야 한다고 한다.
