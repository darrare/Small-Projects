using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;

namespace BlazorComponents.Components.DynamicListView
{
    public class ClickEventArgs<T>
    {
        public T Item { get; set; } = default!;
        [Required] public ColumnDefinition<T> Column { get; set; }


        /// <summary>
        /// MouseEventArgs
        /// Check MouseEventArgs.Button
        ///     0: Left Button
        ///     1: Middle Button
        ///     2: Right Button
        /// </summary>
        [Required] public MouseEventArgs MouseEventArgs { get; set; }
    }
}
