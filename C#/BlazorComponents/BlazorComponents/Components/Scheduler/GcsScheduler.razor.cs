using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.Globalization;

namespace BlazorComponents.Components.Scheduler
{
    public partial class GcsScheduler : ComponentBase
    {
        private ElementReference _scrollContainer;
        private int CurrentMonth = DateTime.Now.Month;
        private int CurrentYear = DateTime.Now.Year;

        [Parameter]
        public List<Event> Events { get; set; }

        private IJSObjectReference _jsModule;

        // Virtualize configuration
        private const int TOTAL_WEEKS = 10000;
        private readonly int CENTER_INDEX = TOTAL_WEEKS / 2;
        private readonly DateTime _baseDate = DateTime.Today;

        // Load weeks for Virtualize
        private async ValueTask<ItemsProviderResult<WeekViewModel>> LoadWeeks(ItemsProviderRequest request)
        {
            var items = new List<WeekViewModel>(request.Count);
            var startIndex = request.StartIndex;
            var count = request.Count;

            for (int i = 0; i < count; i++)
            {
                int index = startIndex + i;
                int offsetWeeks = index - CENTER_INDEX;
                items.Add(GetWeekForOffset(offsetWeeks));
            }

            return new ItemsProviderResult<WeekViewModel>(items, TOTAL_WEEKS);
        }

        private string GetMonthName(int month) => CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[month - 1];

        private WeekViewModel GetWeekForOffset(int weekOffset)
        {
            var target = _baseDate.AddDays(weekOffset * 7);
            var start = target.StartOfWeek(DayOfWeek.Sunday);
            var days = Enumerable.Range(0, 7).Select(d => start.AddDays(d)).ToList();
            return new WeekViewModel(start, days, Events);
        }


        private string GetDayCss(DateTime day)
        {
            var classes = "day-cell text-center";
            if (day.Month == CurrentMonth && day.Year == CurrentYear) classes += " current-month";
            else classes += " text-muted";
            if (day.Date == DateTime.Today) classes += " today"; // extra class

            return classes;
        }

        private async Task GoToDate(DateTime date)
        {
            if (_jsModule != null)
            {
                // Calculate how many weeks the target date is from the base date
                int weekOffset = (int)((date.Date - _baseDate).TotalDays / 7);

                // Translate to Virtualize index
                int targetIndex = CENTER_INDEX + weekOffset;

                await _jsModule.InvokeVoidAsync("scrollToIndex", _scrollContainer, targetIndex, 150);

                CurrentMonth = date.AddDays(10).Month;
                CurrentYear = date.AddDays(10).Year;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _jsModule = await JS.InvokeAsync<IJSObjectReference>("import", "./Components/Scheduler/GcsScheduler.razor.js");
                var dotnetRef = DotNetObjectReference.Create(this);

                // Scroll to today initially
                await GoToDate(DateTime.Now);

                // Initialize month header updates
                await _jsModule.InvokeVoidAsync("initializeScrollListener", _scrollContainer, dotnetRef);
            }
        }

        [JSInvokable]
        public void UpdateCurrentMonth(string isoWeekStart)
        {
            if (DateTime.TryParse(isoWeekStart, null, DateTimeStyles.RoundtripKind, out var visibleWeekStart))
            {
                // 10 days seems like the right offset so that the most 
                // visible month is correctly marked as the current month
                CurrentMonth = visibleWeekStart.AddDays(10).Month;
                CurrentYear = visibleWeekStart.AddDays(10).Year;
                StateHasChanged();
            }
        }
    }
}
