#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System;
using System.Windows.Controls;
#endif

namespace MVU2526
{
    /// <summary>
    /// Interaction logic for MVU2526MainView.xaml
    /// </summary>
    public partial class MVU2526MainView : UserControl
    {
        public MVU2526MainView()
        {
            InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            NoesisUnity.LoadComponent(this);
        }
#endif
    }
}
