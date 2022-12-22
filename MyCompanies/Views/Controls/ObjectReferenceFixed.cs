using System.Windows;

namespace MyCompanies.Views.Controls
{
    public abstract class ObjectReferenceFixedBase : Freezable
    {
        public static readonly DependencyProperty ReferencedObjectProperty = DependencyProperty.Register("ReferencedObject", typeof(object), typeof(ObjectReferenceFixedBase), new PropertyMetadata(null));
    }

    public class ObjectReferenceFixed : ObjectReferenceFixedBase
    {
        protected override Freezable CreateInstanceCore()
        {
            return new ObjectReferenceFixed();
        }

        public object ReferencedObject
        {
            get => GetValue(ReferencedObjectProperty);
            set => SetValue(ReferencedObjectProperty, value);
        }
    }
}
