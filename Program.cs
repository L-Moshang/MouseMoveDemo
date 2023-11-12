using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using Timer = System.Timers.Timer;


namespace MouseMoveDemo
{
    class Program
    {
        static int MOUSEEVENTF_MOVE = 0x0001; //模拟鼠标移动 
        /*
        static int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下 
        static int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起 
        static int MOUSEEVENTF_ABSOLUTE = 0x8000; //鼠标绝对位置 
        static int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
        static int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
        static int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
        static int MOUSEEVENTF_MIDDLEUP = 0x0040; //模拟鼠标中键抬起
        */
        [DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        static void Main(string[] args)
        {
            try
            {
                int main_id_sum = 0;
                Process[] procs = Process.GetProcesses();
                for (int i = 0; i < procs.Length; i++)
                {
                    if (procs[i].ProcessName.ToString() == "MouseMoveDemo") main_id_sum++;
                }
                if (main_id_sum >= 2)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
            }
            string timerInterval = "10";
            //Console.WriteLine("Input the Timer Interval by second : ");
            //timerInterval = Console.ReadLine();
            Console.WriteLine("Input succeeded({0}).Executing", timerInterval);
            int TimerInterval;
            if (Int32.TryParse(timerInterval,out TimerInterval))
            {
                Timer timer = new Timer();
                timer.Enabled = true;
                timer.Interval = 1000 * TimerInterval;
                timer.Elapsed += MoveMouseHandler;
            }

            ConsoleHelper.hideConsole(Console.Title);
            Console.ReadKey();
        }

        protected static void MoveMouseHandler(object sender, ElapsedEventArgs e)
        {
            mouse_event(MOUSEEVENTF_MOVE, 1, 1, 0, 0);
            mouse_event(MOUSEEVENTF_MOVE, -1, -1, 0, 0);
            //if (DateTime.Now.Hour >= 19 && DateTime.Now.Minute >= 30)
            //    Environment.Exit(0);
        }
    }
}
