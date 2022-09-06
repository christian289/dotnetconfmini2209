# dotnetconfmini2209
닷넷콘 미니 2209 준비 (그레이프시티 GCExcel, 라즈베리파이 x Avalonia UI 11)

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
  3. wifi를 통해 [한글 폰트 및 한글 입력기 설치](https://stackoverflow.com/questions/71804429/raspberry-pi-ssh-access-denied) (2번의 업데이트 중일 때는 apt-get이 동작하지 않음, 재부팅 필요)
- https://docs.microsoft.com/ko-kr/dotnet/iot/deployment 문서를 통해 라즈베리안에 .NET 6 설치 (정말 MSDN에 있는 그대로 따라하면 됨. 어디에 칠지 모르겠는 명령어는 모두 터미널에 입력하면 알아서 설치 다 해줌.)
- 아마 지금까지 부팅/재부팅을 하면서 계속 경고 창이 떴을 텐데, 비밀번호를 재설정해주면 없어진다.
- 그리고 windows에서 라즈베리파이로 ssh 붙는 키를 까먹었기 때문에 라즈비언에서 SSH를 열어줘도 접속하지 못한다. [이거](https://elbruno.com/2020/01/27/raspberrypi-how-to-solve-the-ssh-warning-warning-remote-host-identification-has-changed/)보고 해결한다. powershell에 고대로 치면 된다.
- 왜 인지 모르겠지만 Windows 10의 Powershell로 raspbian에 hostname으로 연결이 안된다. ssh pi@raspberry 가 안된다는 말... 결국 ssh pi@192.168.219.105 IP로 접속해서 raspbian에 설정한 비밀번호를 입력하고 접속 성공했다.
- Windows 에서 Avalonia UI 11 Helloworld App을 빌드하여 게시한 뒤, 해당 폴더에 가서 Powershell을 열고 SCP 명령어를 통해 raspbian으로 .NET App을 복사한다.
```scp -r ./* pi@192.168.219.105:/home/pi/{내가만든 폴더}/{닷넷앱배포경로}```


[Next Step]
- Avalonia UI 11로 UI 앱을 구성해야 함. (Reactive UI는 어떻게할지 선택해야 함.)

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
