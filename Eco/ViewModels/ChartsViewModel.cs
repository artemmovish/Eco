using Microcharts;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Globalization;
using Eco.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Eco.ViewModels
{
    public partial class ChartsViewModel : ObservableObject
    {
        private readonly TransactionService _transactionService;

        [ObservableProperty]
        private Chart _chart;

        [ObservableProperty]
        private string _currentChartType = "expense";

        [ObservableProperty]
        private string _currentPeriod = "month";

        public ObservableCollection<SummaryData> SummaryData { get; set; } = new();

        public ChartsViewModel(TransactionService transactionService)
        {
            _transactionService = transactionService;
            LoadData();
        }

        [RelayCommand]
        private void ChangeChartType(string type)
        {
            CurrentChartType = type;
            UpdateChart();
        }

        [RelayCommand]
        private void ChangePeriod(string period)
        {
            CurrentPeriod = period;
            LoadData();
        }

        private async void LoadData()
        {
            var summary = await _transactionService.GetSummary();

            // Очищаем предыдущие данные
            SummaryData.Clear();

            // Добавляем общие данные
            SummaryData.Add(new SummaryData
            {
                Period = "Общий",
                Income = summary.TotalIncome,
                Expense = summary.TotalExpense
            });

            // Обновляем диаграмму
            UpdateChart();
        }

        private void UpdateChart()
        {
            var entries = new List<ChartEntry>();
            var summary = SummaryData.FirstOrDefault(); // Берем первый элемент (общие данные)

            if (summary == null) return;

            if (CurrentChartType == "all")
            {
                // Общая диаграмма (доходы и расходы)
                entries.Add(new ChartEntry((float)summary.Income)
                {
                    Label = "Доходы",
                    ValueLabel = summary.Income.ToString("C", CultureInfo.CurrentCulture),
                    Color = SKColor.Parse("#2E8B57") // Зеленый
                });

                entries.Add(new ChartEntry((float)summary.Expense)
                {
                    Label = "Расходы",
                    ValueLabel = summary.Expense.ToString("C", CultureInfo.CurrentCulture),
                    Color = SKColor.Parse("#CD5C5C") // Красный
                });

                Chart = new BarChart()
                {
                    Entries = entries,
                    LabelTextSize = 14,
                    BackgroundColor = SKColors.Transparent
                };
            }
            else if (CurrentChartType == "income")
            {
                // Диаграмма доходов по категориям
                var summaryFull = _transactionService.GetSummary().Result;

                foreach (var category in summaryFull.Categories.Income)
                {
                    entries.Add(new ChartEntry((float)category.Value)
                    {
                        Label = category.Key,
                        ValueLabel = category.Value.ToString("C", CultureInfo.CurrentCulture),
                        Color = SKColor.Parse("#2E8B57") // Зеленый
                    });
                }

                Chart = new DonutChart()
                {
                    Entries = entries,
                    LabelTextSize = 14,
                    BackgroundColor = SKColors.Transparent,
                    HoleRadius = 0.4f
                };
            }
            else // "expense"
            {
                // Диаграмма расходов по категориям
                var summaryFull = _transactionService.GetSummary().Result;

                foreach (var category in summaryFull.Categories.Expense)
                {
                    entries.Add(new ChartEntry((float)category.Value)
                    {
                        Label = category.Key,
                        ValueLabel = category.Value.ToString("C", CultureInfo.CurrentCulture),
                        Color = SKColor.Parse("#CD5C5C") // Красный
                    });
                }

                Chart = new DonutChart()
                {
                    Entries = entries,
                    LabelTextSize = 14,
                    BackgroundColor = SKColors.Transparent,
                    HoleRadius = 0.4f
                };
            }
        }
    }

    public class SummaryData
    {
        public string Period { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
    }
}