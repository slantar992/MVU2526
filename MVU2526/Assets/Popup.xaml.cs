#if UNITY_5_3_OR_NEWER
    #define NOESIS
    using Noesis;
#else
    using System.Windows;
    using System.Windows.Controls;
#endif

namespace MVU2526
{
    public partial class Popup: UserControl
    {
        public Popup()
        {
            InitializeComponent();
        }

    #if NOESIS

        private void InitializeComponent()
        {
            NoesisUnity.LoadComponent(this);
        }
    #endif
    };
}