using System.Runtime.CompilerServices;

namespace TheDivisionUtility.TheDivision.Gear.Module.Validation
{
    public interface INotifyDataErrorInfoPropertyValidator
    {
        void Validate([CallerMemberName] string propertyName = null);
    }
}
