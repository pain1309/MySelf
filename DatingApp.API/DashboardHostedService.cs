using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DatingApp.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace DatingApp.API
{
    // class này sẽ được start hoặc stop bởi Host một khi chúng ta đăng ký nó trong Startup
    // nó khởi tạo một Timer và ghi lại thời gian mỗi phút, mục đích để biết nó đang chạy
    public class DashboardHostedService : IHostedService
    {
        private Timer _timer;
        private readonly IHubContext<NotificationHub> _hubContext;
        public DashboardHostedService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(2));
 
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _hubContext.Clients.All.SendAsync("SendMessage", 
                new {
                    val1 = getRandomString(),
                    val2 = getRandomString(),
                    val3 = getRandomString(),
                    val4 = getRandomString()
                });
        }
        
        private string getRandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, random.Next(10, 16))
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
 
            return Task.CompletedTask;
        }
    }
}