using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

namespace Relay.TestChat
{
    public partial class MainWindow : Window
    {
        HubConnection connection;  // подключение для взаимодействия с хабом
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Connect()
        {
            try
            {
                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl(serverAdress.Text)
                    .WithAutomaticReconnect(new[] { TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20) })
                    .WithServerTimeout(TimeSpan.FromMinutes(5))
                    .Build();

                connection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        var newMessage = $"{user}: {message}";
                        chatbox.Items.Insert(0, newMessage);
                    });
                });

                connection.Closed += async (error) =>
                {
                    chatbox.Items.Add("Connection closed.");
                    await connection.StartAsync();
                };

                // подключемся к хабу
                await connection.StartAsync();
                chatbox.Items.Add("Вы вошли в чат");
                reconect.IsEnabled = false;
                sendBtn.IsEnabled = true;
            }
            catch (Exception ex)
            {
                reconect.IsEnabled = true;
                chatbox.Items.Add(ex.Message);
            }
        }

        // обработчик загрузки окна
        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Connect();
            }
            catch (Exception ex)
            {
                reconect.IsEnabled = true;
                chatbox.Items.Add(ex.Message);
            }
        }

        // обработчик нажатия на кнопку
        private async void ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // отправка сообщения
                await connection.InvokeAsync("SendMessage", userTextBox.Text, messageTextBox.Text);
            }
            catch (Exception ex)
            {
                chatbox.Items.Add(ex.Message);
            }
        }

        private void ReconnectButton(object sender, RoutedEventArgs e) => Connect();

        private async void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await connection.InvokeAsync("SendMessage", "", $"Пользователь {userTextBox.Text} выходит из чата");
            await connection.StopAsync();   // отключение от хаба
        }
    }
}