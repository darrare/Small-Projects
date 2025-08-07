using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Security.Cryptography.X509Certificates;

namespace BlazorComponents.Components.DynamicListView
{
    public enum SelectionMode
    {
        NONE,
        CLICK,
        SINGLE,
        MULTI
    }

    public partial class DynamicListView<T> : ComponentBase
    {
        [Parameter] public List<T> Items { get; set; } = new();
        [Parameter] public List<ColumnDefinition<T>> Columns { get; set; } = new();
        [Parameter] public int DefaultSortColumnIndex { get; set; } = 0;
        [Parameter] public SelectionMode SelectionMode { get; set; } = SelectionMode.NONE;
        [Parameter] public EventCallback<ClickEventArgs<T>> OnClick { get; set; }
        [Parameter] public EventCallback<List<T>> SelectedRowsChanged { get; set; }
        [Parameter] public bool AllowBrowserRightCLickMenu { get; set; } = false;

        protected List<T> SelectedRows { get; set; } = new();

        protected string? SortColumn;
        protected bool SortDescending;

        protected override void OnParametersSet()
        {
            validateOrThrow();
            if (!Items.Any() || !Columns.Any())
            {
                return;
            }

            // If already sorted, just update the list without resetting sort
            if (!string.IsNullOrEmpty(SortColumn))
            {
                ApplySort();
            }
            else
            {
                var defaultSortColumn = DefaultSortColumnIndex < Columns.Count 
                    ? Columns[DefaultSortColumnIndex] 
                    : Columns.First();
                SortBy(defaultSortColumn);
            }
        }

        private void validateOrThrow()
        {
            if (SelectionMode == SelectionMode.CLICK && !OnClick.HasDelegate)
            {
                throw new Exception("When Selection mode is set to CLICK, OnClick EventCallback must be set.");
            }
            if ((SelectionMode == SelectionMode.NONE || SelectionMode == SelectionMode.CLICK) && SelectedRowsChanged.HasDelegate)
            {
                throw new Exception("");
            }
        }

        private void SortBy(ColumnDefinition<T> column)
        {
            if (column.Header == null)
                return;

            if (SortColumn == column.Header)
            {
                SortDescending = !SortDescending;
            }
            else
            {
                SortColumn = column.Header;
                SortDescending = false;
            }

            ApplySort();
        }

        private void ApplySort()
        {
            if (SortColumn != null)
            {
                var column = Columns.FirstOrDefault(c => c.Header == SortColumn);
                if (column != null)
                {
                    var sorted = SortDescending
                        ? Items.OrderByDescending(column.ValueSelector)
                        : Items.OrderBy(column.ValueSelector);

                    Items = sorted.ToList();
                }
            }
            else
            {
                Items = Items.ToList();
            }
        }

        private async Task HandleClick(T item, ColumnDefinition<T> column, MouseEventArgs e)
        {
            switch (SelectionMode)
            {
                case SelectionMode.NONE:
                    return;
                case SelectionMode.CLICK:
                    if (OnClick.HasDelegate)
                    {
                        await OnClick.InvokeAsync(new ClickEventArgs<T>
                        {
                            Item = item,
                            Column = column,
                            MouseEventArgs = e
                        });
                    }
                    break;
                case SelectionMode.SINGLE:
                    SelectedRows.Clear();
                    SelectedRows.Add(item);
                    break;
                case SelectionMode.MULTI:
                    if (SelectedRows.Contains(item))
                        SelectedRows.Remove(item);
                    else
                        SelectedRows.Add(item);
                    break;
                default:
                    break;
            }
        }
    }
}