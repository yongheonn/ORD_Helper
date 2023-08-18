using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ORD_MainView_UnitInfo
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UnitInfo : UserControl
    {
        public UnitInfo()
        {
            InitializeComponent();
        }
        
    }

    public class ParameterConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Tuple<object, object> tuple = new Tuple<object, object>(values[0], values[1]);
            return tuple;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static ParameterConverter _converter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null) _converter = new ParameterConverter();
            return _converter;
        }

        public ParameterConverter()
            : base()
        {
        }
    }

    public class CustomPanel : Panel
    {
        class MinColumn :IComparable
        {
            public int index;
            public double height;

            public int CompareTo(object obj)
            {
                if (height > (obj as MinColumn).height)
                    return 1;
                else if (height < (obj as MinColumn).height)
                    return -1;
                else
                {
                    if (index > (obj as MinColumn).index)
                        return 1;
                    else
                        return -1;
                }
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            double totalWidth = 0;
            UIElementCollection elements = base.InternalChildren;
            double[] x = new double[elements.Count];
            double[] y = new double[elements.Count];

            MinColumn[] minColumns = new MinColumn[1];
            bool check = false;
            for (int n = 0; n < elements.Count; n++)
            {
                elements[n].Measure(availableSize);
                totalWidth += elements[n].DesiredSize.Width;

                if (totalWidth > availableSize.Width && check == false)
                {
                    check = true;
                    minColumns = new MinColumn[n];
                    for (int i = 0; i < n; i++)
                    {
                        minColumns[i] = new MinColumn();
                        minColumns[i].height = elements[i].DesiredSize.Height;
                        minColumns[i].index = i;
                    }

                    Array.Sort(minColumns);

                    totalWidth = elements[n].DesiredSize.Width;
                    x[n] = x[minColumns[0].index];
                    y[n] = y[minColumns[0].index] + minColumns[0].height;
                    elements[n].Arrange(new Rect(x[n], y[n], elements[n].DesiredSize.Width, elements[n].DesiredSize.Height));
                    minColumns[0].height += elements[n].DesiredSize.Height;
                }
                else if (check == false)
                {
                    if (n == 0)
                    {
                        x[n] = 0;
                        y[n] = 0;
                        elements[n].Arrange(new Rect(x[n], y[n], elements[n].DesiredSize.Width, elements[n].DesiredSize.Height));
                    }
                    else
                    {
                        x[n] = x[n - 1] + elements[n - 1].DesiredSize.Width;
                        y[n] = y[n - 1];
                        elements[n].Arrange(new Rect(x[n], y[n], elements[n].DesiredSize.Width, elements[n].DesiredSize.Height));
                    }
                }
                else
                {
                    Array.Sort(minColumns);
                    x[n] = x[minColumns[0].index];
                    y[n] = y[minColumns[0].index] + minColumns[0].height;
                    elements[n].Arrange(new Rect(x[n], y[n], elements[n].DesiredSize.Width, elements[n].DesiredSize.Height));
                    minColumns[0].height += elements[n].DesiredSize.Height;
                }
            }
            if (check == true)
                return new Size(availableSize.Width, minColumns[minColumns.Length - 1].height);
            else
                return new Size(availableSize.Width, 10000);
        }
    }
}
