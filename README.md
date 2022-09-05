# dotnetconfmini2209
닷넷콘 미니 2209 준비 (그레이프시티 GCExcel, 라즈베리파이 x Avalonia UI 11)

1. 라즈베리파이4 + Avalonia UI
- 라즈베리파이4가 필요함.
- 엘레파츠에서 스타트 패키지로 구매했음 (17만원 + 세금 별도)
- 동봉된 SD 카드에 Windows 에서 라즈베리안 (라즈베리파이4 OS) 의 64bit 운영체제 설치
- 라즈베리파이4 에는 블루투스와 와이파이 기능이 내장되어 있으므로 무선 키보드 & 무선 마우스를 연결하고 라즈베리안을 업데이트 한 다음 한글 폰트 및 한글 입력기 설치
- https://docs.microsoft.com/ko-kr/dotnet/iot/deployment 문서를 통해 라즈베리안에 .NET 6 설치

- 이제 SSH 환경으로 원격 디버깅환경을 구성해야함.
- Avalonia UI 11로 UI 앱을 구성해야 함. (Reactive UI는 어떻게할지 선택해야 함.)
