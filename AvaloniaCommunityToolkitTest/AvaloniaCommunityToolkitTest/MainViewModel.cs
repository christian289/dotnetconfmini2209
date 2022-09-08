using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvaloniaCommunityToolkitTest
{
    // Avalonia UI 11 Preview를 사용하면 에러난다;;

    [ObservableObject]
    public partial class MainViewModel
    {
        PeriodicTimer myTimer;
        Task timerTask;

        [ObservableProperty]
        public string text; // Text 프로퍼티 생성

        [ObservableProperty]
        public byte hour;
        
        [ObservableProperty]
        public byte minute;
        
        [ObservableProperty]
        public byte second;

        [ObservableProperty]
        public long clickCounter;

        public MainViewModel()
        {
            Text = "Test";
            myTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            timerTask = Task.Run(async () =>
            {
                while (await myTimer.WaitForNextTickAsync())
                {
                    Hour = (byte)DateTime.Now.Hour;
                    Minute = (byte)DateTime.Now.Minute;
                    Second = (byte)DateTime.Now.Second;
                }
            });
        }

        [RelayCommand]
        public void Base() // BaseCommand로 동작
        {
            ClickCounter++;
        }

        partial void OnTextChanged(string value) // 소스제네레이터 생성
        {
            
        }

        partial void OnTextChanging(string value) // 소스제네레이터 생성
        {
            
        }
    }
}
