using Mazes.Wpf.Helpers;
using System.ComponentModel;

namespace Mazes.Wpf.Model
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Algorithm
    {
        [Description("Binary Tree")]
        BinaryTree,

        [Description("Sidewinder")]
        Sidewinder
    }
}
