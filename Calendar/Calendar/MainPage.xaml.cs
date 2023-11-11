using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calendar
{
    public partial class MainPage : ContentPage
    {
        public int[] Days { get; set; }
        public int Day = DateTime.Now.Day;
        public int Month = DateTime.Now.Month;
        public int Year = DateTime.Now.Year;
        public static string DOW = DateTime.Now.DayOfWeek.ToString();
        public int numOfDayOfWeek = GetNumOfDayOfWeek(DOW);

        // создадим экземпляры для возврата на текущий месяц, которые не будут больше нигде использоваться
        public int ThisDay = DateTime.Now.Day;
        public int ThisMonth = DateTime.Now.Month;
        public int ThisYear = DateTime.Now.Year;
        public static string ThisDOW = DateTime.Now.DayOfWeek.ToString();
        public int ThisDayOfWeek = GetNumOfDayOfWeek(ThisDOW);

        public MainPage()
        {
            InitializeComponent();
            CurrentDate.Text = $"{GetMonthOfNum(Month)} {Year}";                // выводим месяц и год на экран 
            
            int numOfFirstDayOfWeek = GetThisMonthFirstDayDOW(numOfDayOfWeek);  // получаем порядковый номер дня недели первого числа
            int daysCount = CountDays(Month, Year);                             // получаем количество дней в месяце
            Days = MakeMass(daysCount);                                         // получаем массив с днями месяца (от 1 до 28,29,30,31)
            ShowFullMonth(Days, numOfFirstDayOfWeek);                           // выводим дни месяца на экран
        }

        public int GetThisMonthFirstDayDOW(int CurrDOW) // приимает сегодняшний день недели
        { 
            int firstDOW;
            int first = 1;                      // первое число месяца
            int today = DateTime.Now.Day;       // сегодняшнее число
            
            while (first < today)
            {
                first += 7;
            }
            firstDOW = (CurrDOW + (first - today)) % 7;
            numOfDayOfWeek = firstDOW;
            return firstDOW;
        }

        public static int GetNumOfDayOfWeek(string CurrDayOfWeek)
        {
            // получить день недели первого числа
            // CurrDayOfWeek - день недели текущего числа

            int CurrDOW;
            switch (CurrDayOfWeek)
            {
                case "Monday":
                    CurrDOW = 1;
                    break;
                case "Tuesday":
                    CurrDOW = 2;
                    break;
                case "Wednesday":
                    CurrDOW = 3;
                    break;
                case "Thursday":
                    CurrDOW = 4;
                    break;
                case "Friday":
                    CurrDOW = 5;
                    break; ;
                case "Saturday":
                    CurrDOW = 6;
                    break;
                default:
                    CurrDOW = 7;
                    break;
            }
            

            return CurrDOW;

        }     // Получаем порядковый номер дня недели первого числа

        private int[] MakeMass(int daysCount)
        {
            int[] count = new int[42];

            for (int i = 1; i <= daysCount; i++)
                count[i - 1] = i;
            for (int i = daysCount; i < 42; i++)
                count[i] = 0;
            return count;
        }                   // Получаем массив с днями месяца (от 1 до 28,29,30,31)

        private int CountDays(int monthNum, int yearNum)
        {
            int[] month31 = {1, 3, 5, 7, 8, 10, 12 };
            int[] month30 = {4, 6, 9, 11};

            if (month31.Contains(monthNum))
                return 31;
            else if (month30.Contains(monthNum))
                return 30;
            else
            {
                if (yearNum % 4 != 0)
                    return 28;
                else return 29;
            }
        }        // Получаем количество дней в месяце

        private void ShowFullMonth(int[] result, int num)
        {
            int[] days = new int[] { };
            if (num != 1)
                days = PutZeroes(result, num); // Заполняем в начале нулями, если начинаем не с пн
            else
                days = result;


            int row = 0;
            for (int j = 0; j < 6; j++)     // кол-во рядов
            {
                for (int i = 0; i < 7; i++) // кол-во столбиков
                {
                    Label label1 = new Label()
                    {
                        FontSize = 16,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        HeightRequest = 50
                    };

                    if(i == 5 || i == 6)                                
                        label1.TextColor = Color.Red;                   // Помечаем выходные красным цветом
                    try
                    {
                        if (days[i + row] != 0)
                            label1.Text = days[i + row].ToString();         // Делаем label со значением = число месяца
                    }
                    catch(Exception e) { }
                    

                    if (label1.Text == DateTime.Now.Day.ToString() && Month == ThisMonth && Year == ThisYear)     // Выделяем цветом label 
                        label1.BackgroundColor = Color.LightBlue;       // со значением = сегодняшнее число


                    Grid.SetColumn(label1, i);
                    Grid.SetRow(label1, j);                    
                    grid1.Children.Add(label1);
                }
                row+=7;
            }
        }       // Выводим месяц на экран

        private int[] PutZeroes(int[] days, int num)
        { // num = 0?
            if (num == 0)
                num = 7;

            int[] result = new int[days.Length];
            
            for (int i = 0; i < num - 1; i++)
                result[i] = 0;

            for (int i = num - 1; i < days.Length; i++)
                result[i] = days[i - num + 1];
            return result;
        }            // Ставим лишние нули в начале

        private string GetMonthOfNum(int CurrMonth)
        {
            switch (CurrMonth)
            {
                case 1:
                    return "Январь";
                case 2:
                    return "Февраль";
                case 3:
                    return "Март";
                case 4:
                    return "Апрель";
                case 5:
                    return "Май";
                case 6:
                    return "Июнь";
                case 7:
                    return "Июль";
                case 8:
                    return "Август";
                case 9:
                    return "Сентябрь";
                case 10:
                    return "Октябрь";
                case 11:
                    return "Ноябрь";
                default:
                    return "Декабрь";
            }
        }             // Получаем порядковый номер месяца

        private void Left_ArrowButton_Clicked(object sender, EventArgs e)
        {
            int thisMonthDays = CountDays(Month, Year);
            Month -= 1;
            if (Month == 0)
            {
                Month = 12;
                Year--;
            } 

            CurrentDate.Text = $"{GetMonthOfNum(Month)} {Year}";
            string where = "prev";

            int daysCount = CountDays(Month, Year);
            int newNumOfDayOfWeek = GetNewMonthDOW(where, daysCount, thisMonthDays); 

            Days = MakeMass(daysCount);
            grid1.Children.Clear();
            ShowFullMonth(Days, newNumOfDayOfWeek);                

        }       // Выводим предыдущий месяц

        private void Right_ArrowButton_Clicked(object sender, EventArgs e)
        {
            int thisMonthDays = CountDays(Month, Year);
            Month += 1;
            if (Month == 13)
            {
                Month = 1;
                Year++;
            }

            CurrentDate.Text = $"{GetMonthOfNum(Month)} {Year}";
            string where = "next";

            int daysCount = CountDays(Month, Year);
            int newNumOfDayOfWeek = GetNewMonthDOW(where, daysCount, thisMonthDays);

            Days = MakeMass(daysCount);
            grid1.Children.Clear();
            ShowFullMonth(Days, newNumOfDayOfWeek);

        }       // Выводим следующий месяц

        private int GetNewMonthDOW(string where, int daysCount, int thisMonthDaysCount)
        {
            // результат работы функции - число от 1 до 7 - номер дня недели первого числа нового месяца

            // daysCount - кол-во дней в новом месяце.
            // thisMonthDaysCount - кол-во дней в старом месяце. 
            //int kk;
            if (where == "prev")            // листаем назад календарь
            {
                //if(thisMonthDaysCount == 31 || thisMonthDaysCount == 30 || thisMonthDaysCount == 29 || thisMonthDaysCount == 28)
                //{
                if (daysCount == 31)
                {
                    numOfDayOfWeek = (numOfDayOfWeek + 4) % 7;
                    return numOfDayOfWeek;
                }
                if (daysCount == 30)
                {
                    numOfDayOfWeek = (numOfDayOfWeek + 5) % 7;
                    return numOfDayOfWeek;
                }
                    
                if (daysCount == 29)
                {
                    numOfDayOfWeek = (numOfDayOfWeek + 6) % 7;
                    return numOfDayOfWeek;
                }
                else return numOfDayOfWeek;
                //}
            }
            else                            // листаем вперед календарь
            {
                if (thisMonthDaysCount == 31)
                {
                    numOfDayOfWeek = (numOfDayOfWeek + 3) % 7;
                    return numOfDayOfWeek;
                }
                    
                if (thisMonthDaysCount == 30)
                {
                    numOfDayOfWeek = (numOfDayOfWeek + 2) % 7;
                    return numOfDayOfWeek;
                }
                    
                if (thisMonthDaysCount == 29)
                {
                    numOfDayOfWeek = (numOfDayOfWeek + 1) % 7;
                    return numOfDayOfWeek;
                }
                else
                    return numOfDayOfWeek;
            }
        }  // Получаем день недели первого числа нового месяца

        

        private void ShowCurrentMonth_Clicked(object sender, EventArgs e) // fix pls
        {
            CurrentDate.Text = $"{GetMonthOfNum(ThisMonth)} {ThisYear}";                // выводим месяц и год на экран 
            Month = ThisMonth;
            Year = ThisYear;
            int numOfFirstDayOfWeek = GetThisMonthFirstDayDOW(ThisDayOfWeek);  // получаем порядковый номер дня недели первого числа
            int daysCount = CountDays(ThisMonth, ThisYear);                             // получаем количество дней в месяце
            Days = MakeMass(daysCount);                                         // получаем массив с днями месяца (от 1 до 28,29,30,31)
            grid1.Children.Clear();
            ShowFullMonth(Days, numOfFirstDayOfWeek);
        }


        // Если длина текущего месяца = 30, то
        // ДНПЧ след. месяца = ДНПЧ текущего месяца +2 / -5,
        // ДНПЧ пред. месяца = ДНПЧ текущего месяца +4 / -3,


        // если длина текущего месяца = 31, а след. = 30, то
        // ДНПЧ след. месяца = ДНПЧ текущего месяца +3 / -4,

        // если длина текущего месяца = 31, а пред. = 30, то
        // ДНПЧ пред. месяца = ДНПЧ текущего месяца -2 / +5,


        // если длина текущего месяца = 31, а след. = 31, то
        // ДНПЧ след. месяца = ДНПЧ текущего месяца +3 / -4,

        // если длина текущего месяца = 31, а пред. = 31, то
        // ДНПЧ пред. месяца = ДНПЧ текущего месяца +4 / -3,


        // Для главной страницы:
        // CurrDay = сегодняшний день, CurrMonth = сегодняшний месяц, CurrYear = сегодняшний год,
        // Все переменные - числа (месяц - номер месяца по порядку)

        // Если нажата левая стрелка, то вызываем функцию, в которую передаем все переменные,
        // В этой функции делаем номер месяца -= 1 и  если номер месяца = 12, то год -= 1
        // передаем номер месяца и год в функцию GetMonthLeng(month, year) и возвращаем кол-во дней в месяце
        // После чего у нас есть кол-во дней, номер месяца, год для отображения

        // Если нажата правая стрелка, то вызываем функцию, в которую передаем все переменные,
        // В этой функции делаем номер месяца += 1 и если номер месяца = 13, то год += 1, а номер месяца делаем = 1,
        // далее по аналогии с левой стрелкой.


        // днпч = ДНПЧ
        // Определим ДНПЧ числа след./пред. месяца:
        // Для начала определим ДНПЧ текущего месяца
    }
}
