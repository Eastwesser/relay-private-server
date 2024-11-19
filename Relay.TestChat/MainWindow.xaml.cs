using System;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

namespace Relay.TestChat
{
    public partial class MainWindow : Window
    {
        private HubConnection? connection;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Connect()
        {
            try
            {
                if (connection == null)
                {
                    // Устанавливаем подключение к SignalR Hub
                    connection = new HubConnectionBuilder()
                        .WithUrl(serverAdress.Text)
                        .WithAutomaticReconnect(new[] { TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20) })
                        .WithServerTimeout(TimeSpan.FromMinutes(5))
                        .Build();

                    // Настраиваем обработку входящих сообщений
                    connection.On<string, string>("ReceiveMessage", (user, message) =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            chatbox.Items.Insert(0, $"{user}: {message}");
                        });
                    });

                    // Действие при закрытии подключения
                    connection.Closed += async (error) =>
                    {
                        Dispatcher.Invoke(() => chatbox.Items.Add("Подключение закрыто."));
                        await connection.StartAsync();
                    };
                }

                await connection.StartAsync();
                chatbox.Items.Add("Вы вошли в чат.");
                reconect.IsEnabled = false;
                sendBtn.IsEnabled = true;
            }
            catch (Exception ex)
            {
                reconect.IsEnabled = true;
                chatbox.Items.Add($"Ошибка: {ex.Message}");
            }
        }

        private void ReconnectButton(object sender, RoutedEventArgs e) => Connect();

        private async void ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (connection != null)
                {
                    await connection.InvokeAsync("SendMessage", userTextBox.Text, messageTextBox.Text);
                    messageTextBox.Clear(); // Очищаем поле ввода после отправки
                }
            }
            catch (Exception ex)
            {
                chatbox.Items.Add($"Ошибка отправки: {ex.Message}");
            }
        }

        private async void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (connection != null)
            {
                await connection.InvokeAsync("SendMessage", "", $"Пользователь {userTextBox.Text} выходит из чата");
                await connection.StopAsync();
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e) => Connect();
    }
}
