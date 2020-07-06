using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Wiseyard.Core.Models;
using Wiseyard.Core.Services;
using Wiseyard.Helpers;

namespace Wiseyard.ViewModels
{
    public class IncomeChartViewModel : Observable
    {
        public ObservableCollection<SeasonChartData> Source { get; } = new ObservableCollection<SeasonChartData>();

        public IncomeChartViewModel()
        {
        }

        public void LoadData()
        {
            Source.Clear();

            var seasons = SeasonService.GetAllSeasons().OrderBy(x => x.StartDate);
            var totalIncome = IncomeService.GetAllIncomes();

            foreach(var season in seasons)
            {
                string label = String.Format("{0} - {1}", season.StartDate.ToString("yyyy"), season.StartDate.AddYears(1).ToString("yyyy"));
                double income = totalIncome
                    .Where(x => season.StartDate <= x.Date && x.Date <= (season.EndDate != null ? season.EndDate : DateTime.MaxValue))
                    .Sum(x => x.Price);

                Source.Add(new SeasonChartData
                {
                    Season = label,
                    Value = income
                });
            }
        }
    }
}
