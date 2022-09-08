using System.Text;

namespace AvaloniaUIDashboard.ViewModels
{
    // ObservableObjectAttribute 를 이용하면 ViewLocator.Match() 에서 실패한다...왜?
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            StringBuilder noticeSb = new();
            noticeSb.AppendLine("1. 어~? 금지");
            noticeSb.AppendLine("2. 아~! 금지");
            noticeSb.AppendLine("3. 헐... 금지");
            noticeSb.AppendLine("4. WTF 금지");
            noticeSb.AppendLine("5. Aㅏ 금지");
            NoticeStr = noticeSb.ToString();
            startTime = DateTime.Now;
            clockTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            timerTask = Task.Run(async () =>
            {
                while (await clockTimer.WaitForNextTickAsync())
                {
                    CurrentYear = (short)DateTime.Now.Year;
                    CurrentMonth = (short)DateTime.Now.Month;
                    CurrentDay = (short)DateTime.Now.Day;
                    CurrentHour = (short)DateTime.Now.Hour;
                    CurrentMinute = (short)DateTime.Now.Minute;
                    CurrentSecond = (short)DateTime.Now.Second;

                    TimeSpan diff = DateTime.Now - startTime;
                    DueTime = new DateTime(diff.Ticks);
                }
            });
        }

        PeriodicTimer clockTimer;
        Task timerTask;
        DateTime startTime;

        [ObservableProperty]
        DateTime dueTime;

        [ObservableProperty]
        private short currentYear;

        [ObservableProperty]
        private short currentMonth;

        [ObservableProperty]
        private short currentDay;

        [ObservableProperty]
        public short currentHour;

        [ObservableProperty]
        public short currentMinute;

        [ObservableProperty]
        public short currentSecond;

        [ObservableProperty]
        public string noticeStr = default!;
    }
}
