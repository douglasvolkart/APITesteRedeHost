using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace APIDesafio.Core
{
    public class CoreController : Controller
    {
        public string ChangeDate(string date, char op, long value)
        {

            try
            {
                string[] values = date.Split('/');

                if (values.Length < 3)
                {
                    return "Data formatada incorretamente.";
                }

                string[] years = values[2].Split(' ');

                if (years.Length < 1)
                {
                    return "ano ou horas formatadas incorretamente.";
                }


                string[] hour = years[1].Split(':');

                if (hour.Length < 2)
                {
                    return "horas formatada incorretamente.";
                }

                if (value < 0)
                {
                    value = value * -1;
                }



                //converte a data para inteiros
                int minutes = int.Parse(hour[1]);
                int hours = int.Parse(hour[0]); ;
                int day = int.Parse(values[0]);
                int month = int.Parse(values[1]);
                int year = int.Parse(years[0]);

                int totalDaysOfMonth;

                //operação de adição
                if (op == '+')
                {

                    //converte tudo para minutos
                    int dayMinutes = day * 24 * 60;
                    int hourMinutes = hours * 60;

                    int totalMinutes = dayMinutes + hourMinutes + minutes + Convert.ToInt32(value);

                    //converte o a soma para dias / horas e minutos
                    int resultMinutes = totalMinutes % 60;
                    int resultHours = (totalMinutes / 60) % 24;
                    int resultDays = (totalMinutes / 60) / 24;

                    //trata o mes para pegar o total de dias do mês
                    totalDaysOfMonth = getTotalDaysOfMonth(month);



                    //compara para ver se a quantidade de dias somados é maior que o numero de dias do mês
                    if (resultDays > totalDaysOfMonth)
                    {
                        resultDays = resultDays - totalDaysOfMonth;

                        if (month == 12)
                        {
                            month = 1;
                            year += 1;
                        }
                        else
                        {
                            month += 1;
                        }


                    }

                    day = resultDays;
                    hours = resultHours;
                    minutes = resultMinutes;


                    string resultado = day.ToString().PadLeft(2, '0') + "/" + month.ToString().PadLeft(2, '0') + "/" + year.ToString() + " " + hours.ToString().PadLeft(2, '0') + ":" + minutes.ToString().PadLeft(2, '0');

                    return resultado;

                }
                //operação de subtração
                else if (op == '-')
                {

                    //converte tudo para minutos
                    int dayMinutes = day * 24 * 60;
                    int hourMinutes = hours * 60;

                    int totalMinutes = dayMinutes + hourMinutes + minutes - Convert.ToInt32(value);


                    if (totalMinutes < 0)
                    {
                        if (month > 1)
                        {
                            month = month - 1;
                        }
                        else
                        {
                            month = 12;
                            year = year - 1;
                        }

                        totalDaysOfMonth = getTotalDaysOfMonth(month);

                        int minutesToSubtract = totalMinutes * -1;

                        totalMinutes = (totalDaysOfMonth * 24 * 60) - minutesToSubtract;

                    }


                    //converte o a soma para dias / horas e minutos
                    int resultMinutes = totalMinutes % 60;
                    int resultHours = (totalMinutes / 60) % 24;
                    int resultDays = (totalMinutes / 60) / 24;


                    day = resultDays;
                    hours = resultHours;
                    minutes = resultMinutes;


                    string resultado = day.ToString().PadLeft(2, '0') + "/" + month.ToString().PadLeft(2, '0') + "/" + year.ToString() + " " + hours.ToString().PadLeft(2, '0') + ":" + minutes.ToString().PadLeft(2, '0');

                    return resultado;


                }
                //operação não identificada
                else
                {
                    return "operação não idntificada";
                }
            }
            catch (Exception)
            {

                return "error";
                throw;

            }


        }

        private int getTotalDaysOfMonth(int month)
        {
            int totalDaysOfMonth = 0;
            switch (month)
            {
                case 1:
                    totalDaysOfMonth = 31;
                    break;
                case 2:
                    totalDaysOfMonth = 28;
                    break;
                case 3:
                    totalDaysOfMonth = 31;
                    break;
                case 4:
                    totalDaysOfMonth = 30;
                    break;
                case 5:
                    totalDaysOfMonth = 31;
                    break;
                case 6:
                    totalDaysOfMonth = 30;
                    break;
                case 7:
                    totalDaysOfMonth = 31;
                    break;
                case 8:
                    totalDaysOfMonth = 31;
                    break;
                case 9:
                    totalDaysOfMonth = 31;
                    break;
                case 10:
                    totalDaysOfMonth = 30;
                    break;
                case 11:
                    totalDaysOfMonth = 31;
                    break;
                case 12:
                    totalDaysOfMonth = 31;
                    break;

                default:
                    totalDaysOfMonth = 31;
                    break;
            }
            return totalDaysOfMonth;
        }
    }
}