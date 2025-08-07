namespace BlazorComponents.Components.DynamicListView
{
    public class ColumnDefinition<T>
    {
        public string Header { get; set; } = string.Empty;
        public Func<T, object?> ValueSelector { get; set; } = _ => null;
        public string? CssClass { get; set; }
        public string? Width { get; set; } = "auto";
    }
}
