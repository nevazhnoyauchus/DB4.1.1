﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace database2
{
    class PageChange : INotifyPropertyChanged  // класс, который наследуется от интерфейса INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;  //сибытие, для изменения значения одного из свойтс, описанных ниже
        static int countitems = 5; //количество объектов для отображения
        public int[] NPage { get; set; } = new int[countitems];// массив с номерами отображаемых страниц    
        public string[] Visible { get; set; } = new string[countitems];//массив свойст, отвечающий за видимость номера страницы, Visible - видимый, Hidden - скрытый
        public string[] Bold { get; set; } = new string[countitems];//массив свойств, отвечающий за выделение номера текущей страницы
        int countpages;  // переменная, в которой буде храниться количество страниц
        public int CountPages//свойство в котором хранится общее кол-во страц, при изменении данного свойства будет определяться, скрыт будет номер той или итой страницы или нет (в зависимости об общего кол-ва записей в списке) 
        {
            get => countpages;
            set
            {
                countpages = value;
                for (int i = 1; i < countitems; i++)//цикл для определения видимости номеров страниц
                {
                    if (CountPages <= i)
                    {
                        Visible[i] = "Hidden";//если страниц меньше, чем кнопок - скрываем лишние
                    }
                    else
                    {
                        Visible[i] = "Visible";// а если их опять стало больше, то показываем назад
                    }
                }
            }
        }

        int countpage;//количество записей на странице
        public int CountPage  //свойство, в котором хранится количество записей на странице, при изменении данного свойства будет изменяться общее количесво страниц для отображения
        {
            get => countpage;
            set
            {
                countpage = value;
                if (Countlist % value == 0)
                {
                    CountPages = Countlist / value;//определение количества страниц
                }
                else
                {
                    CountPages = Countlist / value + 1;//в случае нецелого числа прибавляем 1 к итоговому количеству страниц
                }
            }
        }

        int countlist; // количество записей в списке
        public int Countlist //свойство, в котором хранится общее количество записей в списке, при изменении данного свойства будет изменяться общее количесво страниц для отображения
        {
            get => countlist;
            set
            {
                countlist = value;
                if (value % CountPage == 0)
                {
                    CountPages = value / CountPage;//определение количества страниц
                }
                else
                {
                    CountPages = 1 + value / CountPage;
                }
            }
        }
        int currentpage;//текущая страница
        public int CurrentPage  // свойство, в котором будет хранится текущая страница, при изменении которого будет меняться вся отрисовка меню с номерами страниц
        {
            get => currentpage;
            set
            {
                currentpage = value;
                if (currentpage < 1)
                {
                    currentpage = 1;
                }
                if (currentpage >= CountPages)
                {
                    currentpage = CountPages;
                }
                //отрисовка меню с номерами страниц, рассмотрим три возможных случая                            
                for (int i = 0; i < countitems; i++)
                {
                    if (currentpage < (1 + countitems / 2) || CountPages < countitems) NPage[i] = i + 1;//если страница в начале списка
                    else if (currentpage > CountPages - (countitems / 2 + 1)) NPage[i] = CountPages - (countitems - 1) + i;//если страница в конце списка
                    else NPage[i] = currentpage + i - (countitems / 2);//если страница в середине списка
                }
                for (int i = 0; i < countitems; i++)//выделяем активную страницу жирным
                {
                    if (NPage[i] == currentpage) Bold[i] = "ExtraBold";
                    else Bold[i] = "Regular";
                }
                //вызываем созбытие, связанное с изменением свойств, используемых в привязке на странице
                PropertyChanged(this, new PropertyChangedEventArgs("NPage"));
                PropertyChanged(this, new PropertyChangedEventArgs("Visible"));
                PropertyChanged(this, new PropertyChangedEventArgs("Bold"));
            }
        }
        public PageChange() // контруктор
        {
            for (int i = 0; i < countitems; i++)  // показываем исходное меню ( 1 2 3 4 5)
            {
                Visible[i] = "Visible";
                NPage[i] = i + 1;
                Bold[i] = "Regular";
            }
            currentpage = 1;  // по умолчанию 1-ая страница будет текущей
            countpage = 1;  // по умолчанию все записи будут отображаться на одной странице
            countlist = 1;  // по умолчанию в общес списке будет только одна запись
        }
    }
}

