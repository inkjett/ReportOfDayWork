using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportOfDayWork
{
    class SampleColumn
    {
        //public string Name { get; set; } //обязательно нужно использовать get конструкцию
        public string Data { get; set; } //Данное свойство не будет отображаться как колонка

        public SampleColumn(string data)
        {
            //Name = name;
            Data = data;
        }

    }
}
