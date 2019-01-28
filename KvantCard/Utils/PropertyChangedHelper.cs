using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace KvantCard.Utils
{
    public class PropertyChangedHelper
    {
        public static void RaisePropertyChanged<T>(object sender, PropertyChangedEventHandler propertyChanged, Expression<Func<T>> propertyExpression)
        {
            propertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyExpression.GetPropertyName()));
        }

        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return propertyExpression.GetPropertyName();
        }

        public static void SetProperty<T>(object sender, PropertyChangedEventHandler propertyChanged,
            ref T backingField, T newValue, Expression<Func<T>> propertyExpression)
        {
            if (backingField == null && newValue == null)
            {
                return;
            }

            if (backingField == null || !backingField.Equals(newValue))
            {
                backingField = newValue;
                RaisePropertyChanged(sender, propertyChanged, propertyExpression);
            }
        }

    }
}
