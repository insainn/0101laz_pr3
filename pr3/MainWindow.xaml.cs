using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace pr3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Classes.PersonInfo> Enemys = new List<Classes.PersonInfo>();
        public Classes.PersonInfo Player = new Classes.PersonInfo("Student", 100, 10, 1, 0, 0, 5);
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public Classes.PersonInfo Enemy;
        public MainWindow()
        {
            InitializeComponent();
            UserInfoPlayer();
            // Добавляем данные о противниках в коллекцию
            Enemys.Add(new Classes.PersonInfo("Название врага №1", 100, 20, 1, 15, 5, 20));
            Enemys.Add(new Classes.PersonInfo("Название врага №2", 20, 5, 1, 5, 2, 5));
            Enemys.Add(new Classes.PersonInfo("Название врага №3", 50, 3, 1, 10, 10, 15));

            // Задаём настройки для таймера
            dispatcherTimer.Tick += AttackPlayer;
            // Задаём интервал с которым выполняется таймер
            dispatcherTimer.Interval = new System.TimeSpan(0, 0, 10);
            // Запускаем таймер
            dispatcherTimer.Start();
            SelectEnemy();
        }
        public void SelectEnemy()
        {
            // Выбираем случайный индекс противника
            int Id = new Random().Next(0, Enemys.Count);
            // Создаём экземпляр с данными противника
            Enemy = new Classes.PersonInfo(
                Enemys[Id].Name,
                Enemys[Id].Health,
                Enemys[Id].Armor,
                Enemys[Id].Level,
                Enemys[Id].Glasses,
                Enemys[Id].Money,
                Enemys[Id].Damage);
        }
        private void AttackPlayer(object sender, System.EventArgs e)
        {
            // Наносим урон в процентном соотношении имеющейся броня
            Player.Health -= Convert.ToInt32(Enemy.Damage * 100f / (100f - Player.Armor));
            // Обновляем характеристики персонажа
            UserInfoPlayer();
        }
        /// <summary> Повышение уровня и обновление данных на UI
        public void UserInfoPlayer()
        {
            // Если уровень персонажа больше чем 100 * уровень персонажа
            if (Player.Glasses > 100 * Player.Level)
            {
                // Увеличиваем уровень на 1
                Player.Level++;
                // Обновляем очки уровня
                Player.Glasses = 0;
                // Увеличиваем здоровье на 100
                Player.Health += 100;
                // Увеличиваем урон на 1
                Player.Damage++;
                // Увеличиваем броню на 1
                Player.Armor++;
            }
            // выводим данные на экран
            playerHealth.Content = "Жизненные показатели: " + Player.Health;
            playerArmor.Content = "Броня: " + Player.Armor;
            playerLevel.Content = "Уровень: " + Player.Level;
            playerGlasses.Content = "Опыт: " + Player.Glasses;
            playerMoney.Content = "Монеты: " + Player.Money;
        }
        private void AttackEnemy(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Наносим урон в процентном соотношении имеющейся брона
            Enemy.Health -= Convert.ToInt32(Player.Damage * 100f / (100f - Enemy.Armor));
            // Если жизненные показатели меньше или равны 0
            if (Enemy.Health <= 0)
            {
                // Увеличиваем очки персонажа
                Player.Glasses += Enemy.Glasses;
                // Увеличиваем монеты персонажа
                Player.Money += Enemy.Money;
                // Обновляем информацию на UI
                UserInfoPlayer();
                // Выбираем нового противника
                SelectEnemy();
            }
            else
            {
                // Обновляем UI персонажа
                emptyHealth.Content = "Жизненные показатели: " + Enemy.Health;
                emptyArmor.Content = "Броня: " + Enemy.Armor;
            }
        }
    }
}
