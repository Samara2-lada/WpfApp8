using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
namespace RaceWPF
{

    public partial class MainWindow : Window
    {
        private ObservableCollection<Participant> participants = new ObservableCollection<Participant>();

        public MainWindow()
        {
            InitializeComponent();
            ParticipantsGrid.ItemsSource = participants;
        }
        private void LoadDefaultBtn_Click(object sender, RoutedEventArgs e)
        {
            participants.Clear();
            participants.Add(new Participant("Иванов Иван Иванович", 1, 12.45));
            participants.Add(new Participant("Петров Пётр Петрович", 2, 11.89));
            participants.Add(new Participant("Сидоров Алексей Сергеевич", 3, 13.02));
            participants.Add(new Participant("Кузнецов Дмитрий Олегович", 4, 12.11));
            participants.Add(new Participant("Смирнов Андрей Викторович", 5, 11.54));
            participants.Add(new Participant("Волков Никита Андреевич", 6, 12.78));
            participants.Add(new Participant("Морозов Егор Дмитриевич", 7, 13.45));
            participants.Add(new Participant("Соколов Максим Игоревич", 8, 12.33));
            UpdateStatistics();
        }
        private void ShowAddPanelBtn_Click(object sender, RoutedEventArgs e)
        {
            // Показываем панель и очищаем поля
            AddPanel.Visibility = Visibility.Visible;
            NameBox.Text = "";
            NumberBox.Text = "";
            ResultBox.Text = "";
            NameBox.Focus();
        }
        private void CancelAdd_Click(object sender, RoutedEventArgs e)
        {
            AddPanel.Visibility = Visibility.Collapsed;
        }
        private void SaveNewParticipant_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите ФИО участника.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(NumberBox.Text, out int number) || number <= 0)
            {
                MessageBox.Show("Номер должен быть положительным целым числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string resultText = ResultBox.Text.Replace(',', '.');
            if (!double.TryParse(resultText, NumberStyles.Float, CultureInfo.InvariantCulture, out double result) || result <= 0)
            {
                MessageBox.Show("Результат должен быть положительным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Добавление участника
            participants.Add(new Participant(NameBox.Text.Trim(), number, result));
            UpdateStatistics();
            AddPanel.Visibility = Visibility.Collapsed; // Скрываем панель обратно
        }
        //Очистка поля с даннымми
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            participants.Clear();
            AverageText.Text = "—";
            BestText.Text = "—";
        }
        // Изменение статистики
        private void UpdateStatistics()
        {
            if (participants.Count == 0)
            {
                AverageText.Text = "—";
                BestText.Text = "—";
                return;
            }
            double average = participants.Average(p => p.Result);
            AverageText.Text = average.ToString("F2");

            var best = participants.OrderBy(p => p.Result).First();
            BestText.Text = $"{best.Result:F2} сек — {best.FullName} (№{best.Number})";
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("3. Заполнить таблицу участников забега на 100 метров на 8 человек с полями: ФИО, \r\n " +
                "номер, результат. Вывести результат на экран. Вывести средний результат. ");
        }
    }
}